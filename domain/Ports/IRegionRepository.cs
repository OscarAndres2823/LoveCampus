using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.domain.Ports
{
    public interface IRegionRepository
    {
        void CrearRegion(Region region);
        Region ObtenerRegionPorId(int id);
        List<Region> ObtenerTodos();
        void ActualizarRegion(Region region);   
        void EliminarRegion(int id);
    }
}