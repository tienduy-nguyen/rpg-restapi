using System.Collections.Generic;
using System.Linq;
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
    public ActionResult<IEnumerable<Character>> GetAll () {
      return Ok (_characterService.GetAllCharacters ());
    }

    [HttpGet ("{id}")]
    public ActionResult<Character> GetCharacterById (int id) {
      return Ok (_characterService.GetCharacterById (id));
    }

    [HttpPost]
    public ActionResult<Character> CreateCharacter (Character newCharacter) {
      var check = _characterService.GetAllCharacters ().FirstOrDefault (c => c.Id == newCharacter.Id);
      if (check != null) return Conflict (new { message = $"Character with id '{newCharacter.Id}' already existed!" });
      _characterService.AddCharacter (newCharacter);
      return Ok (newCharacter);
    }

    [HttpPut ("{id}")]
    public IActionResult UpdateCharacter (int id, Character character) {
      var charList = _characterService.GetAllCharacters ();
      var check = charList.FirstOrDefault (c => c.Id == id);
      if (check == null) return NotFound ();
      int index = charList.FindIndex (c => c.Id == id);
      charList[index] = character;
      check = character;
      return NoContent ();
    }

    [HttpPatch ("{id}")]
    public IActionResult UpdatePartialCharacter (int id, JsonPatchDocument<Character> patchDoc) {
      var charList = _characterService.GetAllCharacters ();
      var check = _characterService.GetAllCharacters ().FirstOrDefault (c => c.Id == id);
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
    public IActionResult DeleteCharacter (int id) {
      var charList = _characterService.GetAllCharacters ();
      var check = charList.FirstOrDefault (c => c.Id == id);
      if (check == null) return NotFound ();
      charList.RemoveAll (c => c.Id == id);
      return NoContent ();
    }

  }
}