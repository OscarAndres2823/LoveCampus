using LoveCampus.domain.Entities;
using System.Collections.Generic;

namespace LoveCampus.domain.Ports
{
    public interface IUsuarioRepository
    {
        void CrearUsuario(Usuario usuario);
        Usuario ObtenerUsuarioPorId(int id);
        List<Usuario> ObtenerTodos();
        void ActualizarUsuario(Usuario usuario);
        void EliminarUsuario(int id);
    }
}
