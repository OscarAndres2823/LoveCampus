public class UICuenta
{
    private readonly CuentaService _cuentaService;

    public UICuenta(CuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }

    public void MostrarMenu()
    {
        Console.Clear();
        Console.WriteLine("🧡  1. Registrarse");
        Console.WriteLine("🧡  2. Iniciar sesión");
        Console.Write("🧡  Ingrese una opción: ");
        var opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                Registrar();
                break;
            case "2":
                IniciarSesion();
                break;
            default:
                Console.WriteLine("Opción no válida.");
                break;
        }
    }

    private void Registrar()
    {
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Contraseña: ");
        string pass = Console.ReadLine();

        try
        {
            var cuenta = _cuentaService.RegistrarCuenta(email, pass);
            Console.WriteLine("Cuenta registrada con éxito. ID: " + cuenta.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        Console.ReadKey();
    }

    private void IniciarSesion()
    {
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Contraseña: ");
        string pass = Console.ReadLine();

        var cuenta = _cuentaService.IniciarSesion(email, pass);
        if (cuenta != null)
        {
            Console.WriteLine("Inicio de sesión exitoso. Bienvenido, " + cuenta.Email);
        }
        else
        {
            Console.WriteLine("Credenciales incorrectas.");
        }

        Console.ReadKey();
    }
}
