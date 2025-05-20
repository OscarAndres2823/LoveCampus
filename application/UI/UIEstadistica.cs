using LoveCampus.application.Services;
using System;

namespace LoveCampus.application.UI
{
    public class UIEstadistica
    {
        private readonly EstadisticaService _estadisticaService;

        public UIEstadistica(EstadisticaService estadisticaService)
        {
            _estadisticaService = estadisticaService;
        }

        public void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║         📊 Estadísticas de LoveCampus        ║");
                Console.WriteLine("╠══════════════════════════════════════════════╣");
                Console.WriteLine("║  1. 📊 Ver estadísticas generales            ║");
                Console.WriteLine("║  2. 🔙 Volver al menú principal             ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.Write("\n🎯 Ingrese una opción: ");
                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        _estadisticaService.MostrarEstadisticas();
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("❤️  Opción inválida");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
