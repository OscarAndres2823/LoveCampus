using System;
using System.Collections.Generic;
using System.Linq;
using LoveCampus.domain.Ports;
using LoveCampus.domain.Entities;

namespace LoveCampus.domain.Services
{
    public class EstadisticaService
    {
        private readonly IInteraccionRepository _interaccionRepo;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IMatchRepository _matchRepo;

        public EstadisticaService(IInteraccionRepository interaccionRepo, IUsuarioRepository usuarioRepo, IMatchRepository matchRepo)
        {
            _interaccionRepo = interaccionRepo ?? throw new ArgumentNullException(nameof(interaccionRepo));
            _usuarioRepo = usuarioRepo ?? throw new ArgumentNullException(nameof(usuarioRepo));
            _matchRepo = matchRepo ?? throw new ArgumentNullException(nameof(matchRepo));
        }

        public void MostrarEstadisticas()
        {
            // Obtener datos
            var interacciones = _interaccionRepo.ObtenerTodos();
            var usuarios = _usuarioRepo.ObtenerTodos();
            var matches = _matchRepo.ObtenerTodos();

            // Top 3 usuarios con más likes recibidos
            var topLikes = interacciones
                .Where(i => i.TipoInteraccion.Equals("LIKE", StringComparison.OrdinalIgnoreCase))
                .GroupBy(i => i.UsuarioIdDestino)
                .Select(g => new { UsuarioId = g.Key, Likes = g.Count() })
                .OrderByDescending(x => x.Likes)
                .Take(3)
                .ToList();

            // Top 3 usuarios con más matches
            var topMatches = matches
                .SelectMany(m => new[] { m.Usuario1Id, m.Usuario2Id })
                .GroupBy(id => id)
                .Select(g => new { UsuarioId = g.Key, Matches = g.Count() })
                .OrderByDescending(x => x.Matches)
                .Take(3)
                .ToList();

            Console.Clear();
            Console.WriteLine("📊 Estadísticas del sistema:\n");

            Console.WriteLine("💖 Top 3 usuarios con más Likes recibidos:");
            if (topLikes.Any())
            {
                foreach (var l in topLikes)
                {
                    var usuario = usuarios.FirstOrDefault(u => u.Id == l.UsuarioId);
                    if (usuario != null)
                        Console.WriteLine($"- {usuario.Nombre}: {l.Likes} likes");
                    else
                        Console.WriteLine($"- Usuario con ID {l.UsuarioId}: {l.Likes} likes");
                }
            }
            else
            {
                Console.WriteLine("No hay likes registrados.");
            }

            Console.WriteLine("\n💞 Top 3 usuarios con más Matches:");
            if (topMatches.Any())
            {
                foreach (var m in topMatches)
                {
                    var usuario = usuarios.FirstOrDefault(u => u.Id == m.UsuarioId);
                    if (usuario != null)
                        Console.WriteLine($"- {usuario.Nombre}: {m.Matches} matches");
                    else
                        Console.WriteLine($"- Usuario con ID {m.UsuarioId}: {m.Matches} matches");
                }
            }
            else
            {
                Console.WriteLine("No hay matches registrados.");
            }

            Console.WriteLine("\nPresione cualquier tecla para volver...");
            Console.ReadKey();
        }
    }
}
