using Microsoft.AspNetCore.Mvc;
using DevKickstart.Api.Contracts.Requests;
using DevKickstart.Api.Contracts.Responses;
using DevKickstart.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace DevKickstart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;

    public UsuariosController(UsuarioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearUsuarioRequest request)
    {
        var usuario = await _service.CrearUsuario(request.Nombre, string.Empty);

        var response = new UsuarioResponse
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Obtener(Guid id)
    {
        var usuario = await _service.ObtenerUsuario(id);

        if (usuario == null)
            return NotFound();

        var response = new UsuarioResponse
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre
        };

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var usuarios = await _service.ObtenerTodos();
        var response = usuarios.Select(u=> new UsuarioResponse
        {
            Id = u.Id,
            Nombre = u.Nombre
        });
        return Ok(response);
    }
 }