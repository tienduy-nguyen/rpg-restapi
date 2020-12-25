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
  public class CharacterSkillController : ControllerBase {
    private readonly ICharacterSkillService _characterSkillService;
    public CharacterSkillController (ICharacterSkillService weaponService) {
      _characterSkillService = weaponService;
    }
    public async Task<IActionResult> AddCharacterSkill (AddCharacterSkillDto newCharacterSkillDto) {
      ServiceResponse<GetCharacterDto> response = await _characterSkillService.AddCharacterSkill (newCharacterSkillDto);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }
  }
}