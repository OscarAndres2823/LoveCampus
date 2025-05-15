using System;
using LoveCampus.infrastructure.Mysql.Repositories;
using LoveCampus.application.Services;
using UI;


class Program {
    static void Main(string[] args) {
        string connectionString = "server=localhost;user=roor; password=123456;database=love";

        //Servicios
        var usuarioService = new UsuarioService(new UsuarioRepository());
        var ciudadService = new CiudadService(new CiudadRepository());
        var regionService = new RegionService(new RegionRepository());
        var paisService = new PaisService(new PaisRepository());

        //Interfaces de Usuario
        var uiUsuario = new UIUsuario(usuarioService);
        var uiCiudad = new UICiudad(ciudadService);
        var uiRegion = new UIRegion(regionService);
        var uiPais = new UIPais(paisService);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("❤️  Bienvenido a LoveCampus ❤️");
            Console.WriteLine("❤️  1. Usuarios");
            Console.WriteLine("❤️  2. Ciudad");
            Console.WriteLine("❤️  3. Region");
            Console.WriteLine("❤️  4. Pais");
            Console.WriteLine("❤️  5. Salir");
            Console.Write("❤️  Ingrese una opción: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    uiUsuario.MostrarMenu();
                    break;
                case "2":
                    uiCiudad.MostrarMenu();
                    break;
                case "3":
                    uiRegion.MostrarMenu();
                    break;
                case "4":
                    uiPais.MostrarMenu();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("❤️  Opción inválida");
                    break;
            }
        }
    }
}