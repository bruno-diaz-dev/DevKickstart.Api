using DevKickstart.Domain.Entities;

namespace DevKickstart.Application.Interfaces;

public interface IUsuarioRepository
{
    Task Guardar(Usuario usuario);
    Task<Usuario?> ObtenerPorId(Guid id);

    Task<Usuario?> ObtenerPorNombre(string nombre);

    Task<IEnumerable<Usuario>> ObtenerTodos();
}