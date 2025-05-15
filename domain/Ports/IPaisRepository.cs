using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.domain.Ports
{
    public interface IPaisRepository
    {
        void CrearPais(Pais pais);
        Pais ObtenerPaisPorId(int id);
        List<Pais> ObtenerTodos();
        void ActualizarPais(Pais pais);
        void EliminarPais(int id);
    }
}