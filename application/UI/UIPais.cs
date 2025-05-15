using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace UI
{
    public class UIPais
    {
        private readonly PaisService _servicio;

        public UIPais(PaisService servicio)
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
                Console.WriteLine("║          GESTIÓN DE PAÍSES        ║");
                Console.WriteLine("╠═══════════════════════════════════╣");
                Console.WriteLine("║ 1. Registrar nuevo país           ║");
                Console.WriteLine("║ 2. Ver todos los países           ║");
                Console.WriteLine("║ 3. Buscar país por ID             ║");
                Console.WriteLine("║ 4. Actualizar país                ║");
                Console.WriteLine("║ 5. Eliminar país                  ║");
                Console.WriteLine("║ 0. Volver al menú principal       ║");
                Console.WriteLine("╚═══════════════════════════════════╝");
                Console.ResetColor();
                
                Console.Write("\nSeleccione una opción: ");
                
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            RegistrarPais();
                            break;
                        case 2:
                            MostrarTodosPaises();
                            break;
                        case 3:
                            BuscarPaisPorId();
                            break;
                        case 4:
                            ActualizarPais();
                            break;
                        case 5:
                            EliminarPais();
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

        private void DibujarTabla(List<Pais> paises)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════╦══════════════════════════════════════════╗");
            Console.WriteLine("║   ID   ║                  NOMBRE                  ║");
            Console.WriteLine("╠════════╬══════════════════════════════════════════╣");
            Console.ResetColor();

            foreach (var pais in paises)
            {
                Console.WriteLine($"║ {pais.Id,-6} ║ {pais.Nombre,-40} ║");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚════════╩══════════════════════════════════════════╝");
            Console.ResetColor();
        }

        private void DibujarDetallePais(Pais pais)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║                DETALLES DEL PAÍS                  ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════╣");
            Console.ResetColor();
            Console.WriteLine($"║ ID:         {pais.Id,-39} ║");
            Console.WriteLine($"║ Nombre:     {pais.Nombre,-39} ║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        private void RegistrarPais()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║        REGISTRAR NUEVO PAÍS       ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            var pais = new Pais();
            
            Console.Write("\nNombre del país: ");
            pais.Nombre = Console.ReadLine();
            
            try
            {
                _servicio.RegistrarPais(pais);
                MostrarMensaje("¡País registrado exitosamente!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al registrar el país: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void MostrarTodosPaises()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║          LISTA DE PAÍSES          ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            try
            {
                List<Pais> paises = _servicio.ListarPais();
                
                if (paises == null || paises.Count == 0)
                {
                    MostrarMensaje("No hay países registrados.", ConsoleColor.Yellow);
                }
                else
                {
                    DibujarTabla(paises);
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al obtener los países: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void BuscarPaisPorId()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║        BUSCAR PAÍS POR ID         ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\nIngrese el ID del país: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Pais pais = _servicio.BuscarPorId(id);
                    
                    if (pais != null)
                    {
                        DibujarDetallePais(pais);
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró un país con ID {id}.", ConsoleColor.Yellow);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al buscar el país: {ex.Message}", ConsoleColor.Red);
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

        private void ActualizarPais()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║          ACTUALIZAR PAÍS          ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\nIngrese el ID del país a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Pais pais = _servicio.BuscarPorId(id);
                    
                    if (pais != null)
                    {
                        Console.WriteLine("\nDatos actuales:");
                        DibujarDetallePais(pais);
                        
                        Console.WriteLine("\nIngrese los nuevos datos (deje en blanco para mantener el valor actual):");
                        
                        Console.Write($"Nombre [{pais.Nombre}]: ");
                        string nombre = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nombre))
                        {
                            pais.Nombre = nombre;
                        }
                        
                        // Mostrar mensaje de error ya que el método no existe en el servicio
                        MostrarMensaje("Error: La función de actualización no está implementada en el servicio.\nPor favor, implemente el método ActualizarPais en PaisService.", ConsoleColor.Red);
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró un país con ID {id}.", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al actualizar el país: {ex.Message}", ConsoleColor.Red);
                }
            }
            else
            {
                MostrarMensaje("ID inválido.", ConsoleColor.Red);
            }
        }

        private void EliminarPais()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║           ELIMINAR PAÍS           ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\nIngrese el ID del país a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Pais pais = _servicio.BuscarPorId(id);
                    
                    if (pais != null)
                    {
                        Console.WriteLine("\nDatos del país a eliminar:");
                        DibujarDetallePais(pais);
                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n¿Está seguro de eliminar este país? (S/N): ");
                        Console.ResetColor();
                        string confirmacion = Console.ReadLine()?.ToUpper() ?? "N";
                        
                        if (confirmacion == "S")
                        {
                            // Mostrar mensaje de error ya que el método no existe en el servicio
                            MostrarMensaje("Error: La función de eliminación no está implementada en el servicio.\nPor favor, implemente el método EliminarPais en PaisService.", ConsoleColor.Red);
                        }
                        else
                        {
                            MostrarMensaje("Operación cancelada.", ConsoleColor.Yellow);
                        }
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró un país con ID {id}.", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al eliminar el país: {ex.Message}", ConsoleColor.Red);
                }
            }
            else
            {
                MostrarMensaje("ID inválido.", ConsoleColor.Red);
            }
        }
    }
}
