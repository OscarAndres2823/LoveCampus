using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace LoveCampus.infrastructure.Mysql.Repositories
{
    public class MatchRepository
    {
        private readonly string connectionString = "server=localhost;user=root;password=123456;database=love";

        public bool CrearMatch(int idUsuario1, int idUsuario2)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                // Evitar duplicados (match ya existente)
                string checkQuery = "SELECT COUNT(*) FROM matches WHERE (id_usuario_1 = @u1 AND id_usuario_2 = @u2) OR (id_usuario_1 = @u2 AND id_usuario_2 = @u1)";
                using var checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@u1", idUsuario1);
                checkCmd.Parameters.AddWithValue("@u2", idUsuario2);
                var count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    // El match ya existe
                    return false;
                }

                // Insertar nuevo match
                string insertQuery = "INSERT INTO matches (id_usuario_1, id_usuario_2) VALUES (@u1, @u2)";
                using var insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@u1", idUsuario1);
                insertCmd.Parameters.AddWithValue("@u2", idUsuario2);
                insertCmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear match: {ex.Message}");
                return false;
            }
        }

        public List<(int, int)> ObtenerMatchesDeUsuario(int idUsuario)
        {
            var matches = new List<(int, int)>();

            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                string query = @"
                    SELECT id_usuario_1, id_usuario_2 
                    FROM matches 
                    WHERE id_usuario_1 = @idUsuario OR id_usuario_2 = @idUsuario";

                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int u1 = reader.GetInt32("id_usuario_1");
                    int u2 = reader.GetInt32("id_usuario_2");
                    matches.Add((u1, u2));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener matches: {ex.Message}");
            }

            return matches;
        }

        public bool HayMatchMutuo(int idUsuarioOrigen, int idUsuarioDestino)
        {
            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                string query = @"
                    SELECT COUNT(*) FROM interacciones 
                    WHERE id_usuario_origen = @u1 AND id_usuario_destino = @u2 AND tipo_interaccion = 'LIKE'
                    AND EXISTS (
                        SELECT 1 FROM interacciones 
                        WHERE id_usuario_origen = @u2 AND id_usuario_destino = @u1 AND tipo_interaccion = 'LIKE'
                    )";

                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@u1", idUsuarioOrigen);
                cmd.Parameters.AddWithValue("@u2", idUsuarioDestino);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en verificación de match mutuo: {ex.Message}");
                return false;
            }
        }
    }
}
