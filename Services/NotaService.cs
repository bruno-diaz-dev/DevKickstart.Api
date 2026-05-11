using DevKickstart.Application.Interfaces;
using DevKickstart.Domain.Entities;

namespace DevKickstart.Api.Services;

public class NotaService
{
    private readonly INotaRepository _repository;

    public NotaService(
        INotaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Nota> CrearNota(
        string titulo,
        string contenido,
        Guid usuarioId)
    {
        var nota = new Nota(
            titulo,
            contenido,
            usuarioId
        );

        await _repository.Guardar(nota);

        return nota;
    }

    public async Task<IEnumerable<Nota>>
        ObtenerPorUsuario(Guid usuarioId)
    {
        return await _repository
            .ObtenerPorUsuario(usuarioId);
    }

    public async Task<Nota?> ObtenerPorId(
        Guid id)
    {
        return await _repository
            .ObtenerPorId(id);
    }
}