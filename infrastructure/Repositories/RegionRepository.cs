using MySql.Data.MySqlClient;
using infrastructure.Mysql;
using System.Collections.Generic;
using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;

namespace LoveCampus.infrastructure.Mysql.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        
        public void CrearRegion(Region region)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "INSERT INTO region (nombre, id_pais) VALUES (@nombre, @pais)";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", region.Nombre);
            cmd.Parameters.AddWithValue("@pais", region.IdPais);
            cmd.ExecuteNonQuery();
        }

        public Region ObtenerRegionPorId(int id)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "SELECT * FROM region WHERE id = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Region
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    IdPais = reader.GetInt32("id_pais")
                };
            }
            return null;
        }

        public List<Region> ObtenerTodos()
        {
            var lista = new List<Region>();
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "SELECT * FROM region";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Region
                {
                    Id = reader.GetInt32("id"),
                    Nombre = reader.GetString("nombre"),
                    IdPais = reader.GetInt32("id_pais")
                });
            }
            return lista;
        }

        public void ActualizarRegion(Region region)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "UPDATE region SET nombre = @nombre, id_pais = @pais WHERE id = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", region.Nombre);
            cmd.Parameters.AddWithValue("@pais", region.IdPais);
            cmd.Parameters.AddWithValue("@id", region.Id);
            cmd.ExecuteNonQuery();
        }

        public void EliminarRegion(int id)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "DELETE FROM region WHERE id = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}