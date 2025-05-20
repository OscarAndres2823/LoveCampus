using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using LoveCampus.domain.Ports;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UI
{
    public class UIInteraccion
    {
        private readonly InteraccionService _servicio;
        private readonly UsuarioService _usuarioService;

        public UIInteraccion(InteraccionService servicio, UsuarioService usuarioService)
        {
            _servicio = servicio;
            _usuarioService = usuarioService;
        }

        public void MostrarMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("❤️  Interacciones");
            Console.ResetColor();

            Console.Write("Ingrese su ID de usuario: ");
            if (!int.TryParse(Console.ReadLine(), out int idUsuario))
            {
                Console.WriteLine("❌ ID inválido.");
                Console.ReadKey();
                return;
            }

            // Verificar si el usuario existe
            var usuario = _usuarioService.BuscarPorId(idUsuario);
            if (usuario == null)
            {
                Console.WriteLine("❌ Usuario no encontrado.");
                Console.ReadKey();
                return;
            }

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"❤️  Menú de Interacciones - Usuario: {usuario.Nombre}");
                Console.WriteLine("\n1. 💝 Ver perfiles disponibles");
                Console.WriteLine("2. 💌 Ver mis interacciones");
                Console.WriteLine("3. 🔙 Volver al menú principal");
                Console.ResetColor();

                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        MostrarPerfilesDisponibles(idUsuario);
                        break;
                    case "2":
                        MostrarMisInteracciones(idUsuario);
                        break;
                    case "3":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("❌ Opción inválida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void MostrarPerfilesDisponibles(int idUsuario)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💝 Perfiles Disponibles");
            Console.ResetColor();

            var perfiles = _servicio.ObtenerPerfilesDisponibles(idUsuario).ToList();
            
            if (perfiles.Count == 0)
            {
                Console.WriteLine("\nNo hay perfiles disponibles en este momento.");
                Console.ReadKey();
                return;
            }

            // Mostrar perfiles disponibles
            for (int i = 0; i < perfiles.Count; i++)
            {
                var perfil = perfiles[i];
                Console.WriteLine($"\n{i + 1}. ID: {perfil.Id} - {perfil.Nombre} ({perfil.Edad} años) - {perfil.Carrera}");
                Console.WriteLine($"   \"{perfil.FrasePerfil}\"");
            }

            Console.Write("\nIngrese el número del perfil al que desea dar like (0 para volver): ");
            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion > 0 && seleccion <= perfiles.Count)
            {
                var perfilSeleccionado = perfiles[seleccion - 1];
                if (_servicio.DarLike(idUsuario, perfilSeleccionado.Id))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n✅ Has dado like a {perfilSeleccionado.Nombre}!");
                    Console.ResetColor();
                }
            }
            Console.ReadKey();
        }

        private void MostrarMisInteracciones(int idUsuario)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("💌 Mis Interacciones");
            Console.ResetColor();

            var interacciones = _servicio.ObtenerInteraccionesDeUsuario(idUsuario);
            
            if (interacciones.Count == 0)
            {
                Console.WriteLine("\nNo has realizado ninguna interacción aún.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nHas dado like a {interacciones.Count} usuarios:");
            
            foreach (var interaccion in interacciones)
            {
                var usuarioDestino = _usuarioService.BuscarPorId(interaccion.UsuarioIdDestino);
                string nombreDestino = usuarioDestino?.Nombre ?? $"Usuario ID: {interaccion.UsuarioIdDestino}";
                
                Console.WriteLine($"- {nombreDestino} (Fecha: {interaccion.FechaInteraccion.ToString("dd/MM/yyyy HH:mm")})");
            }
            
            Console.ReadKey();
        }
    }
}
