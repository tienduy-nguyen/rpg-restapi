using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
using Rpg_Restapi.Services;

namespace Rpg_Restapi.Controllers {
  [Authorize (Roles = "Admin")]
  [Route ("api/[controller]")]
  [ApiController]
  public class WeaponsController : ControllerBase {
    private readonly IWeaponService _weaponService;
    public WeaponsController (IWeaponService weaponService) {
      _weaponService = weaponService;
    }
    /// <summary>
    /// Private Admin route: Create new weapon
    /// </summary>
    /// <param name="newWeaponDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddWeapon (AddWeaponDto newWeaponDto) {
      ServiceResponse<GetCharacterDto> response = await _weaponService.AddWeapon (newWeaponDto);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Public route: Get all weapons types
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllWeapons () {
      return Ok (await _weaponService.GetAllWeapons ());
    }

  }
}