using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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
  }
}