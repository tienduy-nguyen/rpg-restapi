using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Controllers {
  [Route ("api/[controller]")]
  [ApiController]
  public class CharactersController : ControllerBase {
    private static Character _knight = new Character ();
    private static List<Character> _characterList = new List<Character> {
      new Character (),
      new Character { Id = 1, Name = "Same" },
      new Character { Id = 2, Name = "Paul" },
      new Character { Id = 2, Name = "John" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Character>> GetAll () {
      return Ok (_characterList);
    }

    [HttpGet ("{id}")]
    public ActionResult<Character> GetCharacterById (int id) {
      return Ok (_characterList.FirstOrDefault (c => c.Id == id));
    }

    [HttpPost]
    public ActionResult<Character> CreateCharacter (Character newCharacter) {
      var check = _characterList.FirstOrDefault (c => c.Id == newCharacter.Id);
      if (check != null) return Conflict (new { message = $"Character with id '{newCharacter.Id}' already existed!" });
      _characterList.Add (newCharacter);
      return Ok (newCharacter);
    }

    [HttpPut ("{id}")]
    public IActionResult UpdateCharacter (int id, Character character) {
      var check = _characterList.FirstOrDefault (c => c.Id == id);
      if (check == null) return NotFound ();
      int index = _characterList.FindIndex (c => c.Id == id);
      _characterList[index] = character;
      return NoContent ();
    }

    [HttpPatch ("{id}")]
    public IActionResult UpdatePartialCharacter (int id, JsonPatchDocument<Character> patchDoc) {
      var check = _characterList.FirstOrDefault (c => c.Id == id);
      if (check == null) return NotFound ();
      patchDoc.ApplyTo (check);
      if (!TryValidateModel (check)) {
        return ValidationProblem (ModelState);
      }
      int index = _characterList.FindIndex (c => c.Id == id);
      _characterList[index] = check;
      return NoContent ();
    }

    [HttpDelete ("{id}")]
    public IActionResult DeleteCharacter (int id) {
      var check = _characterList.FirstOrDefault (c => c.Id == id);
      if (check == null) return NotFound ();
      _characterList.RemoveAll (c => c.Id == id);
      return NoContent ();
    }

  }
}