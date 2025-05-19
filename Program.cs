using System;
using LoveCampus.infrastructure.Mysql.Repositories;
using LoveCampus.application.Services;
using UI;
using LoveCampus.infrastructure.Repositories;
using LoveCampus.domain.Services;

class Program
{
    static void Main(string[] args)
    {
        // Aquí creamos servicios, y ellos crean internamente sus repositorios.
        var usuarioService = new UsuarioService(new UsuarioRepository());
        var ciudadService = new CiudadService(new CiudadRepository());
        var regionService = new RegionService(new RegionRepository());
        var paisService = new PaisService(new PaisRepository());
        var cuentaService = new CuentaService(new CuentaRepository());
        var matchService = new MatchService(new MatchRepository());
        var interaccionService = new InteraccionService(new InteraccionRepository());

        // El servicio de estadísticas recibe los servicios para operar con ellos.
        var estadisticaService = new EstadisticaService(interaccionRepository, usuarioRepository, matchRepository);
        // Interfaces de Usuario (UI)
        var uiUsuario = new UIUsuario(usuarioService);
        var uiCiudad = new UICiudad(ciudadService);
        var uiRegion = new UIRegion(regionService);
        var uiPais = new UIPais(paisService);
        var uiCuenta = new UICuenta(cuentaService);
        var uiMatch = new UIMatch(matchService);
        var uiInteraccion = new UIInteraccion(interaccionService);
        var uiEstadisticas = new UIEstadisticas(estadisticaService);

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║         💖  Bienvenido a LoveCampus  💖     ║");
            Console.WriteLine("╠══════════════════════════════════════════════╣");
            Console.WriteLine("║  1. 💌 Iniciar sesión / Registrarse          ║");
            Console.WriteLine("║  2. 🧑‍🎓 Gestión de Usuarios                   ║");
            Console.WriteLine("║  3. 🏙️  Gestión de Ciudades                  ║");
            Console.WriteLine("║  4. 🗺️  Gestión de Regiones                  ║");
            Console.WriteLine("║  5. 🌍 Gestión de Países                     ║");
            Console.WriteLine("║  6. 💞 Gestión de Matches                    ║");
            Console.WriteLine("║  7. 🔄 Gestión de Interacciones              ║");
            Console.WriteLine("║  8. 📊 Mostrar Estadísticas                   ║");
            Console.WriteLine("║  9. ❌ Salir del sistema                     ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.Write("\n🎯 Ingrese una opción: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    uiCuenta.MostrarMenu();
                    break;
                case "2":
                    uiUsuario.MostrarMenu();
                    break;
                case "3":
                    uiCiudad.MostrarMenu();
                    break;
                case "4":
                    uiRegion.MostrarMenu();
                    break;
                case "5":
                    uiPais.MostrarMenu();
                    break;
                case "6":
                    uiMatch.MostrarMenu();
                    break;
                case "7":
                    uiInteraccion.MostrarMenu();
                    break;
                case "8":
                    uiEstadisticas.MostrarMenu();
                    break;
                case "9":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("❤️  Opción inválida");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
