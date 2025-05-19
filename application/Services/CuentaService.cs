public class CuentaService
{
    private readonly CuentaRepository _cuentaRepo;

    public CuentaService(CuentaRepository cuentaRepo)
    {
        _cuentaRepo = cuentaRepo;
    }

    public Cuenta RegistrarCuenta(string email, string contraseña)
    {
        var existente = _cuentaRepo.ObtenerPorEmail(email);
        if (existente != null) throw new Exception("Ya existe una cuenta con ese email.");

        var nuevaCuenta = new Cuenta { Email = email, Contraseña = contraseña };
        int id = _cuentaRepo.RegistrarCuenta(nuevaCuenta);
        nuevaCuenta.Id = id;
        return nuevaCuenta;
    }

    public Cuenta IniciarSesion(string email, string contraseña)
    {
        return _cuentaRepo.IniciarSesion(email, contraseña);
    }
}
