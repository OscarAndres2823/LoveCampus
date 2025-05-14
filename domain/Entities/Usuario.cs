using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCampus.domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? Edad { get; set; }
        public string? Genero { get; set; }public string? Carrera { get; set; }
        public string? FrasePerfil { get; set; }
        public int? IdCiudad { get; set; } 
    }
}