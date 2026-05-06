

namespace DevKickstart.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }

    public Usuario(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

            Id = Guid.NewGuid();
            Nombre = nombre;
    }
}