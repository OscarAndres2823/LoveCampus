using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoveCampus.application.Services
{
    public class MatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IInteraccionRepository _interaccionRepository;

        public MatchService(IMatchRepository matchRepository, IInteraccionRepository interaccionRepository)
        {
            _matchRepository = matchRepository;
            _interaccionRepository = interaccionRepository;
        }

        public bool CrearMatch(int usuario1Id, int usuario2Id)
        {
            if (_matchRepository.ExisteMatch(usuario1Id, usuario2Id))
                return false;

            return _matchRepository.CrearMatch(usuario1Id, usuario2Id);
        }

        public List<Match> ObtenerMatchesPorUsuario(int usuarioId)
        {
            return _matchRepository.ObtenerTodos()
                .Where(m => m.Usuario1Id == usuarioId || m.Usuario2Id == usuarioId)
                .ToList();
        }

        public List<Match> ObtenerTodosLosMatches()
        {
            return _matchRepository.ObtenerTodos();
        }

        public void VerificarMatch(int usuarioId)
        {
            var interacciones = _interaccionRepository.ObtenerInteraccionesPorUsuario(usuarioId);
            var usuariosQueMeDieronLike = interacciones
                .Where(i => i.TipoInteraccion == "LIKE" && i.UsuarioIdDestino == usuarioId)
                .Select(i => i.UsuarioIdOrigen)
                .Distinct();

            foreach (var usuario in usuariosQueMeDieronLike)
            {
                // Verificar si yo le di like a este usuario
                var interaccionPropia = interacciones
                    .FirstOrDefault(i => i.TipoInteraccion == "LIKE" && i.UsuarioIdDestino == usuario);

                if (interaccionPropia != null)
                {
                    // Crear match si no existe
                    CrearMatch(usuarioId, usuario);
                }
            }
        }

        public List<(int IdUsuario, int CantidadMatches)> ObtenerUsuariosConMasMatches()
        {
            var matches = _matchRepository.ObtenerTodos();
            var usuariosConMatches = matches
                .SelectMany(m => new[] { m.Usuario1Id, m.Usuario2Id })
                .GroupBy(id => id)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => (IdUsuario: g.Key, CantidadMatches: g.Count()))
                .ToList();

            return usuariosConMatches;
        }
    }
}
