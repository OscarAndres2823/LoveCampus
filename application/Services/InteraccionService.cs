using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoveCampus.application.Services
{
    public class InteraccionService
    {
        private readonly IInteraccionRepository _interaccionRepo;
        private readonly ICreditoInteraccionRepository _creditoRepo;

        public InteraccionService(IInteraccionRepository interaccionRepo, ICreditoInteraccionRepository creditoRepo)
        {
            _interaccionRepo = interaccionRepo;
            _creditoRepo = creditoRepo;
        }

        public bool DarLike(int usuarioId, int usuarioObjetivoId)
        {
            try
            {
                var credito = _creditoRepo.ObtenerPorUsuario(usuarioId);

                if (credito == null)
                {
                    credito = new CreditoInteraccion
                    {
                        UsuarioId = usuarioId,
                        Fecha = DateTime.Now,
                        CreditosDisponibles = 10
                    };
                    _creditoRepo.Agregar(credito);
                }

                if (credito.CreditosDisponibles <= 0)
                {
                    Console.WriteLine("❌ No tienes créditos disponibles para dar likes hoy. Regresa mañana!");
                    return false;
                }

                if (_interaccionRepo.ObtenerInteraccionesPorUsuario(usuarioId)
                    .Any(i => i.TipoInteraccion == "LIKE" && i.UsuarioIdDestino == usuarioObjetivoId))
                {
                    Console.WriteLine("❌ Ya has dado like a este usuario");
                    return false;
                }

                var interaccion = new Interaccion
                {
                    UsuarioIdOrigen = usuarioId,
                    UsuarioIdDestino = usuarioObjetivoId,
                    TipoInteraccion = "LIKE",
                    FechaInteraccion = DateTime.Now
                };

                _interaccionRepo.AgregarInteraccion(interaccion);
                credito.CreditosDisponibles--;
                credito.CreditosUsados++;
                _creditoRepo.Actualizar(credito);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al dar like: {ex.Message}");
                return false;
            }
        }

        public void ReiniciarCreditosDiarios()
        {
            var creditos = _creditoRepo.ObtenerTodosCreditos();
            foreach (var credito in creditos)
            {
                if (credito.Fecha.Date < DateTime.Now.Date)
                {
                    credito.CreditosDisponibles = credito.CreditosMaximosDiarios;
                    credito.CreditosUsados = 0;
                    credito.Fecha = DateTime.Now;
                    _creditoRepo.Actualizar(credito);
                }
            }
        }

        public IEnumerable<Usuario> ObtenerPerfilesDisponibles(int usuarioId)
        {
            try
            {
                // Obtener todos los usuarios disponibles en la base de datos
                var usuarioRepo = new LoveCampus.infrastructure.Mysql.Repositories.UsuarioRepository();
                var todosLosUsuarios = usuarioRepo.ObtenerTodos();
                
                // Obtener los usuarios a los que ya se les dio like
                var likesPropios = _interaccionRepo.ObtenerInteraccionesPorUsuario(usuarioId)
                    .Where(i => i.TipoInteraccion == "LIKE" && i.UsuarioIdOrigen == usuarioId)
                    .Select(i => i.UsuarioIdDestino)
                    .ToList();
                
                Console.WriteLine($"Usuarios totales: {todosLosUsuarios.Count}");
                Console.WriteLine($"Likes dados: {likesPropios.Count()}");
                
                // Filtrar los usuarios que no son el usuario actual y a los que no se les ha dado like
                var perfilesDisponibles = todosLosUsuarios
                    .Where(u => u.Id != usuarioId)
                    .Where(u => !likesPropios.Contains(u.Id))
                    .OrderBy(u => u.Id)
                    .ToList();
                
                Console.WriteLine($"Perfiles disponibles: {perfilesDisponibles.Count}");
                
                return perfilesDisponibles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener perfiles disponibles: {ex.Message}");
                return new List<Usuario>();
            }
        }

        public List<Interaccion> ObtenerInteraccionesDeUsuario(int usuarioId)
        {
            return _interaccionRepo.ObtenerInteraccionesPorUsuario(usuarioId)
                .Where(i => i.TipoInteraccion == "LIKE")
                .ToList();
        }
    }
}