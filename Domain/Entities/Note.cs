namespace DevKickstart.Domain.Entities;
public class Nota
{
    public Guid Id { get; private set; }

    public string Titulo { get; private set; }

    public string Contenido { get; private set; }

    public Guid UsuarioId { get; private set; }

    public DateTime FechaCreacion { get; private set; }

    public DateTime FechaActualizacion { get; private set; }

    public Nota(
        string titulo,
        string contenido,
        Guid usuarioId)
    {
        if (string.IsNullOrWhiteSpace(titulo))
        {
            throw new ArgumentException(
                "El titulo no puede estar vacio.",
                nameof(titulo)
            );
        }

        Id = Guid.NewGuid();
        Titulo = titulo;
        Contenido = contenido;
        UsuarioId = usuarioId;
        FechaCreacion = DateTime.UtcNow;
        FechaActualizacion = DateTime.UtcNow;
    }

    public void ActualizarContenido(
        string titulo,
        string contenido)
    {
        Titulo = titulo;
        Contenido = contenido;
        FechaActualizacion = DateTime.UtcNow;
    }
}