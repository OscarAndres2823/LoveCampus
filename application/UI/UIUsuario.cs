using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using System;

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
                Console.WriteLine("0. Salir");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Registrar();
                        break;
                    case 2:
                        MostrarTodos();
                        break;
                }

            } while (opcion != 0);
        }

        private void Registrar()
        {
            var usuario = new Usuario();
            Console.Write("Nombre: ");
            usuario.Nombre = Console.ReadLine();
            Console.Write("Edad: ");
            usuario.Edad = int.Parse(Console.ReadLine());
            Console.Write("Género: ");
            usuario.Genero = Console.ReadLine();
            Console.Write("Carrera: ");
            usuario.Carrera = Console.ReadLine();
            Console.Write("Frase perfil: ");
            usuario.FrasePerfil = Console.ReadLine();
            Console.Write("ID Ciudad: ");
            usuario.IdCiudad = int.Parse(Console.ReadLine());

            _servicio.RegistrarUsuario(usuario);
            Console.WriteLine("Usuario registrado.");
            Console.ReadKey();
        }

        private void MostrarTodos()
        {
            var usuarios = _servicio.ListarUsuarios();
            foreach (var u in usuarios)
            {
                Console.WriteLine($"ID: {u.Id} | Nombre: {u.Nombre} | Edad: {u.Edad} | Género: {u.Genero}");
            }
            Console.ReadKey();
        }
    }
}
