using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Controllers {
  [Route ("api/[controller]")]
  [ApiController]
  public class CharactersController : ControllerBase {
    private static Character _knight = new Character ();

    [HttpGet]
    public ActionResult<IEnumerable<Character>> Get () {
      return Ok (_knight);
    }
  }
}