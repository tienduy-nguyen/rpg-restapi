namespace Rpg_Restapi.Dtos {
  public class AttackResultDto {
    public string Attacker { get; set; } // Name of attack character
    public string Opponent { get; set; } // Name of opponent character
    public int AttackerHP { get; set; }
    public int OpponentHP { get; set; }
    public int Damage { get; set; }
  }
}