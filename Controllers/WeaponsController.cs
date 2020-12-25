using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
using Rpg_Restapi.Services;

namespace Rpg_Restapi.Controllers {
  [Route ("api/[controller]")]
  [ApiController]
  public class WeaponsController : ControllerBase {
    private readonly IWeaponService _weaponService;
    public WeaponsController (IWeaponService weaponService) {
      _weaponService = weaponService;
    }

    [HttpPost]
    public async Task<IActionResult> AddWeapon (AddWeaponDto newWeaponDto) {
      ServiceResponse<GetCharacterDto> response = await _weaponService.AddWeapon (newWeaponDto);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

  }
}