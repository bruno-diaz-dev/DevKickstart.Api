using DecKickstart.Api.Contracts.Requests;
using DevKickstart.Api.Contracts.Requests;
using DevKickstart.Api.Contracts.Responses;
using DevKickstart.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using DevKickstart.Api.Services;
using BCrypt.Net;
using System.Threading.Tasks;

namespace DevKickstart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly DevKickstart.Api.Services.Auth.TokenService _tokenService;
    private readonly UsuarioService _usuarioService;

    public AuthController(
    DevKickstart.Api.Services.Auth.TokenService tokenService,
    UsuarioService usuarioService)
    {
        _tokenService = tokenService;
        _usuarioService = usuarioService;
    }

    

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var usuario =
            await _usuarioService.ObtenerPorNombre(
                request.Username
            );

        if (usuario == null)
        {
            return Unauthorized(
                "Usuario o password incorrectos"
            );
        }

        var passwordValido =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                usuario.PasswordHash
            );

        if (!passwordValido)
        {
            return Unauthorized(
                "Usuario o password incorrectos"
            );
        }

        var token =
            _tokenService.CrearToken(
                usuario.Nombre
            );

        var response = new LoginResponse
        {
            Token = token
        };

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var usuario = await _usuarioService.CrearUsuario(request.Username, passwordHash);
        return Ok(usuario);
    }
}