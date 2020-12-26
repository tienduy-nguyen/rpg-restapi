using System.Threading.Tasks;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services.SkillService {
  public interface ISkillService {
    Task<ServiceResponse<GetSkillDto>> GetAllSkills ();
  }
}