using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Controllers {
  [Route ("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase {

    private IAuthRepository _authRepo;
    public AuthController (IAuthRepository authRepo) {
      _authRepo = authRepo;
    }

    [HttpPost ("Login")]
    public async Task<IActionResult> Login (UserLoginDto request) {
      ServiceResponse<string> response = await _authRepo.Login (request.Username, request.Password);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    [HttpPost ("register")]
    public async Task<IActionResult> Register (UserRegisterDto request) {
      ServiceResponse<string> response = await _authRepo.Register (
        new User { Username = request.Username }, request.Password
      );
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

  }
}