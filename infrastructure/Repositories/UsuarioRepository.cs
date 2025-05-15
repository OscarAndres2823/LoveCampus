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
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "INSERT INTO usuarios (nombre, genero, edad, carrera, correo, id_ciudad) VALUES (@nombre, @genero, @edad, @carrera, @fraseperfil, @ciudad)";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@genero", usuario.Genero);
            cmd.Parameters.AddWithValue("@edad", usuario.Edad);
            cmd.Parameters.AddWithValue("@carrera", usuario.Carrera);
            cmd.Parameters.AddWithValue("@fraseperfil", usuario.FrasePerfil);
            cmd.Parameters.AddWithValue("@ciudad", usuario.IdCiudad);
            cmd.ExecuteNonQuery();
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
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
                    FrasePerfil = reader.GetString("frase perfil"),
                    IdCiudad = reader.GetInt32("id_ciudad")
                };
            }
            return null;
        }

        public List<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            using var conn = ConexionSingleton.ObtenerConexion();
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
                    FrasePerfil = reader.GetString("frase perfil"),
                    IdCiudad = reader.GetInt32("id_ciudad")
                });
            }
            return lista;
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "UPDATE usuarios SET nombre=@nombre, genero=@genero, edad=@edad, carrera=@carrera, correo=@correo, id_ciudad=@ciudad WHERE id=@id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@genero", usuario.Genero);
            cmd.Parameters.AddWithValue("@edad", usuario.Edad);
            cmd.Parameters.AddWithValue("@carrera", usuario.Carrera);
            cmd.Parameters.AddWithValue("@fraseperfil", usuario.FrasePerfil);
            cmd.Parameters.AddWithValue("@ciudad", usuario.IdCiudad);
            cmd.Parameters.AddWithValue("@id", usuario.Id);
            cmd.ExecuteNonQuery();
        }

        public void EliminarUsuario(int id)
        {
            using var conn = ConexionSingleton.ObtenerConexion();
            conn.Open();
            string query = "DELETE FROM usuarios WHERE id = @id";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
