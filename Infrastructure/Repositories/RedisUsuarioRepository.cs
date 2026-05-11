using System.Text.Json;
using DevKickstart.Application.Interfaces;
using DevKickstart.Domain.Entities;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using DevKickstart.Api.Configuration;

namespace DevKickstart.Infrastructure.Repositories;

public class RedisUsuarioRepository : IUsuarioRepository
{
  private readonly IDatabase _database;
  public RedisUsuarioRepository(IOptions<RedisOptions> options)
  {
    var connection = ConnectionMultiplexer.Connect(options.Value.ConnectionString);
    _database = connection.GetDatabase();
  }

  public async Task Guardar(Usuario usuario)
  {
    var json = JsonSerializer.Serialize(usuario);
    await _database.StringSetAsync(usuario.Id.ToString(), json);

    await _database.SetAddAsync("usuarios", usuario.Id.ToString());
  }

  public async Task<Usuario?> ObtenerPorId(Guid id)
  {
    var value = await _database.StringGetAsync(id.ToString());
    if (value.IsNullOrEmpty)
      return null;

    return DeserializarUsuario(value!);
  }

  public async Task<Usuario?> ObtenerPorNombre(
    string nombre)
  {
    var usuarios = await ObtenerTodos();
    return usuarios.FirstOrDefault(
      u => u.Nombre == nombre
    );
  }

  public async Task<IEnumerable<Usuario>> ObtenerTodos()
  {
    var ids = await _database.SetMembersAsync("usuarios");
    var usuarios = new List<Usuario>();

    foreach (var id in ids)
    {
      var value = await _database.StringGetAsync(id.ToString());

      if (value.IsNullOrEmpty)
          continue;
      
      var usuario = DeserializarUsuario(value!);
      if (usuario != null)
      {
        usuarios.Add(usuario);
      }
    }
    return usuarios;
  }

  private static Usuario? DeserializarUsuario(string json)
  {
    var usuarioGuardado = JsonSerializer.Deserialize<UsuarioGuardado>(json);

    if (usuarioGuardado == null ||
        usuarioGuardado.Id == Guid.Empty ||
        string.IsNullOrWhiteSpace(usuarioGuardado.Nombre) ||
        string.IsNullOrWhiteSpace(usuarioGuardado.PasswordHash))
    {
      return null;
    }

    return Usuario.Rehidratar(
      usuarioGuardado.Id,
      usuarioGuardado.Nombre,
      usuarioGuardado.PasswordHash
    );
  }

  private sealed class UsuarioGuardado
  {
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
  }
}
