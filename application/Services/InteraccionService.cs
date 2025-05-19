using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using System;
using System.Collections.Generic;

namespace LoveCampus.application.Services
{
    public class InteraccionService
    {
        private readonly IInteraccionRepository _interaccionRepo;
        private readonly MatchService _matchService;

        public InteraccionService(IInteraccionRepository interaccionRepo, MatchService matchService)
        {
            _interaccionRepo = interaccionRepo;
            _matchService = matchService;
        }

        public void Interactuar(int usuarioOrigen, int usuarioDestino, bool esLike)
        {
            if (usuarioOrigen == usuarioDestino)
            {
                Console.WriteLine("❌ No puedes interactuar contigo mismo.");
                return;
            }

            // Convertir el bool a string para el tipo_interaccion en DB
            string tipoInteraccion = esLike ? "LIKE" : "DISLIKE";

            var interaccion = new Interaccion
            {
                UsuarioIdOrigen = usuarioOrigen,
                UsuarioIdDestino = usuarioDestino,
                TipoInteraccion = tipoInteraccion,
                FechaInteraccion = DateTime.Now
            };

            try
            {
                _interaccionRepo.AgregarInteraccion(interaccion);

                if (esLike && _interaccionRepo.ExisteLikeMutuo(usuarioOrigen, usuarioDestino))
                {
                    _matchService.CrearMatch(usuarioOrigen, usuarioDestino);
                    Console.WriteLine("🎉 ¡Match generado!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al guardar la interacción: " + ex.Message);
            }
        }

        public List<Interaccion> ObtenerInteraccionesDeUsuario(int usuarioId)
        {
            return _interaccionRepo.ObtenerInteraccionesPorUsuario(usuarioId);
        }
    }
}
