using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LoveCampus.domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public string FrasePerfil { get; set; } = string.Empty;
        public int IdCiudad { get; set; }
        public int IdCuenta { get; set; }
        public List<string> Intereses { get; set; } = new List<string>();

        public void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
                throw new ArgumentException("El nombre es requerido");

            if (Edad < 18 || Edad > 100)
                throw new ArgumentException("La edad debe estar entre 18 y 100 años");

            if (!new[] { "Masculino", "Femenino", "Otro" }.Contains(Genero, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException("Género no válido");

            if (string.IsNullOrWhiteSpace(Carrera))
                throw new ArgumentException("La carrera es requerida");

            if (FrasePerfil.Length > 200)
                throw new ArgumentException("La frase de perfil no puede exceder 200 caracteres");
        }

        public string FormatearNombre()
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Nombre.ToLower());
        }

        public string FormatearEdad()
        {
            return Edad.ToString("N0", CultureInfo.CurrentCulture);
        }
    }
}