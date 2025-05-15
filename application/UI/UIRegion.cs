using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace UI
{
    public class UIRegion
    {
        private readonly RegionService _servicio;

        public UIRegion(RegionService servicio)
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
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("╔═══════════════════════════════════╗");
                Console.WriteLine("║         GESTIÓN DE REGIONES       ║");
                Console.WriteLine("╠═══════════════════════════════════╣");
                Console.WriteLine("║ 1. Registrar nueva región         ║");
                Console.WriteLine("║ 2. Ver todas las regiones         ║");
                Console.WriteLine("║ 3. Buscar región por ID           ║");
                Console.WriteLine("║ 4. Actualizar región              ║");
                Console.WriteLine("║ 5. Eliminar región                ║");
                Console.WriteLine("║ 0. Volver al menú principal       ║");
                Console.WriteLine("╚═══════════════════════════════════╝");
                Console.ResetColor();
                
                Console.Write("\nSeleccione una opción: ");
                
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            RegistrarRegion();
                            break;
                        case 2:
                            MostrarTodasRegiones();
                            break;
                        case 3:
                            BuscarRegionPorId();
                            break;
                        case 4:
                            ActualizarRegion();
                            break;
                        case 5:
                            EliminarRegion();
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

        private void DibujarTabla(List<Region> regiones)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════╦══════════════════════════╦════════════════╗");
            Console.WriteLine("║   ID   ║          NOMBRE          ║     ID PAÍS    ║");
            Console.WriteLine("╠════════╬══════════════════════════╬════════════════╣");
            Console.ResetColor();

            foreach (var region in regiones)
            {
                Console.WriteLine($"║ {region.Id,-6} ║ {region.Nombre,-24} ║ {region.IdPais,-14} ║");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚════════╩══════════════════════════╩════════════════╝");
            Console.ResetColor();
        }

        private void DibujarDetalleRegion(Region region)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║               DETALLES DE LA REGIÓN               ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════╣");
            Console.ResetColor();
            Console.WriteLine($"║ ID:         {region.Id,-39} ║");
            Console.WriteLine($"║ Nombre:     {region.Nombre,-39} ║");
            Console.WriteLine($"║ ID País:    {region.IdPais,-39} ║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        private void RegistrarRegion()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║       REGISTRAR NUEVA REGIÓN      ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            var region = new Region();
            
            Console.Write("\nNombre de la región: ");
            region.Nombre = Console.ReadLine();
            
            Console.Write("ID del país: ");
            if (int.TryParse(Console.ReadLine(), out int idPais))
            {
                region.IdPais = idPais;
            }
            else
            {
                MostrarMensaje("ID de país inválido. Se establecerá como 0.", ConsoleColor.Red);
                region.IdPais = 0;
            }

            try
            {
                _servicio.RegistrarRegion(region);
                MostrarMensaje("¡Región registrada exitosamente!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al registrar la región: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void MostrarTodasRegiones()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║         LISTA DE REGIONES         ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            try
            {
                List<Region> regiones = _servicio.ListarRegiones();
                
                if (regiones == null || regiones.Count == 0)
                {
                    MostrarMensaje("No hay regiones registradas.", ConsoleColor.Yellow);
                }
                else
                {
                    DibujarTabla(regiones);
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al obtener las regiones: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void BuscarRegionPorId()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║       BUSCAR REGIÓN POR ID        ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\nIngrese el ID de la región: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Region region = _servicio.ObtenerPorId(id);
                    
                    if (region != null)
                    {
                        DibujarDetalleRegion(region);
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró una región con ID {id}.", ConsoleColor.Yellow);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al buscar la región: {ex.Message}", ConsoleColor.Red);
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

        private void ActualizarRegion()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║         ACTUALIZAR REGIÓN         ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\nIngrese el ID de la región a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Region region = _servicio.ObtenerPorId(id);
                    
                    if (region != null)
                    {
                        Console.WriteLine("\nDatos actuales:");
                        DibujarDetalleRegion(region);
                        
                        Console.WriteLine("\nIngrese los nuevos datos (deje en blanco para mantener el valor actual):");
                        
                        Console.Write($"Nombre [{region.Nombre}]: ");
                        string nombre = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nombre))
                        {
                            region.Nombre = nombre;
                        }
                        
                        Console.Write($"ID País [{region.IdPais}]: ");
                        string idPaisStr = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(idPaisStr) && int.TryParse(idPaisStr, out int idPais))
                        {
                            region.IdPais = idPais;
                        }
                        
                        // Mostrar mensaje de error ya que el método no existe en el servicio
                        MostrarMensaje("Error: La función de actualización no está implementada en el servicio.\nPor favor, implemente el método ActualizarRegion en RegionService.", ConsoleColor.Red);
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró una región con ID {id}.", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al actualizar la región: {ex.Message}", ConsoleColor.Red);
                }
            }
            else
            {
                MostrarMensaje("ID inválido.", ConsoleColor.Red);
            }
        }

        private void EliminarRegion()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║          ELIMINAR REGIÓN          ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();
            
            Console.Write("\nIngrese el ID de la región a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Region region = _servicio.ObtenerPorId(id);
                    
                    if (region != null)
                    {
                        Console.WriteLine("\nDatos de la región a eliminar:");
                        DibujarDetalleRegion(region);
                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n¿Está seguro de eliminar esta región? (S/N): ");
                        Console.ResetColor();
                        string confirmacion = Console.ReadLine()?.ToUpper() ?? "N";
                        
                        if (confirmacion == "S")
                        {
                            // Mostrar mensaje de error ya que el método no existe en el servicio
                            MostrarMensaje("Error: La función de eliminación no está implementada en el servicio.\nPor favor, implemente el método EliminarRegion en RegionService.", ConsoleColor.Red);
                        }
                        else
                        {
                            MostrarMensaje("Operación cancelada.", ConsoleColor.Yellow);
                        }
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró una región con ID {id}.", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al eliminar la región: {ex.Message}", ConsoleColor.Red);
                }
            }
            else
            {
                MostrarMensaje("ID inválido.", ConsoleColor.Red);
            }
        }
    }
}
