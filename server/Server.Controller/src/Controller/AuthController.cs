using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.src.Common;
using Server.Service.src.DTO;
using Server.Service.src.ServiceAbstract.Authentication;

namespace Server.Controller.src.Controller;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public string Login([FromBody] UserCredential credential)
    {
        Console.WriteLine("In Authentication");
        return _authService.Login(credential);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<UserReadDTO> GetCurrentProfile()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        return await _authService.GetCurrentProfile(Guid.Parse(userId));
    }
}