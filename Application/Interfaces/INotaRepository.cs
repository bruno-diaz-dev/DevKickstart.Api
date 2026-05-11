using DevKickstart.Domain.Entities;

namespace DevKickstart.Application.Interfaces;

public interface INotaRepository
{
    Task Guardar(Nota nota);

    Task<Nota?> ObtenerPorId(Guid id);

    Task <IEnumerable<Nota>>
        ObtenerPorUsuario(Guid usuarioId);
}