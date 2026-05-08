using DecKickstart.Api.Contracts.Requests;
using DevKickstart.Api.Contracts.Requests;
using DevKickstart.Api.Contracts.Responses;
using DevKickstart.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using DevKickstart.Api.Services;

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
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // En caso real, aqui se valida si el usuario existe y las credenciales son correctas
        var token = _tokenService.CrearToken(request.Username);
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