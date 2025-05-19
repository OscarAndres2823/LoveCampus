using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.domain.Ports
{
    public interface IMatchRepository
    {
        void CrearMatch(Match match);
        List<Match> ObtenerTodos();
    }
}
