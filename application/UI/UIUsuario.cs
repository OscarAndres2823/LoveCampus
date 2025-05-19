using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace UI
{
    public class UIUsuario
    {
        private readonly UsuarioService _servicio;

        public UIUsuario(UsuarioService servicio)
        {
            _servicio = servicio;
        }

        public void MostrarMenu()
        {
            int opcion;
            do
            {
                Console.Clear();
                MostrarTitulo();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("╔═══════════════════════════════════╗");
                Console.WriteLine("║        GESTIÓN DE USUARIOS        ║");
                Console.WriteLine("╠═══════════════════════════════════╣");
                Console.WriteLine("║ 1. Registrar nuevo usuario        ║");
                Console.WriteLine("║ 2. Ver todos los usuarios         ║");
                Console.WriteLine("║ 3. Buscar usuario por ID          ║");
                Console.WriteLine("║ 4. Actualizar usuario             ║");
                Console.WriteLine("║ 5. Eliminar usuario               ║");
                Console.WriteLine("║ 0. Volver al menú principal       ║");
                Console.WriteLine("╚═══════════════════════════════════╝");
                Console.ResetColor();

                Console.Write("\nSeleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            RegistrarUsuario();
                            break;
                        case 2:
                            MostrarTodosUsuarios();
                            break;
                        case 3:
                            BuscarUsuarioPorId();
                            break;
                        case 4:
                            ActualizarUsuario();
                            break;
                        case 5:
                            EliminarUsuario();
                            break;
                        case 0:
                            MostrarMensaje("Volviendo al menú principal...", ConsoleColor.Yellow);
                            Thread.Sleep(1500);
                            break;
                        default:
                            MostrarMensaje("Opción no válida. Intente nuevamente.", ConsoleColor.Red);
                            break;
                    }
                }
                else
                {
                    MostrarMensaje("Por favor, ingrese un número válido.", ConsoleColor.Red);
                }

            } while (opcion != 0);
        }

        private void MostrarTitulo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ResetColor();
        }

        private void MostrarMensaje(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n" + mensaje);
            Console.ResetColor();
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void DibujarTabla(List<Usuario> usuarios)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════╦══════════════════════╦═══════╦═════════════╦══════════════════════╦══════════════════════════════════════╦════════════╗");
            Console.WriteLine("║   ID   ║        NOMBRE        ║ EDAD  ║   GÉNERO    ║        CARRERA       ║             FRASE PERFIL             ║ ID CIUDAD  ║");
            Console.WriteLine("╠════════╬══════════════════════╬═══════╬═════════════╬══════════════════════╬══════════════════════════════════════╬════════════╣");
            Console.ResetColor();

            foreach (var usuario in usuarios)
            {
                Console.WriteLine($"║ {usuario.Id,-6} ║ {usuario.Nombre,-20} ║ {usuario.Edad,-5} ║ {usuario.Genero,-11} ║ {usuario.Carrera,-20} ║ {usuario.FrasePerfil,-36} ║ {usuario.IdCiudad,-10} ║");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚════════╩══════════════════════╩═══════╩═════════════╩══════════════════════╩══════════════════════════════════════╩════════════╝");
            Console.ResetColor();
        }

        private void DibujarDetalleUsuario(Usuario usuario)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║               DETALLES DEL USUARIO                ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════╣");
            Console.ResetColor();
            Console.WriteLine($"║ ID:           {usuario.Id,-37} ║");
            Console.WriteLine($"║ Nombre:       {usuario.Nombre,-37} ║");
            Console.WriteLine($"║ Edad:         {usuario.Edad,-37} ║");
            Console.WriteLine($"║ Género:       {usuario.Genero,-37} ║");
            Console.WriteLine($"║ Carrera:      {usuario.Carrera,-37} ║");
            Console.WriteLine($"║ Frase Perfil: {usuario.FrasePerfil,-37} ║");
            Console.WriteLine($"║ ID Ciudad:    {usuario.IdCiudad,-37} ║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        private void RegistrarUsuario()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║      REGISTRAR NUEVO USUARIO      ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            var usuario = new Usuario();

            Console.Write("\nNombre: ");
            usuario.Nombre = Console.ReadLine();

            Console.Write("Edad: ");
            if (int.TryParse(Console.ReadLine(), out int edad))
            {
                usuario.Edad = edad;
            }
            else
            {
                Console.WriteLine("Edad inválida. Se establecerá como 0.");
                usuario.Edad = 0;
            }

            Console.Write("Género: ");
            usuario.Genero = Console.ReadLine();

            Console.Write("Carrera: ");
            usuario.Carrera = Console.ReadLine();

            Console.Write("Frase perfil: ");
            usuario.FrasePerfil = Console.ReadLine();

            Console.Write("ID Ciudad: ");
            if (int.TryParse(Console.ReadLine(), out int idCiudad))
            {
                usuario.IdCiudad = idCiudad;
            }
            else
            {
                Console.WriteLine("ID Ciudad inválido. Se establecerá como 0.");
                usuario.IdCiudad = 0;
            }

            try
            {
                _servicio.RegistrarUsuario(usuario);
                MostrarMensaje("¡Usuario registrado exitosamente!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al registrar el usuario: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void MostrarTodosUsuarios()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║         LISTA DE USUARIOS         ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            try
            {
                List<Usuario> usuarios = _servicio.ListarUsuarios();

                if (usuarios == null || usuarios.Count == 0)
                {
                    MostrarMensaje("No hay usuarios registrados.", ConsoleColor.Yellow);
                }
                else
                {
                    DibujarTabla(usuarios);
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al obtener los usuarios: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void BuscarUsuarioPorId()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║       BUSCAR USUARIO POR ID       ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nIngrese el ID del usuario: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Usuario usuario = _servicio.BuscarPorId(id);

                    if (usuario != null)
                    {
                        DibujarDetalleUsuario(usuario);
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró un usuario con ID {id}.", ConsoleColor.Yellow);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al buscar el usuario: {ex.Message}", ConsoleColor.Red);
                    return;
                }
            }
            else
            {
                MostrarMensaje("ID inválido.", ConsoleColor.Red);
                return;
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void ActualizarUsuario()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║        ACTUALIZAR USUARIO         ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nIngrese el ID del usuario a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Usuario usuario = _servicio.BuscarPorId(id);

                    if (usuario != null)
                    {
                        Console.WriteLine("\nDatos actuales:");
                        DibujarDetalleUsuario(usuario);

                        Console.WriteLine("\nIngrese los nuevos datos (deje en blanco para mantener el valor actual):");

                        Console.Write($"Nombre [{usuario.Nombre}]: ");
                        string nombre = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nombre))
                        {
                            usuario.Nombre = nombre;
                        }

                        Console.Write($"Edad [{usuario.Edad}]: ");
                        string edadStr = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(edadStr) && int.TryParse(edadStr, out int edad))
                        {
                            usuario.Edad = edad;
                        }

                        Console.Write($"Género [{usuario.Genero}]: ");
                        string genero = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(genero))
                        {
                            usuario.Genero = genero;
                        }

                        Console.Write($"Carrera [{usuario.Carrera}]: ");
                        string carrera = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(carrera))
                        {
                            usuario.Carrera = carrera;
                        }

                        Console.Write($"Frase perfil [{usuario.FrasePerfil}]: ");
                        string frasePerfil = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(frasePerfil))
                        {
                            usuario.FrasePerfil = frasePerfil;
                        }

                        Console.Write($"ID Ciudad [{usuario.IdCiudad}]: ");
                        string idCiudadStr = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(idCiudadStr) && int.TryParse(idCiudadStr, out int idCiudad))
                        {
                            usuario.IdCiudad = idCiudad;
                        }

                        _servicio.ActualizarUsuario(usuario);
                        MostrarMensaje("Usuario actualizado correctamente.", ConsoleColor.Green);
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró un usuario con ID {id}.", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al actualizar el usuario: {ex.Message}", ConsoleColor.Red);
                }
            }
            else
            {
                MostrarMensaje("ID inválido.", ConsoleColor.Red);
            }
        }

        private void EliminarUsuario()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║         ELIMINAR USUARIO          ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nIngrese el ID del usuario a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Usuario usuario = _servicio.BuscarPorId(id);

                    if (usuario != null)
                    {
                        Console.WriteLine("\nDatos del usuario a eliminar:");
                        DibujarDetalleUsuario(usuario);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n¿Está seguro de eliminar este usuario? (S/N): ");
                        Console.ResetColor();
                        string confirmacion = Console.ReadLine()?.ToUpper() ?? "N";

                        if (confirmacion == "S")
                        {
                            _servicio.EliminarUsuario(id);
                            MostrarMensaje("Usuario eliminado correctamente.", ConsoleColor.Green);
                        }
                        else
                        {
                            MostrarMensaje("Operación cancelada.", ConsoleColor.Yellow);
                        }
                    }
                                        else
                    {
                        MostrarMensaje($"No se encontró un usuario con ID {id}.", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al eliminar el usuario: {ex.Message}", ConsoleColor.Red);
                }
            }
            else
            {
                MostrarMensaje("ID inválido.", ConsoleColor.Red);
            }
        }
    }
}
