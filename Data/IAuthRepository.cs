using System.Threading.Tasks;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Data {
  public interface IAuthRepository {
    Task<ServiceResponse<string>> Register (User user, string password);
    Task<ServiceResponse<string>> Login (string username, string password);
    Task<bool> UserExists (string username);
  }
}