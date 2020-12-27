using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
using Rpg_Restapi.Services;

namespace Rpg_Restapi.Controllers {
  [Route ("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase {

    private IAuthService _authService;
    public AuthController (IAuthService authService) {
      _authService = authService;
    }

    [HttpPost ("Login")]
    public async Task<IActionResult> Login (UserLoginDto request) {
      ServiceResponse<string> response = await _authService.Login (request.Username, request.Password);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    [HttpPost ("register")]
    public async Task<IActionResult> Register (UserRegisterDto request) {
      ServiceResponse<string> response = await _authService.Register (
        new User { Username = request.Username }, request.Password
      );
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

  }
}