using System.Threading.Tasks;
using Rpg_Restapi.Data;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public interface IAuthService {
    Task<ServiceResponse<string>> Register (User user, string password);
    Task<ServiceResponse<string>> Login (string username, string password);
    Task<bool> UserExists (string username);
  }
}