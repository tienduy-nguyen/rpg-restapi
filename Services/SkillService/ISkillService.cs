using System.Collections.Generic;
using System.Threading.Tasks;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public interface ISkillService {
    Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills ();
    Task<ServiceResponse<GetSkillDto>> GetSkillById (int id);
    Task<ServiceResponse<List<GetSkillDto>>> AddSkill (AddSkillDto newSkillDto);
    Task<ServiceResponse<GetSkillDto>> UpdateSkill (int id, UpdateSkillDto updateSkillDto);
    Task<ServiceResponse<List<GetSkillDto>>> DeleteSkill (int id);
  }
}