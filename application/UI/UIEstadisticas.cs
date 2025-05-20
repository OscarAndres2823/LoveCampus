using LoveCampus.application.Services;
using System;

namespace UI
{
    public class UIEstadistica
    {
        private readonly EstadisticaService _estadisticaService;

        public UIEstadistica(EstadisticaService estadisticaService)
        {
            _estadisticaService = estadisticaService ?? throw new ArgumentNullException(nameof(estadisticaService));
        }

        public void MostrarMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("📊 Menú de Estadísticas\n");
                Console.WriteLine("1. Ver estadísticas generales");
                Console.WriteLine("2. Volver al menú principal");
                Console.Write("\nIngrese opción: ");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        _estadisticaService.MostrarEstadisticas();
                        break;
                    case "2":
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Presione cualquier tecla para intentar de nuevo.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
