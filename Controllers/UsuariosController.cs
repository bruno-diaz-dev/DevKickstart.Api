using Microsoft.AspNetCore.Mvc;
using DevKickstart.Api.Models;
using DevKickstart.Api.Services;

namespace DevKickstart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioService _usuarioService;
    private readonly RedisService _redisService;

    public UsuariosController(RedisService redisService, UsuarioService usuarioService)
    {
        _redisService = redisService;
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public IActionResult GetUsuarios()
    {
        var usuarios = new List<Usuario>
        {
            new Usuario { Nombre = "Ana", Edad = 28, Activo = true },
            new Usuario { Nombre = "Luis", Edad = 22, Activo = false },
            new Usuario { Nombre = "Carlos", Edad = 35, Activo = true },
            new Usuario { Nombre = "Marta", Edad = 19, Activo = true },
        };

        return Ok(usuarios);
    }

    [HttpGet("activos")]
    public IActionResult GetActivos()
    {
        var usuarios = new List<Usuario>
        {
            new Usuario { Nombre = "Ana", Edad = 28, Activo = true },
            new Usuario { Nombre = "Luis", Edad = 22, Activo = false },
            new Usuario { Nombre = "Carlos", Edad = 35, Activo = true },
            new Usuario { Nombre = "Marta", Edad = 19, Activo = true },
        };

        var activos = _usuarioService.ContarUsuariosActivos(usuarios);

        return Ok(new { Activos = activos });
    }

    [HttpPost("cache")]
    public async Task<IActionResult> GuardarEnCache()
    {
        await _redisService.SetValue("usuario", "Bruno");

        return Ok("Guardado en Redis");
    }

    [HttpGet("cache")]
    public async Task<IActionResult> ObtenerDeCache()
    {
        var valor = await _redisService.GetValue("usuario");

        return Ok(new { valor });
    }
}