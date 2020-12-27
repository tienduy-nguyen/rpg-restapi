using System.Collections.Generic;
using System.Threading.Tasks;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public interface IUserService {

    Task<ServiceResponse<List<GetUserDto>>> GetAllUsers ();
    Task<ServiceResponse<GetUserDto>> GetUserById (int id);
    Task<ServiceResponse<GetUserDto>> GetUserByUsername (string username);
    Task<ServiceResponse<GetUserDto>> UpdateUser (int id, UpdateUserDto updateUserDto);
    Task<ServiceResponse<GetUserDto>> UpdateUser (string username, UpdateUserDto updateUserDto);
    Task<ServiceResponse<GetUserDto>> DeleteAccount (int id);
    Task<ServiceResponse<List<GetUserDto>>> DeleteUser (int id);

  }
}