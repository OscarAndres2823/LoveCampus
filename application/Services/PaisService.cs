using LoveCampus.domain.Ports;
using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.application.Services
{
    public class PaisService
    {
        private readonly IPaisRepository _repositorio;

        public PaisService(IPaisRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void RegistrarPais(Pais pais)
        {
            _repositorio.CrearPais(pais);
        }

        public List<Pais> ListarPais()
        {
            return _repositorio.ObtenerTodos();
        }

        public Pais BuscarPorId(int id)
        {
            return _repositorio.ObtenerPaisPorId(id);
        }
    }
}
