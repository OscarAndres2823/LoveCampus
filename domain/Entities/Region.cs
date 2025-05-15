using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCampus.domain.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdPais { get; set; }
    }
}