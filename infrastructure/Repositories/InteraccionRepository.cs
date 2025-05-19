using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCampus.infrastructure.Repositories
{
    public class InteraccionRepository
    {
        public interface IInteraccionRepository
        {
            void AgregarInteraccion(Interaccion interaccion);
            List<Interaccion> ObtenerInteraccionesPorUsuario(int idUsuario);
            bool ExisteLikeMutuo(int usuario1, int usuario2);
        }

    }
}