using Rpg_Restapi.Models;
namespace Rpg_Restapi.Dtos {
  public class AddCharacterDto {
    public string Name { get; set; } = "Frodo";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Knight;
  }
}