using MySql.Data.MySqlClient;

namespace infrastructure.Mysql
{
    public class ConexionSingleton
    {
        private static MySqlConnection conexion;

        private ConexionSingleton() { }

        public static MySqlConnection ObtenerConexion()
        {
            if (conexion == null)
            {
                string cadena = "server=localhost;user=root;password=123456;database=love;";
                conexion = new MySqlConnection(cadena);
            }
            return conexion;
        }
    }
}
