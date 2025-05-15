using LoveCampus.domain.Ports;
using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.application.Services
{
    public class CiudadService
    {
        private readonly ICiudadRepository _repositorio;

        public CiudadService(ICiudadRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void RegistrarCiudad(Ciudad ciudad)
        {
            _repositorio.CrearCiudad(ciudad);
        }

        public List<Ciudad> ListarCiudades()
        {
            return _repositorio.ObtenerTodos();
        }

        public Ciudad BuscarPorId(int id)
        {
            return _repositorio.ObtenerCiudadPorId(id);
        }
    }
}