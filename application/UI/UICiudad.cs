using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace UI
{
    public class UICiudad
    {
        private readonly CiudadService _servicio;

        public UICiudad(CiudadService servicio)
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
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔═══════════════════════════════════╗");
                Console.WriteLine("║         GESTIÓN DE CIUDADES       ║");
                Console.WriteLine("╠═══════════════════════════════════╣");
                Console.WriteLine("║ 1. Registrar nueva ciudad         ║");
                Console.WriteLine("║ 2. Ver todas las ciudades         ║");
                Console.WriteLine("║ 3. Buscar ciudad por ID           ║");
                Console.WriteLine("║ 4. Actualizar ciudad              ║");
                Console.WriteLine("║ 5. Eliminar ciudad                ║");
                Console.WriteLine("║ 0. Volver al menú principal       ║");
                Console.WriteLine("╚═══════════════════════════════════╝");
                Console.ResetColor();

                Console.Write("\nSeleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            RegistrarCiudad();
                            break;
                        case 2:
                            MostrarTodasCiudades();
                            break;
                        case 3:
                            BuscarCiudadPorId();
                            break;
                        case 4:
                            ActualizarCiudad();
                            break;
                        case 5:
                            EliminarCiudad();
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

        private void DibujarTabla(List<Ciudad> ciudades)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════╦══════════════════════════╦════════════════╗");
            Console.WriteLine("║   ID   ║          NOMBRE          ║   ID REGIÓN    ║");
            Console.WriteLine("╠════════╬══════════════════════════╬════════════════╣");
            Console.ResetColor();

            foreach (var ciudad in ciudades)
            {
                Console.WriteLine($"║ {ciudad.Id,-6} ║ {ciudad.Nombre,-24} ║ {ciudad.IdRegion,-14} ║");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚════════╩══════════════════════════╩════════════════╝");
            Console.ResetColor();
        }

        private void DibujarDetalleCiudad(Ciudad ciudad)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine("║               DETALLES DE LA CIUDAD               ║");
            Console.WriteLine("╠═══════════════════════════════════════════════════╣");
            Console.ResetColor();
            Console.WriteLine($"║ ID:         {ciudad.Id,-39} ║");
            Console.WriteLine($"║ Nombre:     {ciudad.Nombre,-39} ║");
            Console.WriteLine($"║ ID Región:  {ciudad.IdRegion,-39} ║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚═══════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        private void RegistrarCiudad()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║       REGISTRAR NUEVA CIUDAD      ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            var ciudad = new Ciudad();

            Console.Write("\nNombre de la ciudad: ");
            ciudad.Nombre = Console.ReadLine();

            Console.Write("ID de la región: ");
            if (int.TryParse(Console.ReadLine(), out int idRegion))
            {
                ciudad.IdRegion = idRegion;
            }
            else
            {
                MostrarMensaje("ID de región inválido. Se establecerá como 0.", ConsoleColor.Red);
                ciudad.IdRegion = 0;
            }

            try
            {
                _servicio.RegistrarCiudad(ciudad);
                MostrarMensaje("¡Ciudad registrada exitosamente!", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al registrar la ciudad: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void MostrarTodasCiudades()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║         LISTA DE CIUDADES         ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            try
            {
                List<Ciudad> ciudades = _servicio.ListarCiudades();

                if (ciudades == null || ciudades.Count == 0)
                {
                    MostrarMensaje("No hay ciudades registradas.", ConsoleColor.Yellow);
                }
                else
                {
                    DibujarTabla(ciudades);
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al obtener las ciudades: {ex.Message}", ConsoleColor.Red);
            }
        }

        private void BuscarCiudadPorId()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║       BUSCAR CIUDAD POR ID        ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nIngrese el ID de la ciudad: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Ciudad ciudad = _servicio.BuscarPorId(id);

                    if (ciudad != null)
                    {
                        DibujarDetalleCiudad(ciudad);
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró una ciudad con ID {id}.", ConsoleColor.Yellow);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al buscar la ciudad: {ex.Message}", ConsoleColor.Red);
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

        private void ActualizarCiudad()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║         ACTUALIZAR CIUDAD         ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nIngrese el ID de la ciudad a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Ciudad ciudad = _servicio.BuscarPorId(id);

                    if (ciudad != null)
                    {
                        Console.WriteLine("\nDatos actuales:");
                        DibujarDetalleCiudad(ciudad);

                        Console.WriteLine("\nIngrese los nuevos datos (deje en blanco para mantener el valor actual):");

                        Console.Write($"Nombre [{ciudad.Nombre}]: ");
                        string nombre = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nombre))
                        {
                            ciudad.Nombre = nombre;
                        }

                        Console.Write($"ID Región [{ciudad.IdRegion}]: ");
                        string idRegionStr = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(idRegionStr) && int.TryParse(idRegionStr, out int idRegion))
                        {
                            ciudad.IdRegion = idRegion;
                        }

                        // Mostrar mensaje de error ya que el método no existe en el servicio
                        MostrarMensaje("Error: La función de actualización no está implementada en el servicio.\nPor favor, implemente el método ActualizarCiudad en CiudadService.", ConsoleColor.Red);
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró una ciudad con ID {id}.", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al actualizar la ciudad: {ex.Message}", ConsoleColor.Red);
                }
            }
            else
            {
                MostrarMensaje("ID inválido.", ConsoleColor.Red);
            }
        }


        private void EliminarCiudad()
        {
            Console.Clear();
            MostrarTitulo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════╗");
            Console.WriteLine("║          ELIMINAR CIUDAD          ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
            Console.ResetColor();

            Console.Write("\nIngrese el ID de la ciudad a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                try
                {
                    Ciudad ciudad = _servicio.BuscarPorId(id);

                    if (ciudad != null)
                    {
                        Console.WriteLine("\nDatos de la ciudad a eliminar:");
                        DibujarDetalleCiudad(ciudad);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n¿Está seguro de eliminar esta ciudad? (S/N): ");
                        Console.ResetColor();
                        string confirmacion = Console.ReadLine()?.ToUpper() ?? "N";

                        if (confirmacion == "S")
                        {
                            // Mostrar mensaje de error ya que el método no existe en el servicio
                            MostrarMensaje("Error: La función de eliminación no está implementada en el servicio.\nPor favor, implemente el método EliminarCiudad en CiudadService.", ConsoleColor.Red);
                        }
                        else
                        {
                            MostrarMensaje("Operación cancelada.", ConsoleColor.Yellow);
                        }
                    }
                    else
                    {
                        MostrarMensaje($"No se encontró una ciudad con ID {id}.", ConsoleColor.Yellow);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al eliminar la ciudad: {ex.Message}", ConsoleColor.Red);
                }
            }
            else
            {
                MostrarMensaje("ID inválido.", ConsoleColor.Red);
            }
        }
    }
}
