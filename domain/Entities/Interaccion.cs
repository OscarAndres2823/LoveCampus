namespace LoveCampus.domain.Entities
{
    public class Interaccion
    {
        public int Id { get; set; }
        public int UsuarioIdOrigen { get; set; }
        public int UsuarioIdDestino { get; set; }
        public string TipoInteraccion { get; set; } // "LIKE" o "DISLIKE"
        public DateTime FechaInteraccion { get; set; }
    }
}
