using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
namespace Rpg_Restapi.Services {
  public class UserService : IUserService {
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public UserService (DataContext context, IMapper mapper) {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers () {
      ServiceResponse<List<GetUserDto>> response = new ServiceResponse<List<GetUserDto>> ();
      // var userList = _context.Users.
      return response;
    }

    public async Task<ServiceResponse<GetUserDto>> GetUserById (int id) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<GetUserDto>> GetUserByUsername (string username) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<GetUserDto>> UpdateUser (int id, UpdateUserDto updateUserDto) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<GetUserDto>> UpdateUser (string username, UpdateUserDto updateUserDto) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<GetUserDto>> DeleteAccount (int id) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser (int id) {
      throw new System.NotImplementedException ();
    }

  }
}