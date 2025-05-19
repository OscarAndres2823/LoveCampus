public class Interaccion
{
    public int Id { get; set; }
    public int UsuarioIdOrigen { get; set; }
    public int UsuarioIdDestino { get; set; }
    public string TipoInteraccion { get; set; } // "like" o "dislike"
    public DateTime FechaInteraccion { get; set; }
}
