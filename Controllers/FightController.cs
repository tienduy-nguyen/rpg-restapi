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
  public class FightController : ControllerBase {
    private readonly IFightService _fightService;
    public FightController (IFightService fightService) {
      _fightService = fightService;
    }

    /// <summary>
    /// Private User route: Attack an opponent by weapon
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost ("Weapon")]
    public async Task<IActionResult> WeaponAttack (WeaponAttackDto request) {
      ServiceResponse<AttackResultDto> response = await _fightService.WeaponAttack (request);
      if (!response.Success) {
        return BadRequest (response);

      }
      return Ok (response);
    }

    /// <summary>
    /// Private User route: Attack an opponent by skill
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost ("Skill")]
    public async Task<IActionResult> SkillAttack (SkillAttackDto request) {
      ServiceResponse<AttackResultDto> response = await _fightService.SkillAttack (request);

      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Private User route: Fight between characters
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Fight (FightRequestDto request) {
      ServiceResponse<FightResultDto> response = await _fightService.Fight (request);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Public route: Get highscore of players
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet ("highscore")]
    public async Task<IActionResult> GetHighscore () {
      return Ok (await _fightService.GetHighscore ());
    }
  }
}