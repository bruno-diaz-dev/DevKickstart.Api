

namespace DevKickstart.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }

    public string PasswordHash { get; private set; } = string.Empty;

    public Usuario(string nombre, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("La contraseña no puede estar vacía.", nameof(passwordHash));

        Id = Guid.NewGuid();
        Nombre = nombre;
        PasswordHash = passwordHash;
    }
}