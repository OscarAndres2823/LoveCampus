using LoveCampus.application.Services;
using System;

namespace UI
{
    public class UIMatch
    {
        private readonly MatchService _matchService;

        public UIMatch(MatchService matchService)
        {
            _matchService = matchService;
        }

        public void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("🎯 Menú de Match");
            Console.WriteLine("1. Crear Match");
            Console.WriteLine("2. Ver matches de un usuario");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    CrearMatch();
                    break;
                case "2":
                    MostrarMatches();
                    break;
                default:
                    Console.WriteLine("❌ Opción no válida");
                    break;
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        private void CrearMatch()
        {
            Console.Write("Ingrese el ID del usuario 1: ");
            if (!int.TryParse(Console.ReadLine(), out int id1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
                Console.ResetColor();
                return;
            }

            Console.Write("Ingrese el ID del usuario 2: ");
            if (!int.TryParse(Console.ReadLine(), out int id2))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ ID inválido. Debe ser un número entero.");
                Console.ResetColor();
                return;
            }

            if (_matchService.CrearMatch(id1, id2))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✅ Match creado con éxito");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️ Ya existe un match o hubo un error");
            }

            Console.ResetColor();
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }


        private void MostrarMatches()
        {
            Console.Write("Ingrese el ID del usuario: ");
            int id = int.Parse(Console.ReadLine());

            var matches = _matchService.ObtenerMatchesDeUsuario(id);
            Console.WriteLine($"💞 Matches del usuario {id}:");

            foreach (var (u1, u2) in matches)
                Console.WriteLine($"Match entre {u1} y {u2}");
        }
    }
}
