using System.Security.Claims;
using DevKickstart.Api.Contracts.Requests;
using DevKickstart.Api.Services;
using DevKickstart.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevKickstart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotasController : ControllerBase
{
    private readonly NotaService _notaService;
    private readonly UsuarioService _usuarioService;

    public NotasController(
        NotaService notaService,
        UsuarioService usuarioService)
    {
        _notaService = notaService;
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> CrearNota(
        [FromBody] CrearNotaRequest request)
    {
        var username =
            User.FindFirstValue(
                ClaimTypes.Name
            );

        if (username == null)
        {
            return Unauthorized();
        }

        var usuario =
            await _usuarioService
                .ObtenerPorNombre(username);

        if (usuario == null)
        {
            return Unauthorized();
        }

        var nota =
            await _notaService.CrearNota(
                request.Titulo,
                request.Contenido,
                usuario.Id
            );
        return Ok(nota);
    }

    [HttpGet]
    public async Task<IActionResult>
        ObtenerNotas()
    {
        var username =
            User.FindFirstValue(
                ClaimTypes.Name
            );

        if (username == null)
        {
            return Unauthorized();
        }

        var usuario =
            await _usuarioService
                .ObtenerPorNombre(username);

        if (usuario == null)
        {
            return Unauthorized();
        }

        var notas =
            await _notaService
                .ObtenerPorUsuario(
                    usuario.Id
                );
        return Ok(notas);
    }
}