using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using System;
using System.Collections.Generic;

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
                Console.WriteLine("=== LOVE CAMPUS ===");
                Console.WriteLine("1. Registrar usuario");
                Console.WriteLine("2. Ver usuarios");
                Console.WriteLine("3. Editar usuario");
                Console.WriteLine("4. Eliminar usuario");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            Registrar();
                            break;
                        case 2:
                            MostrarTodos();
                            break;
                        case 3:
                            Editar();
                            break;
                        case 4:
                            Eliminar();
                            break;
                        case 0:
                            Console.WriteLine("Saliendo del programa...");
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }

        private void Registrar()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR USUARIO ===");
            
            var usuario = new Usuario();
            Console.Write("Nombre: ");
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
                Console.WriteLine("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
            }
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void MostrarTodos()
        {
            Console.Clear();
            Console.WriteLine("=== LISTA DE USUARIOS ===");
            
            try
            {
                // Cambio: Usar ListarUsuarios en lugar de ObtenerTodos
                List<Usuario> usuarios = _servicio.ListarUsuarios();
                
                if (usuarios == null || usuarios.Count == 0)
                {
                    Console.WriteLine("No hay usuarios registrados.");
                }
                else
                {
                    Console.WriteLine("ID\tNombre\t\tEdad\tGénero\tCarrera\t\tFrase Perfil\t\tID Ciudad");
                    Console.WriteLine("-----------------------------------------------------------------------------------");
                    
                    foreach (var usuario in usuarios)
                    {
                        Console.WriteLine($"{usuario.Id}\t{usuario.Nombre,-15}\t{usuario.Edad}\t{usuario.Genero,-8}\t{usuario.Carrera,-15}\t{usuario.FrasePerfil,-20}\t{usuario.IdCiudad}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener usuarios: {ex.Message}");
            }
            
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void Editar()
        {
            Console.Clear();
            Console.WriteLine("=== EDITAR USUARIO ===");
            
            Console.Write("Ingrese el ID del usuario a editar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    // Cambio: Usar BuscarPorId en lugar de ObtenerPorId
                    Usuario usuario = _servicio.BuscarPorId(id);
                    
                    if (usuario != null)
                    {
                        Console.WriteLine($"Editando usuario: {usuario.Nombre}");
                        
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
                        
                        // Error: No existe un método para actualizar usuario en UsuarioService
                        // Necesitamos agregar este método al servicio
                        Console.WriteLine("Error: La función de actualización no está implementada en el servicio.");
                        Console.WriteLine("Por favor, implemente el método ActualizarUsuario en UsuarioService.");
                    }
                    else
                    {
                        Console.WriteLine($"No se encontró un usuario con ID {id}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al editar usuario: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private void Eliminar()
        {
            Console.Clear();
            Console.WriteLine("=== ELIMINAR USUARIO ===");
            
            Console.Write("Ingrese el ID del usuario a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    // Cambio: Usar BuscarPorId en lugar de ObtenerPorId
                    Usuario usuario = _servicio.BuscarPorId(id);
                    
                    if (usuario != null)
                    {
                        Console.WriteLine($"¿Está seguro de eliminar al usuario {usuario.Nombre}? (S/N)");
                        string confirmacion = Console.ReadLine()?.ToUpper() ?? "N";
                        
                        if (confirmacion == "S")
                        {
                            // Error: No existe un método para eliminar usuario en UsuarioService
                            // Necesitamos agregar este método al servicio
                            Console.WriteLine("Error: La función de eliminación no está implementada en el servicio.");
                            Console.WriteLine("Por favor, implemente el método EliminarUsuario en UsuarioService.");
                        }
                        else
                        {
                            Console.WriteLine("Operación cancelada.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No se encontró un usuario con ID {id}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al eliminar usuario: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
