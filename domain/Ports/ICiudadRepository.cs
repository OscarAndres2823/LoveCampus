using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.domain.Ports
{
    public interface ICiudadRepository
    {
        void CrearCiudad(Ciudad ciudad);
        Ciudad ObtenerCiudadPorId(int id);
        List<Ciudad> ObtenerTodos();
        void ActualizarCiudad(Ciudad ciudad);
        void EliminarCiudad(int id);
    }
}