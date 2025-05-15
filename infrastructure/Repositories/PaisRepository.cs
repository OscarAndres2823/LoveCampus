using MySql.Data.MySqlClient;
using infrastructure.Mysql;
using System.Collections.Generic;
using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;

namespace LoveCampus.infrastructure.Mysql.Repositories
{
    public class PaisRepository : IPaisRepository
    {
        public void CrearPais(Pais pais)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "INSERT INTO pais (nombre, id_pais) VALUES (@nombre, @pais)";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", pais.Nombre);
            cmd.ExecuteNonQuery();
        }
        public Pais ObtenerPaisPorId(int id)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "SELECT * FROM pais WHERE id_pais = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Pais
                {
                    Id = reader.GetInt32("id_pais"),
                    Nombre = reader.GetString("nombre")
                };
            }
            return null;
        }

        public List<Pais> ObtenerTodos()
        {
            var lista = new List<Pais>();
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "SELECT * FROM pais";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Pais
                {
                    Id = reader.GetInt32("id_pais"),
                    Nombre = reader.GetString("nombre")
                });
            }
            return lista;
        }

        public void ActualizarPais(Pais pais)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "UPDATE pais SET nombre = @nombre WHERE id_pais = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", pais.Nombre);
            cmd.Parameters.AddWithValue("@id", pais.Id);
        }

        public void EliminarPais(int id)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "DELETE FROM pais WHERE id_pais = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }        
    }
}