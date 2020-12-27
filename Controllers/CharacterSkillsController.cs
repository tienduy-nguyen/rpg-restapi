using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
using Rpg_Restapi.Services;

namespace Rpg_Restapi.Controllers {
  [Authorize]
  [Route ("api/[controller]")]
  [ApiController]
  public class CharacterSkillsController : ControllerBase {
    private readonly ICharacterSkillService _characterSkillService;
    public CharacterSkillsController (ICharacterSkillService weaponService) {
      _characterSkillService = weaponService;
    }

    /// <summary>
    /// Private User route: Create new character-skill
    /// </summary>
    /// <param name="newCharacterSkillDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddCharacterSkill (AddCharacterSkillDto newCharacterSkillDto) {
      ServiceResponse<GetCharacterDto> response = await _characterSkillService.AddCharacterSkill (newCharacterSkillDto);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }
  }
}