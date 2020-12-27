using System.Collections.Generic;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Dtos {
  public class UpdateRoleDto {
    public int Id { get; set; }
    public string Role { get; set; } = "Player";
  }
}