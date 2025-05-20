using MySql.Data.MySqlClient;
using infrastructure.Mysql;
using System.Collections.Generic;
using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;

namespace LoveCampus.infrastructure.Mysql.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void CrearUsuario(Usuario usuario)
        {
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            // Incluir id_cuenta en la inserción
            string query = "INSERT INTO usuarios (nombre, genero, edad, carrera, frase_perfil, id_ciudad, id_cuenta) " +
                           "VALUES (@nombre, @genero, @edad, @carrera, @frasePerfil, @ciudad, @idCuenta); SELECT LAST_INSERT_ID();";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@genero", usuario.Genero);
            cmd.Parameters.AddWithValue("@edad", usuario.Edad);
            cmd.Parameters.AddWithValue("@carrera", usuario.Carrera);
            cmd.Parameters.AddWithValue("@frasePerfil", usuario.FrasePerfil);
            cmd.Parameters.AddWithValue("@ciudad", usuario.IdCiudad);
            cmd.Parameters.AddWithValue("@idCuenta", usuario.IdCuenta);
            
            // Ejecutar y obtener el ID generado
            var id = Convert.ToInt32(cmd.ExecuteScalar());
            usuario.Id = id;
            
            Console.WriteLine($"Usuario creado con ID: {id} y asociado a la cuenta ID: {usuario.IdCuenta}");
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            string query = "SELECT * FROM usuarios WHERE id = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Genero = reader.GetString("genero"),
                    Edad = reader.GetInt32("edad"),
                    Carrera = reader.GetString("carrera"),
                    FrasePerfil = reader.GetString("frase_perfil"),
                    IdCiudad = reader.GetInt32("id_ciudad"),
                    IdCuenta = reader.IsDBNull(reader.GetOrdinal("id_cuenta")) ? 0 : reader.GetInt32("id_cuenta")
                };
            }
            return null;
        }
        
        public Usuario ObtenerUsuarioPorIdCuenta(int idCuenta)
        {
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            string query = "SELECT * FROM usuarios WHERE id_cuenta = @idCuenta";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idCuenta", idCuenta);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Genero = reader.GetString("genero"),
                    Edad = reader.GetInt32("edad"),
                    Carrera = reader.GetString("carrera"),
                    FrasePerfil = reader.GetString("frase_perfil"),
                    IdCiudad = reader.GetInt32("id_ciudad"),
                    IdCuenta = idCuenta
                };
            }
            return null;
        }

        public List<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            string query = "SELECT * FROM usuarios";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    Genero = reader.GetString("genero"),
                    Edad = reader.GetInt32("edad"),
                    Carrera = reader.GetString("carrera"),
                    FrasePerfil = reader.GetString("frase_perfil"),
                    IdCiudad = reader.GetInt32("id_ciudad")
                });
            }
            return lista;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            string query = "UPDATE usuarios SET nombre=@nombre, genero=@genero, edad=@edad, carrera=@carrera, frase_perfil=@frasePerfil, id_ciudad=@ciudad WHERE id=@id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@genero", usuario.Genero);
            cmd.Parameters.AddWithValue("@edad", usuario.Edad);
            cmd.Parameters.AddWithValue("@carrera", usuario.Carrera);
            cmd.Parameters.AddWithValue("@frasePerfil", usuario.FrasePerfil);
            cmd.Parameters.AddWithValue("@ciudad", usuario.IdCiudad);
            cmd.Parameters.AddWithValue("@id", usuario.Id);
            cmd.ExecuteNonQuery();
        }

        public void EliminarUsuario(int id)
        {
            using var conn = ConexionSingleton.ObtenerNuevaConexion();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            // Comenzar una transacción para asegurar la integridad de los datos
            using var transaction = conn.BeginTransaction();

            try
            {
                // Primero obtenemos el usuario para saber su id_cuenta
                int idCuenta = 0;
                string queryUsuario = "SELECT id_cuenta FROM usuarios WHERE id = @id";
                using (var cmdGetUsuario = new MySqlCommand(queryUsuario, conn, transaction))
                {
                    cmdGetUsuario.Parameters.AddWithValue("@id", id);
                    var result = cmdGetUsuario.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        idCuenta = Convert.ToInt32(result);
                    }
                }

                // 1. Eliminar referencias en la tabla preferencias_usuario
                string deletePreferenciasQuery = "DELETE FROM preferencias_usuario WHERE id_usuario = @id";
                using var cmdPreferencias = new MySqlCommand(deletePreferenciasQuery, conn, transaction);
                cmdPreferencias.Parameters.AddWithValue("@id", id);
                cmdPreferencias.ExecuteNonQuery();

                // 2. Eliminar referencias en la tabla intereses_usuario
                string deleteInteresesQuery = "DELETE FROM intereses_usuario WHERE id_usuario = @id";
                using var cmdIntereses = new MySqlCommand(deleteInteresesQuery, conn, transaction);
                cmdIntereses.Parameters.AddWithValue("@id", id);
                cmdIntereses.ExecuteNonQuery();

                // 3. Eliminar referencias en la tabla estadisticas_usuario
                string deleteEstadisticasQuery = "DELETE FROM estadisticas_usuario WHERE id_usuario = @id";
                using var cmdEstadisticas = new MySqlCommand(deleteEstadisticasQuery, conn, transaction);
                cmdEstadisticas.Parameters.AddWithValue("@id", id);
                cmdEstadisticas.ExecuteNonQuery();

                // 4. Eliminar referencias en la tabla matches
                string deleteMatchesQuery = "DELETE FROM matches WHERE id_usuario_1 = @id OR id_usuario_2 = @id";
                using var cmdMatches = new MySqlCommand(deleteMatchesQuery, conn, transaction);
                cmdMatches.Parameters.AddWithValue("@id", id);
                cmdMatches.ExecuteNonQuery();

                // 5. Eliminar referencias en la tabla interacciones
                string deleteInteraccionesQuery = "DELETE FROM interacciones WHERE id_usuario_origen = @id OR id_usuario_destino = @id";
                using var cmdInteracciones = new MySqlCommand(deleteInteraccionesQuery, conn, transaction);
                cmdInteracciones.Parameters.AddWithValue("@id", id);
                cmdInteracciones.ExecuteNonQuery();

                // 6. Eliminar referencias en la tabla creditos_interaccion (nombre correcto)
                string deleteCreditosQuery = "DELETE FROM creditos_interaccion WHERE id_usuario = @id";
                using var cmdCreditos = new MySqlCommand(deleteCreditosQuery, conn, transaction);
                cmdCreditos.Parameters.AddWithValue("@id", id);
                cmdCreditos.ExecuteNonQuery();

                // 7. Eliminar referencias en la tabla bloqueos
                string deleteBloquesQuery = "DELETE FROM bloqueos WHERE id_usuario_origen = @id OR id_usuario_bloqueado = @id";
                using var cmdBloqueos = new MySqlCommand(deleteBloquesQuery, conn, transaction);
                cmdBloqueos.Parameters.AddWithValue("@id", id);
                cmdBloqueos.ExecuteNonQuery();

                // 8. Eliminar el usuario
                string deleteUsuarioQuery = "DELETE FROM usuarios WHERE id = @id";
                using var cmdUsuario = new MySqlCommand(deleteUsuarioQuery, conn, transaction);
                cmdUsuario.Parameters.AddWithValue("@id", id);
                cmdUsuario.ExecuteNonQuery();

                // 9. Finalmente, si tenemos un id_cuenta, eliminar la cuenta asociada
                if (idCuenta > 0)
                {
                    string deleteCuentaQuery = "DELETE FROM cuentas WHERE id = @idCuenta";
                    using var cmdCuenta = new MySqlCommand(deleteCuentaQuery, conn, transaction);
                    cmdCuenta.Parameters.AddWithValue("@idCuenta", idCuenta);
                    cmdCuenta.ExecuteNonQuery();
                }

                // Confirmar la transacción
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Revertir la transacción en caso de error
                transaction.Rollback();
                throw new Exception($"Error al eliminar el usuario: {ex.Message}");
            }
        }
    }
}
