using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveCampus.domain.Entities
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdRegion { get; set; }
    }
}