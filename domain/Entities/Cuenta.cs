using System;

public class Cuenta
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string Email { get; set; } = string.Empty;
    private string _contraseña;
    
    public string Contraseña
    {
        get => _contraseña;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("La contraseña no puede estar vacía");
            
            _contraseña = value;
        }
    }

    public Cuenta()
    {
    }

    public Cuenta(string email, string contraseña)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Contraseña = contraseña ?? throw new ArgumentNullException(nameof(contraseña));
    }

    public void Validar()
    {
        if (string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException("El email es requerido");

        if (!IsValidEmail(Email))
            throw new ArgumentException("El formato del email no es válido");

        if (string.IsNullOrWhiteSpace(Contraseña))
            throw new ArgumentException("La contraseña es requerida");

        if (Contraseña.Length < 8)
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres");
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
}
