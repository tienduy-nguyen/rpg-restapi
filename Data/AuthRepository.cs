using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Data {
  public class AuthRepository : IAuthRepository {
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;
    public AuthRepository (DataContext context, IConfiguration configuration) {
      _configuration = configuration;
      _context = context;
    }
    public async Task<ServiceResponse<string>> Login (string username, string password) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<int>> Register (User user, string password) {
      await _context.Users.AddAsync (user);
      await _context.SaveChangesAsync ();
      ServiceResponse<int> response = new ServiceResponse<int> ();
      response.Data = user.Id;
      return response;
    }

    public async Task<bool> UserExists (string username) {
      throw new System.NotImplementedException ();
    }

    // /// <summary>
    // /// Create a Jwt token
    // /// </summary>
    // /// <param name="user"></param>
    // /// <returns></returns>
    // private string CreateToken (User user) {
    //   List<Claim> claims = new List<Claim> {
    //     new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
    //     new Claim (ClaimTypes.Name, user.Username),
    //     new Claim (ClaimTypes.Role, user.Role)
    //   };

    //   SymmetricSecurityKey key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_confi))
    // }

  }
}