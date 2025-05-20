using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using infrastructure.Mysql;

namespace LoveCampus.infrastructure.Repositories
{
    public class InteraccionRepository : IInteraccionRepository
    {
        public InteraccionRepository()
        {
            // Constructor vacío
        }

        public void AgregarInteraccion(Interaccion interaccion)
        {
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            
            // Establecer la fecha si no está establecida
            if (interaccion.FechaInteraccion == default)
            {
                interaccion.FechaInteraccion = DateTime.Now;
            }
            
            // Verificar si ya existe una interacción similar
            string checkQuery = "SELECT id FROM interacciones WHERE id_usuario_origen = @origen AND id_usuario_destino = @destino";
            using (var cmdCheck = new MySqlCommand(checkQuery, conn))
            {
                cmdCheck.Parameters.AddWithValue("@origen", interaccion.UsuarioIdOrigen);
                cmdCheck.Parameters.AddWithValue("@destino", interaccion.UsuarioIdDestino);
                var existingId = cmdCheck.ExecuteScalar();
                
                if (existingId != null)
                {
                    // Actualizar la existente
                    string updateQuery = "UPDATE interacciones SET tipo_interaccion = @tipo, fecha_interaccion = @fecha WHERE id = @id";
                    using var cmdUpdate = new MySqlCommand(updateQuery, conn);
                    cmdUpdate.Parameters.AddWithValue("@tipo", interaccion.TipoInteraccion);
                    cmdUpdate.Parameters.AddWithValue("@fecha", interaccion.FechaInteraccion);
                    cmdUpdate.Parameters.AddWithValue("@id", existingId);
                    cmdUpdate.ExecuteNonQuery();
                    return;
                }
            }
            
            // Agregar nueva interacción
            string insertQuery = "INSERT INTO interacciones (id_usuario_origen, id_usuario_destino, tipo_interaccion, fecha_interaccion) VALUES (@origen, @destino, @tipo, @fecha)";
            using var cmdInsert = new MySqlCommand(insertQuery, conn);
            cmdInsert.Parameters.AddWithValue("@origen", interaccion.UsuarioIdOrigen);
            cmdInsert.Parameters.AddWithValue("@destino", interaccion.UsuarioIdDestino);
            cmdInsert.Parameters.AddWithValue("@tipo", interaccion.TipoInteraccion);
            cmdInsert.Parameters.AddWithValue("@fecha", interaccion.FechaInteraccion);
            cmdInsert.ExecuteNonQuery();
            
            // Obtener el ID generado
            interaccion.Id = (int)cmdInsert.LastInsertedId;
        }

        public List<Interaccion> ObtenerInteraccionesPorUsuario(int usuarioId)
        {
            var interacciones = new List<Interaccion>();
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            
            string query = "SELECT * FROM interacciones WHERE id_usuario_origen = @usuarioId OR id_usuario_destino = @usuarioId";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                interacciones.Add(new Interaccion
                {
                    Id = reader.GetInt32("id"),
                    UsuarioIdOrigen = reader.GetInt32("id_usuario_origen"),
                    UsuarioIdDestino = reader.GetInt32("id_usuario_destino"),
                    TipoInteraccion = reader.GetString("tipo_interaccion"),
                    FechaInteraccion = reader.GetDateTime("fecha_interaccion")
                });
            }
            
            return interacciones;
        }

        public List<Interaccion> ObtenerTodos()
        {
            var interacciones = new List<Interaccion>();
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            
            string query = "SELECT * FROM interacciones";
            using var cmd = new MySqlCommand(query, conn);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                interacciones.Add(new Interaccion
                {
                    Id = reader.GetInt32("id"),
                    UsuarioIdOrigen = reader.GetInt32("id_usuario_origen"),
                    UsuarioIdDestino = reader.GetInt32("id_usuario_destino"),
                    TipoInteraccion = reader.GetString("tipo_interaccion"),
                    FechaInteraccion = reader.GetDateTime("fecha_interaccion")
                });
            }
            
            return interacciones;
        }

        public bool ExisteLikeMutuo(int usuarioOrigen, int usuarioDestino)
        {
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            
            // Buscar like de origen a destino
            string queryOrigen = "SELECT COUNT(*) FROM interacciones WHERE id_usuario_origen = @origen AND id_usuario_destino = @destino AND tipo_interaccion = 'LIKE'";
            using (var cmdOrigen = new MySqlCommand(queryOrigen, conn))
            {
                cmdOrigen.Parameters.AddWithValue("@origen", usuarioOrigen);
                cmdOrigen.Parameters.AddWithValue("@destino", usuarioDestino);
                int countOrigen = Convert.ToInt32(cmdOrigen.ExecuteScalar());
                
                if (countOrigen == 0)
                    return false;
            }
            
            // Buscar like de destino a origen
            string queryDestino = "SELECT COUNT(*) FROM interacciones WHERE id_usuario_origen = @origen AND id_usuario_destino = @destino AND tipo_interaccion = 'LIKE'";
            using (var cmdDestino = new MySqlCommand(queryDestino, conn))
            {
                cmdDestino.Parameters.AddWithValue("@origen", usuarioDestino);
                cmdDestino.Parameters.AddWithValue("@destino", usuarioOrigen);
                int countDestino = Convert.ToInt32(cmdDestino.ExecuteScalar());
                
                return countDestino > 0;
            }
        }
    }
}
