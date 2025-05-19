using MySql.Data.MySqlClient;
using System;

namespace infrastructure.Mysql
{
    public static class ConexionSingleton
    {
        private static readonly string cadenaConexion = "server=localhost;user=root;password=123456;database=love;";

        public static MySqlConnection ObtenerNuevaConexion()
        {
            var nuevaConexion = new MySqlConnection(cadenaConexion);
            nuevaConexion.Open();
            return nuevaConexion;
        }
    }
}
