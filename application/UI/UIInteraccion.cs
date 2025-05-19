using LoveCampus.application.Services;
using LoveCampus.domain.Ports;
using System;
using System.Linq;

namespace LoveCampus.ui
{
    public class UIInteraccion
    {
        private readonly InteraccionService _interaccionService;
        private readonly IUsuarioRepository _usuarioRepo;

        public UIInteraccion(InteraccionService interaccionService, IUsuarioRepository usuarioRepo)
        {
            _interaccionService = interaccionService;
            _usuarioRepo = usuarioRepo;
        }

        public void MostrarMenu()
        {
            Console.Write("Ingrese su ID de usuario: ");
            if (!int.TryParse(Console.ReadLine(), out int idUsuario))
            {
                Console.WriteLine("❌ ID inválido.");
                return;
            }

            var usuarios = _usuarioRepo.ObtenerTodos().Where(u => u.Id != idUsuario).ToList();

            if (!usuarios.Any())
            {
                Console.WriteLine("No hay otros usuarios para mostrar.");
                return;
            }

            foreach (var usuario in usuarios)
            {
                Console.Clear();
                Console.WriteLine($"👤 Nombre: {usuario.Nombre}");
                Console.WriteLine($"🎓 Edad: {usuario.Edad}");
                Console.WriteLine($"📍 Ciudad: {usuario.IdCiudad}");
                Console.Write("💖 (L)ike | ❌ (D)islike | (S)alir: ");

                var opcion = Console.ReadLine()?.Trim().ToUpper();

                if (opcion == "S")
                    break;

                if (opcion != "L" && opcion != "D")
                {
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    continue;
                }

                bool esLike = opcion == "L";

                _interaccionService.Interactuar(idUsuario, usuario.Id, esLike);
            }
        }
    }
}
