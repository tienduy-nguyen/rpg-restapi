using System.Collections.Generic;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Dtos {
  public class GetUserDto {
    public string Username { get; set; }
    public List<Character> Characters { get; set; }
    public string Role { get; set; }
  }
}