using System.Collections.Generic;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Dtos {
  public class UpdateUserDto {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
  }
}