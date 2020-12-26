using System.Collections.Generic;
using System.Threading.Tasks;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public class SkillService : ISkillService {

    public async Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills () {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<GetSkillDto>> GetSkillById (int id) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<List<GetSkillDto>>> AddSkill (AddSkillDto newSkillDto) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<GetSkillDto>> UpdateSkill (int id, UpdateSkillDto updateSkillDto) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<List<GetSkillDto>>> DeleteSkill (int id) {
      throw new System.NotImplementedException ();
    }
  }
}