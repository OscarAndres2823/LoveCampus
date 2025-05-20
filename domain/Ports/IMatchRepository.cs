using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.domain.Ports
{
    public interface IMatchRepository
    {
        bool CrearMatch(int idUsuario1, int idUsuario2);
        List<Match> ObtenerTodos();
        List<Match> ObtenerMatchesPorUsuario(int usuarioId);
        bool ExisteMatch(int usuario1Id, int usuario2Id);
    }
}
