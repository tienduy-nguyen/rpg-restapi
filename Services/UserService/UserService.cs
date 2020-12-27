using System.Collections.Generic;
using System.Threading.Tasks;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public class UserService : IUserService {
    public Task<ServiceResponse<GetUserDto>> DeleteAccount (int id) {
      throw new System.NotImplementedException ();
    }

    public Task<ServiceResponse<List<GetUserDto>>> DeleteUser (int id) {
      throw new System.NotImplementedException ();
    }

    public Task<ServiceResponse<List<GetUserDto>>> GetAllUsers () {
      throw new System.NotImplementedException ();
    }

    public Task<ServiceResponse<GetUserDto>> GetUserById (int id) {
      throw new System.NotImplementedException ();
    }

    public Task<ServiceResponse<GetUserDto>> GetUserByUsername (string username) {
      throw new System.NotImplementedException ();
    }

    public Task<ServiceResponse<GetUserDto>> UpdateUser (int id, UpdateUserDto updateUserDto) {
      throw new System.NotImplementedException ();
    }

    public Task<ServiceResponse<GetUserDto>> UpdateUser (string username, UpdateUserDto updateUserDto) {
      throw new System.NotImplementedException ();
    }
  }
}