using System.Threading.Tasks;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services.CharacterSkillService {
  public interface ICharacterSkillService {
    public Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill (AddCharacterSkillDto newCharacterSkillDto);
  }
}