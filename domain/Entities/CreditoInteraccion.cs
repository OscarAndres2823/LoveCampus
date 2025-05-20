using System;

namespace LoveCampus.domain.Entities
{
    public class CreditoInteraccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public int CreditosDisponibles { get; set; }
        public int CreditosUsados { get; set; }
        public int CreditosMaximosDiarios { get; set; } = 10; // Límite diario de likes

        public bool TieneCreditosDisponibles()
        {
            return CreditosDisponibles > 0;
        }

        public void UsarCredito()
        {
            if (TieneCreditosDisponibles())
            {
                CreditosDisponibles--;
                CreditosUsados++;
            }
        }

        public void ReiniciarCreditos()
        {
            CreditosDisponibles = CreditosMaximosDiarios;
            CreditosUsados = 0;
        }
    }
}
