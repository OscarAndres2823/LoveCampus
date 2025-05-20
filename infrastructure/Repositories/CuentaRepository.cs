using MySql.Data.MySqlClient;
using System;
using infrastructure.Mysql;
using LoveCampus.domain.Entities;

public class CuentaRepository
{
    private readonly MySqlConnection _conexion;

    public CuentaRepository()
    {
        _conexion = ConexionSingleton.ObtenerNuevaConexion();
    }

    // Método para obtener la estructura de la tabla cuentas
    public void MostrarEstructuraTabla()
    {
        try
        {
            // Asegurar que la conexión esté abierta
            if (_conexion.State != System.Data.ConnectionState.Open)
                _conexion.Open();
                
            Console.WriteLine("\n📃 ESTRUCTURA DE LA TABLA CUENTAS:");
            Console.WriteLine("+" + new string('-', 40) + "+" + new string('-', 20) + "+");
            Console.WriteLine($"| {"COLUMNA",-38} | {"TIPO",-18} |");
            Console.WriteLine("+" + new string('-', 40) + "+" + new string('-', 20) + "+");
            
            string query = "DESCRIBE cuentas";
            using var cmd = new MySqlCommand(query, _conexion);
            using var reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                string columnName = reader.GetString("Field");
                string columnType = reader.GetString("Type");
                Console.WriteLine($"| {columnName,-38} | {columnType,-18} |");
            }
            
            Console.WriteLine("+" + new string('-', 40) + "+" + new string('-', 20) + "+");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener estructura de tabla: {ex.Message}");
        }
    }

    public int RegistrarCuenta(Cuenta cuenta)
    {
        try
        {
            // Asegurar que la conexión esté abierta
            if (_conexion.State != System.Data.ConnectionState.Open)
                _conexion.Open();
            
            // Imprimir información de depuración
            Console.WriteLine($"Registrando cuenta con email: {cuenta.Email}");
            
            // Basado en la estructura de la tabla cuentas en love.sql, solo necesitamos email y contraseña
            string query = "INSERT INTO cuentas (email, contraseña) VALUES (@correo, @pass)";
            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@correo", cuenta.Email);
            cmd.Parameters.AddWithValue("@pass", cuenta.Contraseña);
            cmd.ExecuteNonQuery();
            
            int id = (int)cmd.LastInsertedId;
            Console.WriteLine($"Cuenta registrada con ID: {id}");
            return id;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar cuenta: {ex.Message}");
            throw; // Mantenemos el throw para que se maneje en el servicio
        }
    }

    public Cuenta? ObtenerPorEmail(string email)
    {
        try
        {
            // Asegurar que la conexión esté abierta
            if (_conexion.State != System.Data.ConnectionState.Open)
                _conexion.Open();
                
            // Imprimir información de depuración
            Console.WriteLine($"Buscando cuenta con email: {email}");
            
            string query = "SELECT * FROM cuentas WHERE email = @email";
            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@email", email);
            using var reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                var cuenta = new Cuenta
                {
                    Id = reader.GetInt32("id"),
                    Email = reader.GetString("email"),
                    Contraseña = reader.GetString("contraseña")
                    // No intentamos obtener el ID de usuario porque no existe en la tabla cuentas
                };
                
                // Imprimir información de depuración
                Console.WriteLine($"Cuenta encontrada: ID={cuenta.Id}, Email={cuenta.Email}");
                return cuenta;
            }
            
            Console.WriteLine("No se encontró ninguna cuenta con ese email");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener cuenta: {ex.Message}");
            return null; // Cambiar para que no lance excepción y solo retorne null
        }
    }
}
