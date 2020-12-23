using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
using Rpg_Restapi.Services;

namespace Rpg_Restapi.Controllers {
  [Route ("api/[controller]")]
  [ApiController]
  public class CharactersController : ControllerBase {
    public CharactersController (ICharacterService characterService) {
      _characterService = characterService;
    }
    private readonly ICharacterService _characterService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetAll () {
      return Ok (await _characterService.GetAllCharacters ());
    }

    [HttpGet ("{id}")]
    public async Task<ActionResult<Character>> GetCharacterById (int id) {
      return Ok (await _characterService.GetCharacterById (id));
    }

    [HttpPost]
    public async Task<ActionResult<Character>> CreateCharacter (AddCharacterDto newCharacterDto) {
      return Ok (await _characterService.AddCharacter (newCharacterDto));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacter (UpdateCharacterDto updateCharacterDto) {
      ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter (updateCharacterDto);
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

    [HttpPatch ("{id}")]
    public async Task<IActionResult> UpdatePartialCharacter (int id, JsonPatchDocument<UpdateCharacterDto> patchDoc) {
      ServiceResponse<GetCharacterDto> response = await _characterService.UpdatePartialCharacter (id, patchDoc);
      if (response.Data == null) {
        return NotFound (response);
      }
      return Ok (response);
    }

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