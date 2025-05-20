using System;
using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using LoveCampus.application.UI;
using LoveCampus.infrastructure.Mysql.Repositories;
using LoveCampus.domain.Repositories;
using LoveCampus.infrastructure.Repositories;
using LoveCampus.infrastructure.Mysql;
using UI;

namespace LoveCampus
{
    class Program
    {
        private static Usuario? UsuarioActual { get; set; }

        static void Main(string[] args)
        {
            // No verificaremos la disponibilidad de la consola al inicio para evitar errores
            // Simplemente asumiremos que está disponible y manejaremos las excepciones cuando ocurran

            try
            {
                // Inicializar servicios
                // Repositorios
                var usuarioRepository = new UsuarioRepository();
                var ciudadRepository = new CiudadRepository();
                var regionRepository = new RegionRepository();
                var paisRepository = new PaisRepository();
                var cuentaRepository = new CuentaRepository();
                var matchRepository = new MatchRepository();
                var interaccionRepository = new InteraccionRepository();
                var creditoRepository = new CreditoInteraccionRepository();
                var estadisticaRepository = new EstadisticaUsuarioRepository();
                
                // Servicios
                var usuarioService = new UsuarioService(usuarioRepository);
                var ciudadService = new CiudadService(ciudadRepository);
                var regionService = new RegionService(regionRepository);
                var paisService = new PaisService(paisRepository);
                var cuentaService = new CuentaService(cuentaRepository);
                var matchService = new MatchService(matchRepository, interaccionRepository);
                var interaccionService = new InteraccionService(interaccionRepository, creditoRepository);
                var estadisticaService = new EstadisticaService(estadisticaRepository);
                
                // Interfaces de usuario
                var uiUsuario = new UIUsuario(usuarioService);
                var uiCiudad = new UICiudad(ciudadService);
                var uiRegion = new UIRegion(regionService);
                var uiPais = new UIPais(paisService);
                var uiCuenta = new UICuenta(cuentaService, usuarioService);
                var uiMatch = new UIMatch(matchService);
                var uiInteraccion = new UIInteraccion(interaccionService, usuarioService);
                var uiEstadisticas = new UI.UIEstadistica(estadisticaService);

                // Mostrar menú de inicio
                try
                {
                    UsuarioActual = MostrarMenuInicio(uiCuenta);
                    
                    if (UsuarioActual != null)
                    {
                        // Iniciar el ciclo principal del programa
                        MostrarMenuPrincipal(uiUsuario, uiCiudad, uiRegion, uiPais, uiMatch, uiInteraccion, uiEstadisticas);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Error al interactuar con la consola: {ex.Message}");
                    Console.WriteLine("No se puede mostrar la interfaz de usuario en modo consola.");
                    Console.WriteLine("Por favor, ejecute la aplicación en un entorno que soporte consola interactiva.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en el programa: {ex.Message}");
                Console.WriteLine($"Detalles: {ex.StackTrace}");
                
                try
                {
                    // Intentar leer una tecla, pero manejar la excepción si no es posible
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
                catch (InvalidOperationException)
                {
                    // Si no se puede leer una tecla, simplemente continuar
                    Console.WriteLine("No se puede leer entrada de consola. El programa terminará.");
                }
            }
        }

        private static Usuario? MostrarMenuInicio(UICuenta uiCuenta)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("╔══════════════════════════════════════════════╗");
                    Console.WriteLine("║         💖  Bienvenido a LoveCampus  💖     ║");
                    Console.WriteLine("╠══════════════════════════════════════════════╣");
                    Console.WriteLine("║  1. 🔐 Iniciar sesión                       ║");
                    Console.WriteLine("║  2. 📝 Registrarse                           ║");
                    Console.WriteLine("║  3. ❌ Salir del sistema                     ║");
                    Console.WriteLine("╚══════════════════════════════════════════════╝");
                    Console.ResetColor();
                    Console.Write("\n🎯 Ingrese una opción: ");
                    var opcion = Console.ReadLine() ?? "3"; // Si es null, salir del sistema

                    switch (opcion)
                    {
                        case "1":
                            return uiCuenta.IniciarSesion();
                        case "2":
                            return uiCuenta.RegistrarUsuario();
                        case "3":
                            Console.WriteLine("\n👋 ¡Hasta pronto!");
                            return null;
                        default:
                            Console.WriteLine("\n❌ Opción inválida. Presione cualquier tecla para continuar...");
                            try { Console.ReadKey(); } catch (InvalidOperationException) { /* Ignorar */ }
                            break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    // Si hay un error al interactuar con la consola
                    Console.WriteLine($"Error al interactuar con la consola: {ex.Message}");
                    Console.WriteLine("Seleccionando opción por defecto: Salir del sistema");
                    return null;
                }
            }
        }

        private static void MostrarMenuPrincipal(UIUsuario uiUsuario, UICiudad uiCiudad, UIRegion uiRegion, UIPais uiPais, UIMatch uiMatch, UIInteraccion uiInteraccion, UI.UIEstadistica uiEstadisticas)
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("╔══════════════════════════════════════════════╗");
                    Console.WriteLine($"║         💖  Bienvenido {UsuarioActual?.Nombre}  💖     ║");
                    Console.WriteLine("╠══════════════════════════════════════════════╣");
                    Console.WriteLine("║  1. 🧑‍🎓 Gestión de Usuarios                   ║");
                    Console.WriteLine("║  2. 🌙️  Gestión de Ciudades                  ║");
                    Console.WriteLine("║  3. 🗺️  Gestión de Regiones                  ║");
                    Console.WriteLine("║  4. 🌍 Gestión de Países                     ║");
                    Console.WriteLine("║  5. 💞 Gestión de Matches                    ║");
                    Console.WriteLine("║  6. 🔄 Gestión de Interacciones              ║");
                    Console.WriteLine("║  7. 📊 Mostrar Estadísticas                  ║");
                    Console.WriteLine("║  8. 🔐 Cerrar sesión                         ║");
                    Console.WriteLine("╚══════════════════════════════════════════════╝");
                    Console.ResetColor();
                    Console.Write("\n🎯 Ingrese una opción: ");
                    var opcion = Console.ReadLine() ?? "8"; // Si es null, cerrar sesión

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
                            uiMatch.MostrarMenu();
                            break;
                        case "6":
                            uiInteraccion.MostrarMenu();
                            break;
                        case "7":
                            uiEstadisticas.MostrarMenu();
                            break;
                        case "8":
                            return;
                        default:
                            Console.WriteLine("❤️  Opción inválida");
                            try { Console.ReadKey(); } catch (InvalidOperationException) { /* Ignorar */ }
                            break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    // Si hay un error al interactuar con la consola
                    Console.WriteLine($"Error al interactuar con la consola: {ex.Message}");
                    Console.WriteLine("Seleccionando opción por defecto: Salir del sistema");
                    return;
                }
            }
        }
    }
}
