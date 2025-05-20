using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace LoveCampus.infrastructure.Mysql
{
    public class CreditoInteraccionRepository : ICreditoInteraccionRepository
    {
        public CreditoInteraccionRepository()
        {
        }

        public CreditoInteraccion? ObtenerPorUsuario(int usuarioId)
        {
            // Implementación simulada para desarrollo
            return new CreditoInteraccion
            {
                Id = 1,
                UsuarioId = usuarioId,
                Fecha = DateTime.Now,
                CreditosDisponibles = 10,
                CreditosUsados = 0,
                CreditosMaximosDiarios = 10
            };
        }

        public void Agregar(CreditoInteraccion credito)
        {
            // Implementación simulada para desarrollo
            Console.WriteLine($"Agregando créditos para usuario {credito.UsuarioId}");
        }

        public void Actualizar(CreditoInteraccion credito)
        {
            // Implementación simulada para desarrollo
            Console.WriteLine($"Actualizando créditos para usuario {credito.UsuarioId}");
        }

        public IEnumerable<CreditoInteraccion> ObtenerTodosCreditos()
        {
            // Implementación simulada para desarrollo
            var creditos = new List<CreditoInteraccion>();
            
            // Agregamos algunos créditos de ejemplo
            creditos.Add(new CreditoInteraccion
            {
                Id = 1,
                UsuarioId = 1,
                Fecha = DateTime.Now.AddDays(-1),
                CreditosDisponibles = 5,
                CreditosUsados = 5,
                CreditosMaximosDiarios = 10
            });
            
            creditos.Add(new CreditoInteraccion
            {
                Id = 2,
                UsuarioId = 2,
                Fecha = DateTime.Now,
                CreditosDisponibles = 8,
                CreditosUsados = 2,
                CreditosMaximosDiarios = 10
            });
            
            return creditos;
        }
    }
}
