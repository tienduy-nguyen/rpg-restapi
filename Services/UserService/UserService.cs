using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
      try {
        var userList = await _context.Users.ToListAsync ();
        response.Data = userList.Select (u => _mapper.Map<GetUserDto> (u)).ToList ();

      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    public async Task<ServiceResponse<GetUserDto>> GetUserById (int id) {
      ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto> ();

      try {
        User user = await _context.Users.FirstOrDefaultAsync (u => u.Id == id);
        if (user == null) {
          response.Success = false;
          response.Message = $"User with id {id} not found!";
          return response;
        }
        response.Data = _mapper.Map<GetUserDto> (user);
      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;

    }

    public async Task<ServiceResponse<GetUserDto>> GetUserByUsername (string username) {
      ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto> ();
      try {
        User user = await _context.Users.FirstOrDefaultAsync (u => u.Username == username);
        if (user == null) {
          response.Success = false;
          response.Message = $"User with username {username} not found!";
          return response;
        }
        response.Data = _mapper.Map<GetUserDto> (user);
        return response;
      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    public async Task<ServiceResponse<GetUserDto>> UpdateUser (int id, UpdateUserDto updateUserDto) {
      ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto> ();
      try {
        User user = await _context.Users.FirstOrDefaultAsync (u => u.Id == id);
        if (user == null) {
          response.Success = false;
          response.Message = $"User with id {id} not found!";
          return response;
        }
        var updateUser = _mapper.Map<User> (updateUserDto);
        _context.Entry (updateUser).State = EntityState.Modified;
        await _context.SaveChangesAsync ();
        response.Data = _mapper.Map<GetUserDto> (updateUser);
        return response;

      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;

    }

    public async Task<ServiceResponse<GetUserDto>> UpdateUser (string username, UpdateUserDto updateUserDto) {

      ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto> ();
      try {
        User user = await _context.Users.FirstOrDefaultAsync (u => u.Username == username);
        if (user == null) {
          response.Success = false;
          response.Message = $"User with username {username} not found!";
          return response;
        }
        var updateUser = _mapper.Map<User> (updateUserDto);
        _context.Entry (updateUser).State = EntityState.Modified;
        await _context.SaveChangesAsync ();
        response.Data = _mapper.Map<GetUserDto> (updateUser);
        return response;

      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    public async Task<ServiceResponse<GetUserDto>> DeleteAccount (int id) {
      ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto> ();
      try {
        User user = await _context.Users.FirstOrDefaultAsync (u => u.Id == id);
        if (user == null) {
          response.Success = false;
          response.Message = $"User with id {id} not found!";
          return response;
        }
        _context.Users.Remove (user);
        await _context.SaveChangesAsync ();
      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser (int id) {
      ServiceResponse<List<GetUserDto>> response = new ServiceResponse<List<GetUserDto>> ();
      try {
        User user = await _context.Users.FirstOrDefaultAsync (u => u.Id == id);
        if (user == null) {
          response.Success = false;
          response.Message = $"User with id {id} not found!";
          return response;
        }
        _context.Users.Remove (user);
        await _context.SaveChangesAsync ();

        var newUserList = await _context.Users.ToListAsync ();
        response.Data = newUserList.Select (u => _mapper.Map<GetUserDto> (u)).ToList ();
      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

  }
}