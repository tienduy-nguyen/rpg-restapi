using System.Collections.Generic;
using System.Threading.Tasks;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
namespace Rpg_Restapi.Services {
  public interface IFightService {
    Task<ServiceResponse<AttackResultDto>> WeaponAttack (WeaponAttackDto request);
    Task<ServiceResponse<AttackResultDto>> SkillAttack (SkillAttackDto request);
    Task<ServiceResponse<FightResultDto>> Fight (FightRequestDto request);
    Task<ServiceResponse<List<HighscoreDto>>> GetHighScore ();
  }
}