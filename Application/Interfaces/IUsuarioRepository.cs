using DevKickstart.Domain.Entities;

namespace DevKickstart.Application.Interfaces;

public interface IUsuarioRepository
{
    Task Guardar(Usuario usuario);
    Task<Usuario?> ObtenerPorId(Guid id);

    Task<IEnumerable<Usuario>> ObtenerTodos();
}