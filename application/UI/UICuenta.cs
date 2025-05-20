using LoveCampus.application.Services;
using LoveCampus.domain.Entities;
using System;

namespace LoveCampus.application.UI
{
    public class UICuenta
    {
        private readonly CuentaService _cuentaService;
        private readonly UsuarioService _usuarioService;

        public UICuenta(CuentaService cuentaService, UsuarioService usuarioService)
        {
            _cuentaService = cuentaService;
            _usuarioService = usuarioService;
        }

        public Usuario? IniciarSesion()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n🔒 Iniciar Sesión\n");
            Console.ResetColor();

            Console.Write("👤 Correo electrónico: ");
            string correo = Console.ReadLine() ?? string.Empty;

            Console.Write("🔑 Contraseña: ");
            string contrasena = Console.ReadLine() ?? string.Empty;

            try
            {
                var cuenta = _cuentaService.IniciarSesion(correo, contrasena);
                if (cuenta != null)
                {
                    // Buscar el usuario por el ID de cuenta, no por el ID de usuario
                    var usuario = _usuarioService.BuscarPorIdCuenta(cuenta.Id);
                    if (usuario != null)
                    {
                        Console.WriteLine("\n✅ Sesión iniciada correctamente!");
                        Console.ReadKey();
                        return usuario;
                    }
                    else
                    {
                        Console.WriteLine("\n❌ No se encontró un usuario asociado a esta cuenta.");
                    }
                }
                Console.WriteLine("❌ Credenciales inválidas");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al iniciar sesión: {ex.Message}");
                Console.ReadKey();
            }
            return null;
        }

        public Usuario? RegistrarUsuario()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n📝 Registro de Usuario\n");
            Console.ResetColor();

            try
            {
                var usuario = new Usuario();
                Console.Write("👤 Nombre: ");
                usuario.Nombre = Console.ReadLine() ?? string.Empty;

                Console.Write("📧 Correo electrónico: ");
                string correo = Console.ReadLine() ?? string.Empty;

                Console.Write("🔑 Contraseña: ");
                string contrasena = Console.ReadLine() ?? string.Empty;
                
                Console.Write("👨‍🎓 Edad: ");
                if (!int.TryParse(Console.ReadLine(), out int edad))
                {
                    Console.WriteLine("❌ La edad debe ser un número");
                    Console.ReadKey();
                    return null;
                }
                usuario.Edad = edad;
                
                Console.Write("⚧️ Género (Masculino/Femenino/Otro): ");
                usuario.Genero = Console.ReadLine() ?? "Otro";
                
                Console.Write("🎓 Carrera: ");
                usuario.Carrera = Console.ReadLine() ?? string.Empty;
                
                Console.Write("💬 Frase de perfil: ");
                usuario.FrasePerfil = Console.ReadLine() ?? string.Empty;
                
                // Asignar una ciudad por defecto (ID 1)
                usuario.IdCiudad = 1;

                if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena) ||
                    string.IsNullOrEmpty(usuario.Genero) || string.IsNullOrEmpty(usuario.Carrera))
                {
                    Console.WriteLine("❌ Todos los campos son obligatorios");
                    Console.ReadKey();
                    return null;
                }

                // Verificar si el correo ya existe
                var cuentaExistente = _cuentaService.ObtenerPorCorreo(correo);
                if (cuentaExistente != null)
                {
                    Console.WriteLine("❌ El correo electrónico ya está registrado");
                    Console.ReadKey();
                    return null;
                }

                // Primero crear la cuenta
                var cuenta = new Cuenta
                {
                    Email = correo,
                    Contraseña = contrasena
                };

                try
                {
                    // Crear la cuenta primero
                    _cuentaService.CrearCuenta(cuenta);
                    Console.WriteLine($"Cuenta creada con ID: {cuenta.Id}");
                    
                    // Asignar el ID de la cuenta al usuario
                    usuario.IdCuenta = cuenta.Id;
                    
                    // Ahora crear el usuario con la referencia a la cuenta
                    _usuarioService.RegistrarUsuario(usuario);
                    
                    if (usuario.Id <= 0)
                    {
                        Console.WriteLine("\n❌ Error: No se pudo registrar el usuario correctamente.");
                        Console.ReadKey();
                        return null;
                    }
                    
                    Console.WriteLine($"Usuario registrado con ID: {usuario.Id}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error al crear la cuenta o el usuario: {ex.Message}");
                    Console.ReadKey();
                    return null;
                }
                
                Console.WriteLine($"Cuenta creada con ID: {cuenta.Id}");
                Console.WriteLine($"Credenciales: Email={correo}, Contraseña={contrasena}");

                Console.WriteLine("\n✅ Usuario registrado exitosamente!");
                Console.ReadKey();
                return usuario;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al registrar usuario: {ex.Message}");
                Console.ReadKey();
                return null;
            }
        }

    }
}
