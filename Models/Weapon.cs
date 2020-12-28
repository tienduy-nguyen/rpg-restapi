using System.ComponentModel.DataAnnotations;

namespace Rpg_Restapi.Models {
  public class Weapon {
    [Key]
    public System.Guid Id { get; set; }
    public string Name { get; set; }
    public int Damage { get; set; }
  }
}