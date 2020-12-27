using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rpg_Restapi.Data;
using Rpg_Restapi.Models;
using Rpg_Restapi.Utilities;

namespace Rpg_Restapi.Services {
  public class AuthService : IAuthService {
    private DataContext _context;
    private IConfiguration _configuration;
    public AuthService (DataContext context, IConfiguration configuration) {
      _configuration = configuration;
      _context = context;
    }

    /// <summary>
    /// Login user
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>Service response with a data user token</returns>
    public async Task<ServiceResponse<string>> Login (string username, string password) {
      ServiceResponse<string> response = new ServiceResponse<string> ();
      User user = await _context.Users.FirstOrDefaultAsync (u => u.Username.ToLower () == username.ToLower ());
      if (user == null) {
        response.Success = false;
        response.Message = $"User with '{username}' not found";
        return response;
      }
      if (!Security.VerifyPasswordHash (password, user.PasswordHash, user.PasswordSalt)) {
        response.Success = false;
        response.Message = "Invalid credentials";
        return response;
      }
      response.Data = CreateToken (user);
      return response;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns>Service response with a data user token</returns>
    public async Task<ServiceResponse<string>> Register (User user, string password) {
      ServiceResponse<string> response = new ServiceResponse<string> ();
      if (await UserExists (user.Username)) {
        response.Success = false;
        response.Message = $"User with '{user.Username}' already exists";
        return response;
      }
      Security.CreatePasswordHash (password, out byte[] passwordHash, out byte[] passwordSalt);
      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;
      await _context.Users.AddAsync (user);
      await _context.SaveChangesAsync ();
      response.Data = CreateToken (user);
      return response;
    }

    public async Task<bool> UserExists (string username) {
      var isExist = await _context.Users.AnyAsync (u => u.Username.ToLower () == username.ToLower ());
      if (!isExist) {
        return false;
      }
      return true;
    }

    /// <summary>
    /// Create a Jwt token
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private string CreateToken (User user) {
      List<Claim> claims = new List<Claim> {
        new Claim (ClaimTypes.NameIdentifier, user.Id.ToString ()),
        new Claim (ClaimTypes.Name, user.Username),
        new Claim (ClaimTypes.Role, user.Role)
      };

      SymmetricSecurityKey key = new SymmetricSecurityKey (
        Encoding.UTF8.GetBytes (_configuration.GetSection ("AppSettings:Token").Value)
      );

      SigningCredentials creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha512Signature);

      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity (claims),
        Expires = DateTime.Now.AddDays (1),
        SigningCredentials = creds
      };

      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler ();
      SecurityToken token = tokenHandler.CreateToken (tokenDescriptor);

      return tokenHandler.WriteToken (token);
    }

  }
}