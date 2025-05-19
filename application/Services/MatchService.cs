using LoveCampus.infrastructure.Mysql.Repositories;
using System.Collections.Generic;

namespace LoveCampus.application.Services
{
    public class MatchService
    {
        private readonly MatchRepository _matchRepository;

        public MatchService(MatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public bool CrearMatch(int idUsuario1, int idUsuario2)
        {
            return _matchRepository.CrearMatch(idUsuario1, idUsuario2);
        }

        public List<(int, int)> ObtenerMatchesDeUsuario(int idUsuario)
        {
            return _matchRepository.ObtenerMatchesDeUsuario(idUsuario);
        }

        public bool HayMatchMutuo(int idUsuario1, int idUsuario2)
        {
            return _matchRepository.HayMatchMutuo(idUsuario1, idUsuario2);
        }
    }
}
