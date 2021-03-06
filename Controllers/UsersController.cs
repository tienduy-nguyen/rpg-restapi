using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
using Rpg_Restapi.Services;

namespace Rpg_Restapi.Controllers {
  [Authorize]
  [Route ("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase {
    public UsersController (IUserService userService, IHttpContextAccessor httpContextAccessor) {
      _userService = userService;
      _httpContextAccessor = httpContextAccessor;
    }
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private int _GetUserId () => int.Parse (_httpContextAccessor.HttpContext.User.FindFirstValue (ClaimTypes.NameIdentifier));
    private string _GetUserRole () => _httpContextAccessor.HttpContext.User.FindFirstValue (ClaimTypes.Role);

    /// <summary>
    /// Private Admin route: Get all users
    /// </summary>
    /// <returns>User list</returns>
    [Authorize (Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetAll () {
      ServiceResponse<List<GetUserDto>> response = await _userService.GetAllUsers ();
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Private User route: Get User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User object</returns>
    [HttpGet ("{id:int}")]
    public async Task<ActionResult<Character>> GetUserById (int id) {
      ServiceResponse<GetUserDto> response = await _userService.GetUserById (id);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Private User route: Get user by username
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    [HttpGet ("{username}")]
    public async Task<ActionResult<Character>> GetUserByUsername (string username) {
      ServiceResponse<GetUserDto> response = await _userService.GetUserByUsername (username);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);

    }

    /// <summary>
    /// Private User route: Update user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateUser"></param>
    /// <returns></returns>
    [HttpPut ("{id}")]
    public async Task<IActionResult> UpdateCharacter (int id, UpdateUserDto updateUser) {
      if (id != updateUser.Id) {
        return Unauthorized ();
      }
      ServiceResponse<GetUserDto> response = await _userService.UpdateUser (id, updateUser);
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Private User route: Delete account
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete ("{id:int}")]
    public async Task<IActionResult> DeleteUser (int id) {
      int currentUserId = _GetUserId ();
      string currentUserRole = _GetUserRole ();
      // Check if user is admin
      if (currentUserRole == "Admin") {
        var response = await _userService.DeleteUser (id);
        if (response.Data == null) {
          return NotFound (response);
        }
        return Ok (response);
      }

      // Check if user want to delete his account
      if (currentUserId == id) {
        var response = await _userService.DeleteAccount (id);
        if (response.Data == null) {
          return NotFound (response);
        }
        return Ok (response);
      } else {
        return Unauthorized (); // Cannot delete user if id given is not current user
      }
    }

    /// <summary>
    /// Private Admin route: Update user role (Admin, Player)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateRoleDto"></param>
    /// <returns></returns>
    [Authorize (Roles = "Admin")]
    [HttpPost ("{id}/roles")]
    public async Task<IActionResult> UpdateUserRole (int id, UpdateRoleDto updateRoleDto) {
      if (id != updateRoleDto.Id) {
        return Unauthorized ();
      }
      ServiceResponse<GetUserDto> response = await _userService.UpdateUserRole (id, updateRoleDto);
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

  }
}