using System;
using System.Text.RegularExpressions;
using LoveCampus.domain.Entities;
using LoveCampus.infrastructure.Repositories;

namespace LoveCampus.application.Services
{
    public class CuentaService
    {
        private readonly CuentaRepository _cuentaRepo;

        public CuentaService(CuentaRepository cuentaRepo)
        {
            _cuentaRepo = cuentaRepo;
        }

        public Cuenta CrearCuenta(Cuenta cuenta)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(cuenta.Email))
                throw new ArgumentException("El email es requerido");

            if (!IsValidEmail(cuenta.Email))
                throw new ArgumentException("El formato del email no es válido");

            if (string.IsNullOrWhiteSpace(cuenta.Contraseña))
                throw new ArgumentException("La contraseña es requerida");

            if (cuenta.Contraseña.Length < 8)
                throw new ArgumentException("La contraseña debe tener al menos 8 caracteres");

            var existente = _cuentaRepo.ObtenerPorEmail(cuenta.Email);
            if (existente != null)
                throw new ArgumentException("Ya existe una cuenta con ese email.");

            int id = _cuentaRepo.RegistrarCuenta(cuenta);
            cuenta.Id = id;
            return cuenta;
        }

        public Cuenta? IniciarSesion(string email, string contraseña)
        {
            try
            {
                Console.WriteLine($"Intentando iniciar sesión con email: {email}");
                
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contraseña))
                {
                    Console.WriteLine("Email o contraseña vacíos");
                    throw new ArgumentException("Email y contraseña son requeridos");
                }

                var cuenta = _cuentaRepo.ObtenerPorEmail(email);
                if (cuenta == null)
                {
                    Console.WriteLine($"❌ Cuenta no encontrada para el email: {email}");
                    return null;
                }

                Console.WriteLine($"Contraseña almacenada: '{cuenta.Contraseña}', Contraseña ingresada: '{contraseña}'");
                
                if (cuenta.Contraseña != contraseña)
                {
                    Console.WriteLine("❌ Contraseña incorrecta");
                    return null;
                }

                Console.WriteLine($"Inicio de sesión exitoso para la cuenta con ID: {cuenta.Id}");
                return cuenta;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al iniciar sesión: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return null;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public Cuenta? ObtenerPorCorreo(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es requerido");

            return _cuentaRepo.ObtenerPorEmail(email);
        }
    }
}
