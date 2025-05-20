using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using System.Collections.Generic;
using System.Linq;

namespace LoveCampus.domain.Repositories
{
    public class EstadisticaUsuarioRepository : IEstadisticaUsuarioRepository
    {
        // Lista simulada en memoria
        private readonly List<EstadisticasUsuario> _estadisticasUsuarios = new List<EstadisticasUsuario>();

        public List<EstadisticasUsuario> ObtenerTopUsuariosPorLikesRecibidos(int top)
        {
            return _estadisticasUsuarios
                .OrderByDescending(e => e.TotalLikesRecibidos)
                .Take(top)
                .ToList();
        }

        public List<EstadisticasUsuario> ObtenerTopUsuariosPorMatches(int top)
        {
            return _estadisticasUsuarios
                .OrderByDescending(e => e.TotalMatches)
                .Take(top)
                .ToList();
        }

        public List<EstadisticasUsuario> ObtenerTopUsuariosPorLikesDados(int top)
        {
            return _estadisticasUsuarios
                .OrderByDescending(e => e.TotalLikesDados)
                .Take(top)
                .ToList();
        }

        // Método opcional si quieres agregar dinámicamente
        public void AgregarEstadistica(EstadisticasUsuario estadistica)
        {
            _estadisticasUsuarios.Add(estadistica);
        }
    }
}
