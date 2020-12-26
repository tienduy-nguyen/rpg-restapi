namespace Rpg_Restapi.Models {
  public class CharacterSkill {
    public int CharacterId { get; set; }
    public Character Character { get; set; }
    public int SkillId { get; set; }
    public Skill Skill { get; set; }
  }
}