using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rpg_Restapi.Models {
  public class User {
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public List<Character> Characters { get; set; }

    [Required]
    public string Role { get; set; } = "Player";
  }
}