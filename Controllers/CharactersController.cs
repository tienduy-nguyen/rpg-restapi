using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<Character>> CreateCharacter (Character newCharacter) {
      var charList = await _characterService.GetAllCharacters ();
      var check = charList.FirstOrDefault (c => c.Id == newCharacter.Id);
      if (check != null) return Conflict (new { message = $"Character with id '{newCharacter.Id}' already existed!" });
      await _characterService.AddCharacter (newCharacter);
      return Ok (newCharacter);
    }

    [HttpPut ("{id}")]
    public async Task<IActionResult> UpdateCharacter (int id, Character character) {
      var charList = await _characterService.GetAllCharacters ();
      var check = charList.FirstOrDefault (c => c.Id == id);
      if (check == null) return NotFound ();
      int index = charList.FindIndex (c => c.Id == id);
      charList[index] = character;
      check = character;
      return NoContent ();
    }

    [HttpPatch ("{id}")]
    public async Task<IActionResult> UpdatePartialCharacter (int id, JsonPatchDocument<Character> patchDoc) {
      var charList = await _characterService.GetAllCharacters ();
      var check = charList.FirstOrDefault (c => c.Id == id);
      if (check == null) return NotFound ();
      patchDoc.ApplyTo (check);
      if (!TryValidateModel (check)) {
        return ValidationProblem (ModelState);
      }
      int index = charList.FindIndex (c => c.Id == id);
      charList[index] = check;
      return NoContent ();
    }

    [HttpDelete ("{id}")]
    public async Task<IActionResult> DeleteCharacter (int id) {
      var charList = await _characterService.GetAllCharacters ();
      var check = charList.FirstOrDefault (c => c.Id == id);
      if (check == null) return NotFound ();
      charList.RemoveAll (c => c.Id == id);
      return NoContent ();
    }

  }
}