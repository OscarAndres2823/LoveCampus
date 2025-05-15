using MySql.Data.MySqlClient;
using infrastructure.Mysql;
using System.Collections.Generic;
using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;

namespace LoveCampus.infrastructure.Mysql.Repositories
{
    public class CiudadRepository : ICiudadRepository
    {
        public void CrearCiudad(Ciudad ciudad)
        {
            using var conn =  ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "INSERT INTO ciudad (nombre, id_region) VALUES (@nobre, @region)";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", ciudad.Nombre);
            cmd.Parameters.AddWithValue("@region", ciudad.IdRegion);
            cmd.ExecuteNonQuery();
        }

        public Ciudad ObtenerCiudadPorId(int id)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "SELECT * FROM ciudad WHERE id = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Ciudad
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    IdRegion = reader.GetInt32("id_region")
                };
            }
            return null;
        }

        public List<Ciudad> ObtenerTodos()
        {
            var lista = new List<Ciudad>();
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "SELECT * FROM ciudad";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Ciudad
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    IdRegion = reader.GetInt32("id_region")
                });
            }
            return lista;
        }

        public void ActualizarCiudad(Ciudad ciudad)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "UPDATE ciudad SET nombre = @nombre, id_region = @region WHERE id = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", ciudad.Nombre);
            cmd.Parameters.AddWithValue("@region", ciudad.IdRegion);
            cmd.Parameters.AddWithValue("@id", ciudad.Id);
        }

        public void EliminarCiudad(int id)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "DELETE FROM ciudad WHERE id = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}