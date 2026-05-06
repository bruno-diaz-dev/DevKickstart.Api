using Microsoft.AspNetCore.Mvc;
using DevKickstart.Domain.Entities;
using DevKickstart.Api.Services;

namespace DevKickstart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _service;

    public UsuariosController(UsuarioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] string nombre)
    {
        var usuario = await _service.CrearUsuario(nombre);
        return Ok(usuario);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Obtener(Guid id)
    {
        var usuario = await _service.ObtenerUsuario(id);
        if (usuario == null)
            return NotFound();
        return Ok(usuario);
    }
 }