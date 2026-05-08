using DevKickstart.Application.Interfaces;
using DevKickstart.Domain.Entities;

namespace DevKickstart.Api.Services;

public class UsuarioService
{
    private readonly IUsuarioRepository _repository;
    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }
    public async Task<Usuario> CrearUsuario(string nombre, string passwordHash)
    {
     var usuario = new Usuario(nombre, passwordHash);
     await _repository.Guardar(usuario);
     return usuario;
    }

    public async Task<Usuario?> ObtenerUsuario(Guid id)
    {
        return await _repository.ObtenerPorId(id);
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodos()
    {
        return await _repository.ObtenerTodos();
    }
}