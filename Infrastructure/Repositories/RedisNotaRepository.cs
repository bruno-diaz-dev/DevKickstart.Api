using System.Text.Json;
using DevKickstart.Application.Interfaces;
using DevKickstart.Domain.Entities;
using StackExchange.Redis;

namespace DevKickstart.Infrastructure.Repositories;

public class RedisNotaRepository
    : INotaRepository
{
    private readonly IDatabase _database;

    public RedisNotaRepository(
        IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task Guardar(Nota nota)
    {
        var json =
            JsonSerializer.Serialize(nota);

        await _database.StringSetAsync(
            $"nota:{nota.Id}",
            json
        );
    }

    public async Task<Nota?> ObtenerPorId(
        Guid id)
    {
        var value =
            await _database.StringGetAsync(
                $"nota:{id}"
            );
        if (value.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<Nota>(
            value!
        );
    }

    public async Task<IEnumerable<Nota>>
        ObtenerPorUsuario(Guid usuarioId)
    {
        var server =
            _database.Multiplexer
                .GetServer(
                    _database.Multiplexer
                        .GetEndPoints()
                        .First()
                );
        var keys =
            server.Keys(
                pattern: "nota:*"
            );

        var notas = new List<Nota>();

        foreach (var key in keys)
        {
            var value =
                await _database.StringGetAsync(
                    key
                );
            if (value.IsNullOrEmpty)
            {
                continue;
            }

            var nota =
                JsonSerializer.Deserialize<Nota>(
                    value!
                );

            if (
                nota != null &&
                nota.UsuarioId == usuarioId
            )
            {
                notas.Add(nota);
            }
        }

        return notas;
    }
}