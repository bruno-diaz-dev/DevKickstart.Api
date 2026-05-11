using BCrypt.Net;
using DevKickstart.Api.Contracts.Requests;
using DevKickstart.Api.Contracts.Responses;
using DevKickstart.Api.Services;
using DevKickstart.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DevKickstart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    private readonly UsuarioService _usuarioService;

    public AuthController(
        TokenService tokenService,
        UsuarioService usuarioService)
    {
        _tokenService = tokenService;
        _usuarioService = usuarioService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        var passwordHash =
            BCrypt.Net.BCrypt.HashPassword(
                request.Password
            );
        var usuario =
            await _usuarioService.CrearUsuario(
                request.Username,
                passwordHash
            );
        return Ok();
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
                "Usuario o password incorrectos."
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
                "Usuario o password incorrectos.");
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
}