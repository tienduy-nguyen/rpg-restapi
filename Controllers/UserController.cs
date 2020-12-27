using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
using Rpg_Restapi.Services;

namespace Rpg_Restapi.Controllers {
  [Authorize]
  [Route ("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase {
    public UserController (IUserService userService) {
      _userService = userService;
    }
    private readonly IUserService _userService;

    [Authorize (Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetAll () {
      ServiceResponse<List<GetUserDto>> response = await _userService.GetAllUsers ();
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    [HttpGet ("{id:int}")]
    public async Task<ActionResult<Character>> GetUserById (int id) {
      ServiceResponse<GetUserDto> response = await _userService.GetUserById (id);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);

    }

    [HttpGet ("{username:string}")]
    public async Task<ActionResult<Character>> GetUserById (string username) {
      ServiceResponse<GetUserDto> response = await _userService.GetUserByUsername (username);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);

    }

    [HttpPut ("{id}")]
    public async Task<IActionResult> UpdateCharacter (int id, UpdateUserDto updateUser) {
      ServiceResponse<GetUserDto> response = await _userService.UpdateUser (id, updateUser);
      if (id != updateUser.Id) {
        return BadRequest (response);
      }
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

    [Authorize (Roles = "Admin")]
    [HttpDelete ("{id:int}")]
    public async Task<IActionResult> DeleteUser (int id) {
      ServiceResponse<List<GetUserDto>> response = await _userService.DeleteUser (id);
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

  }
}