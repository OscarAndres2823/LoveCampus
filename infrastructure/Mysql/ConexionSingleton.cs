using System;
using MySql.Data.MySqlClient;

namespace LoveCampus.infrastructure.Mysql
{
    public class ConexionSingleton
    {
        private static ConexionSingleton? _instancia;
        private readonly string _connectionString;
        private MySqlConnection? _conexion;

        // Constructor privado para evitar la creación de instancias externas
        private ConexionSingleton(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Método para obtener la instancia única
        public static ConexionSingleton Instancia(string connectionString)
        {
            // Crear la instancia si no existe
            _instancia ??= new ConexionSingleton(connectionString);
            return _instancia;
        }

        // Método para obtener la conexión
        public MySqlConnection ObtenerConexion()
        {
            if (_conexion == null)
            {
                _conexion = new MySqlConnection(_connectionString);
            }
            return _conexion;
        }
    }
}
