using LoveCampus.domain.Ports;
using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.application.Services
{
    public class RegionService
    {
        private readonly IRegionRepository _repositorio;
        public RegionService(IRegionRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void RegistrarRegion(Region region)
        {
            _repositorio.CrearRegion(region);
        }

        public List<Region> ListarRegiones()
        {
            return _repositorio.ObtenerTodos();
        }

        public Region ObtenerPorId(int id)
        {
            return _repositorio.ObtenerRegionPorId(id);
        }
    }
}