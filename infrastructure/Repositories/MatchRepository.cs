using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoveCampus.infrastructure.Mysql.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private List<Match> _matches;

        public MatchRepository()
        {
            _matches = new List<Match>();
            
            // Datos de ejemplo
            _matches.Add(new Match { Id = 1, Usuario1Id = 1, Usuario2Id = 2 });
            _matches.Add(new Match { Id = 2, Usuario1Id = 1, Usuario2Id = 3 });
        }

        public bool CrearMatch(int idUsuario1, int idUsuario2)
        {
            try
            {
                // Evitar duplicados (match ya existente)
                bool matchExistente = _matches.Any(m => 
                    (m.Usuario1Id == idUsuario1 && m.Usuario2Id == idUsuario2) || 
                    (m.Usuario1Id == idUsuario2 && m.Usuario2Id == idUsuario1));

                if (matchExistente)
                {
                    // El match ya existe
                    return false;
                }

                // Crear nuevo match
                var nuevoMatch = new Match
                {
                    Id = _matches.Count > 0 ? _matches.Max(m => m.Id) + 1 : 1,
                    Usuario1Id = idUsuario1,
                    Usuario2Id = idUsuario2
                };
                
                _matches.Add(nuevoMatch);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear match: {ex.Message}");
                return false;
            }
        }

        public List<Match> ObtenerMatchesPorUsuario(int usuarioId)
        {
            return _matches
                .Where(m => m.Usuario1Id == usuarioId || m.Usuario2Id == usuarioId)
                .ToList();
        }

        public bool ExisteMatch(int usuario1Id, int usuario2Id)
        {
            return _matches.Any(m => 
                (m.Usuario1Id == usuario1Id && m.Usuario2Id == usuario2Id) || 
                (m.Usuario1Id == usuario2Id && m.Usuario2Id == usuario1Id));
        }
        
        public List<Match> ObtenerTodos()
        {
            return _matches.ToList();
        }
    }
}
