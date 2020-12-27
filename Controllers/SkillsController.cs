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
    /// <summary>
    /// Public route: Get all skills exist
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllSkill () {
      ServiceResponse<List<GetSkillDto>> response = await _skillService.GetAllSkills ();
      return Ok (response);
    }

    /// <summary>
    /// Public route: Get detail skill by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet ("{id}")]
    public async Task<IActionResult> GetSkillById (int id) {
      ServiceResponse<GetSkillDto> response = await _skillService.GetSkillById (id);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Private Admin route: Create new skill
    /// </summary>
    /// <param name="newSkillDto"></param>
    /// <returns></returns>
    [Authorize (Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddSkill (AddSkillDto newSkillDto) {
      ServiceResponse<List<GetSkillDto>> response = await _skillService.AddSkill (newSkillDto);
      if (!response.Success) {
        return BadRequest (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Private Admin route: Update skill
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateSkillDto"></param>
    /// <returns></returns>
    [Authorize (Roles = "Admin")]
    [HttpPut ("{id}")]
    public async Task<IActionResult> UpdateSkill (int id, UpdateSkillDto updateSkillDto) {
      if (id != updateSkillDto.Id) {
        return BadRequest ();
      }
      ServiceResponse<GetSkillDto> response = await _skillService.UpdateSkill (id, updateSkillDto);
      if (!response.Success) {
        return BadRequest (response);
      }
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Private Admin route: Delete a skill by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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