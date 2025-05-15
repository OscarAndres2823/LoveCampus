using LoveCampus.domain.Ports;
using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repositorio;

        public UsuarioService(IUsuarioRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            _repositorio.CrearUsuario(usuario);
        }

        public List<Usuario> ListarUsuarios()
        {
            return _repositorio.ObtenerTodos();
        }

        public Usuario BuscarPorId(int id)
        {
            return _repositorio.ObtenerUsuarioPorId(id);
        }
    }
}
