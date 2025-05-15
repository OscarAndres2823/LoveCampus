using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using System;

namespace LoveCampus.application.UI.Ciudad
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
                Console.WriteLine("=== LOVE CAMPUS ===");
                Console.WriteLine("1. Crear Ciudad");
                Console.WriteLine("2. Editar Ciudad");
                Console.WriteLine("3. Eliminar Ciudad");
                Console.WriteLine("4. Listar Ciudades");
                Console.WriteLine("5. Salir");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Crear();
                        break;
                    case 2:
                        Editar();
                        break;
                    case 3:
                        Eliminar();
                        break;
                    case 4:
                        Listar();
                        break;
                    case 5:
                        Console.WriteLine("Saliendo...");
                        break;
                }

            } while (opcion != 5);
        }

        private void Crear()
        {
            var ciudad = new Ciudad();
            Console.WriteLine("Nombre: ");
            ciudad.Nombre = Console.ReadLine();
        }
    }
}