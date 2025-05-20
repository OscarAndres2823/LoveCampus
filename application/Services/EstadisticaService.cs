using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using System;
using System.Collections.Generic;

namespace LoveCampus.application.Services
{
    public class EstadisticaService
    {
        private readonly IEstadisticaUsuarioRepository _estadisticaRepo;

        public EstadisticaService(IEstadisticaUsuarioRepository estadisticaRepo)
        {
            _estadisticaRepo = estadisticaRepo;
        }

        public void MostrarEstadisticas()
        {
            try
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("📈 ESTADÍSTICAS GENERALES DE LOVECAMPUS");
                Console.WriteLine("=======================================\n");
                Console.ResetColor();
                
                try
                {
                    // Mostrar top 3 usuarios por likes recibidos
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("🔝 TOP USUARIOS CON MÁS LIKES RECIBIDOS:");
                    Console.ResetColor();
                    var topLikesRecibidos = _estadisticaRepo.ObtenerTopUsuariosPorLikesRecibidos(3);
                    MostrarLista(topLikesRecibidos, "Likes Recibidos");
                }
                catch (Exception)
                {
                    Console.WriteLine("No hay datos disponibles sobre likes recibidos.");
                }
                
                try
                {
                    // Mostrar top 3 usuarios por matches
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n🏆 TOP USUARIOS CON MÁS MATCHES:");
                    Console.ResetColor();
                    var topMatches = _estadisticaRepo.ObtenerTopUsuariosPorMatches(3);
                    MostrarLista(topMatches, "Matches");
                }
                catch (Exception)
                {
                    Console.WriteLine("No hay datos disponibles sobre matches.");
                }
                
                try
                {
                    // Mostrar top 3 usuarios por likes dados
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n❤️  TOP USUARIOS QUE MÁS DAN LIKES:");
                    Console.ResetColor();
                    var topLikesDados = _estadisticaRepo.ObtenerTopUsuariosPorLikesDados(3);
                    MostrarLista(topLikesDados, "Likes Dados");
                }
                catch (Exception)
                {
                    Console.WriteLine("No hay datos disponibles sobre likes dados.");
                }
                
                Console.WriteLine("\n\n❤️  Presione cualquier tecla para continuar...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al mostrar estadísticas: {ex.Message}");
                Console.WriteLine($"Detalles: {ex.StackTrace}");
                Console.ReadKey();
            }
        }

        private void MostrarLista(List<EstadisticasUsuario> lista, string tipo)
        {
            if (lista == null || lista.Count == 0)
            {
                Console.WriteLine("No hay datos disponibles.");
                return;
            }

            // Crear una tabla simple para mostrar los datos
            Console.WriteLine("\n+" + new string('-', 40) + "+" + new string('-', 10) + "+");
            Console.WriteLine($"| {"Usuario",-38} | {tipo,-8} |");
            Console.WriteLine("+" + new string('-', 40) + "+" + new string('-', 10) + "+");

            int posicion = 1;
            foreach (var estadistica in lista)
            {
                var cantidad = tipo switch
                {
                    "Likes Recibidos" => estadistica.TotalLikesRecibidos,
                    "Matches" => estadistica.TotalMatches,
                    "Likes Dados" => estadistica.TotalLikesDados,
                    _ => 0
                };

                string medallaEmoji = posicion switch
                {
                    1 => "🥇 ", // Medalla de oro
                    2 => "🥈 ", // Medalla de plata
                    3 => "🥉 ", // Medalla de bronce
                    _ => "   "
                };

                string nombreUsuario = estadistica.NombreUsuario;
                if (nombreUsuario.Length > 35) nombreUsuario = nombreUsuario.Substring(0, 32) + "...";

                Console.WriteLine($"| {medallaEmoji}{nombreUsuario,-35} | {cantidad,8} |");
                posicion++;
            }

            Console.WriteLine("+" + new string('-', 40) + "+" + new string('-', 10) + "+");
        }
    }
}
