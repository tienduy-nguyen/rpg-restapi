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
  public class CharactersController : ControllerBase {
    public CharactersController (ICharacterService characterService) {
      _characterService = characterService;
    }
    private readonly ICharacterService _characterService;

    /// <summary>
    /// Private User route: Get all characters of current user
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetAll () {
      return Ok (await _characterService.GetAllCharacters ());
    }

    /// <summary>
    /// Private User route: Get Character by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    /// Character DTO
    /// </returns>
    [HttpGet ("{id}")]
    public async Task<ActionResult<Character>> GetCharacterById (int id) {
      return Ok (await _characterService.GetCharacterById (id));
    }

    /// <summary>
    /// Private User route: Create new character for current user
    /// </summary>
    /// <param name="newCharacterDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Character>> CreateCharacter (AddCharacterDto newCharacterDto) {
      return Ok (await _characterService.AddCharacter (newCharacterDto));
    }

    /// <summary>
    /// Private User route: Update a character by Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateCharacterDto"></param>
    /// <returns></returns>
    [HttpPut ("{id}")]
    public async Task<IActionResult> UpdateCharacter (int id, UpdateCharacterDto updateCharacterDto) {
      if (id != updateCharacterDto.Id) {
        return BadRequest ();
      }
      ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter (id, updateCharacterDto);
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

    /// <summary>
    /// Private User route: Delete a character by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteCharacter (int id) {
      ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter (id);
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

  }
}