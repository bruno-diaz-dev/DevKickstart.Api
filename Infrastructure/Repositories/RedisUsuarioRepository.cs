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
  }

  public async Task<Usuario?> ObtenerPorId(Guid id)
  {
    var value = await _database.StringGetAsync(id.ToString());
    if (value.IsNullOrEmpty)
      return null;
    return JsonSerializer.Deserialize<Usuario>(value!);
  }
}