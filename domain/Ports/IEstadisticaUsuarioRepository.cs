using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.domain.Ports
{
    public interface IEstadisticaUsuarioRepository
    {
        List<EstadisticasUsuario> ObtenerTopUsuariosPorLikesRecibidos(int top);
        List<EstadisticasUsuario> ObtenerTopUsuariosPorMatches(int top);
        List<EstadisticasUsuario> ObtenerTopUsuariosPorLikesDados(int top);
    }
}
