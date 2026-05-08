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
    return JsonSerializer.Deserialize<Usuario>(value!);
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
      
      var usuario = JsonSerializer.Deserialize<Usuario>(value!);
      if (usuario != null)
      {
        usuarios.Add(usuario);
      }
    }
    return usuarios;
  }
}