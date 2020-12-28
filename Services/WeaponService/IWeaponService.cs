using System.Collections.Generic;
using System.Threading.Tasks;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public interface IWeaponService {
    Task<ServiceResponse<GetWeaponDto>> CreateNewWeapon (CreateWeaponDto newWeaponDto);
    Task<ServiceResponse<List<GetWeaponDto>>> GetAllWeapons ();
  }
}