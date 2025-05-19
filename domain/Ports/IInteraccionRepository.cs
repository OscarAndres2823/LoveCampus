namespace LoveCampus.domain.Ports
{
    public interface IInteraccionRepository
    {
        void AgregarInteraccion(Interaccion interaccion);
        List<Interaccion> ObtenerInteraccionesPorUsuario(int usuarioId);
        List<Interaccion> ObtenerTodos();
        bool ExisteLikeMutuo(int usuarioOrigen, int usuarioDestino);
    }

}