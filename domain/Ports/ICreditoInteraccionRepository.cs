using LoveCampus.domain.Entities;

namespace LoveCampus.domain.Ports
{
    public interface ICreditoInteraccionRepository
    {
        CreditoInteraccion? ObtenerPorUsuario(int usuarioId);
        void Agregar(CreditoInteraccion credito);
        void Actualizar(CreditoInteraccion credito);
        IEnumerable<CreditoInteraccion> ObtenerTodosCreditos();
    }
}
