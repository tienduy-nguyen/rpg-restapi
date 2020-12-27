using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
using Rpg_Restapi.Services;
namespace Rpg_Restapi.Controllers {
  [Authorize]
  [Route ("api/[controller]")]
  [ApiController]
  public class SkillsController : ControllerBase {
    private readonly ISkillService _skillService;
    public SkillsController (ISkillService skillService) {
      _skillService = skillService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllSkill () {
      ServiceResponse<List<GetSkillDto>> response = await _skillService.GetAllSkills ();
      return Ok (response);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> GetSkillById (int id) {
      ServiceResponse<GetSkillDto> response = await _skillService.GetSkillById (id);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    [Authorize (Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddSkill (AddSkillDto newSkillDto) {
      ServiceResponse<List<GetSkillDto>> response = await _skillService.AddSkill (newSkillDto);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    [Authorize (Roles = "Admin")]
    [HttpPut ("{id}")]
    public async Task<IActionResult> UpdateCharacter (int id, UpdateSkillDto updateSkillDto) {
      ServiceResponse<GetSkillDto> response = await _skillService.UpdateSkill (id, updateSkillDto);
      if (id != updateSkillDto.Id || !response.Success) {
        return BadRequest (response);
      }
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

    [Authorize (Roles = "Admin")]
    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteSkill (int id) {
      ServiceResponse<List<GetSkillDto>> response = await _skillService.DeleteSkill (id);
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

  }
}