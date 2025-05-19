using MySql.Data.MySqlClient;
using System;
using infrastructure.Mysql; // Asegúrate de importar tu Singleton
using LoveCampus.domain.Entities;         // Si tienes una carpeta para modelos

public class CuentaRepository
{
    public int RegistrarCuenta(Cuenta cuenta)
    {
        using var conn = ConexionSingleton.ObtenerNuevaConexion();
        string query = "INSERT INTO cuentas (email, contraseña) VALUES (@correo, @pass)";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@correo", cuenta.Email);
        cmd.Parameters.AddWithValue("@pass", cuenta.Contraseña); 

        cmd.ExecuteNonQuery();
        return (int)cmd.LastInsertedId;
    }

    public Cuenta IniciarSesion(string email, string contraseña)
    {
        using var conn = ConexionSingleton.ObtenerNuevaConexion();
        string query = "SELECT * FROM cuentas WHERE email = @correo AND contraseña = @pass";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@correo", email);
        cmd.Parameters.AddWithValue("@pass", contraseña);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Cuenta
            {
                Id = reader.GetInt32("id"),
                Email = reader.GetString("email"),
                Contraseña = reader.GetString("contraseña")
            };
        }
        return null;
    }

    public Cuenta ObtenerPorEmail(string email)
    {
        using var conn = ConexionSingleton.ObtenerNuevaConexion();
        string query = "SELECT * FROM cuentas WHERE email = @correo";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@correo", email);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Cuenta
            {
                Id = reader.GetInt32("id"),
                Email = reader.GetString("email"),
                Contraseña = reader.GetString("contraseña")
            };
        }
        return null;
    }
}
