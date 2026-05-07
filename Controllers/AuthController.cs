using DevKickstart.Api.Contracts.Requests;
using DevKickstart.Api.Contracts.Responses;
using DevKickstart.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DevKickstart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly DevKickstart.Api.Services.Auth.TokenService _tokenService;

    public AuthController(
    DevKickstart.Api.Services.Auth.TokenService tokenService)
    {
        _tokenService = tokenService;
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
}