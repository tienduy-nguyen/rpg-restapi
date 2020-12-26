using System.Threading.Tasks;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services.SkillService {
  public class SkillService : ISkillService {
    public Task<ServiceResponse<GetSkillDto>> GetAllSkills () {
      throw new System.NotImplementedException ();
    }
  }
}