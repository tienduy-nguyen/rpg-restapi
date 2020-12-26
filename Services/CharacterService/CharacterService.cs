using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
namespace Rpg_Restapi.Services {

  public class CharacterService : ICharacterService {
    /* Private variables */
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private int _GetUserId () => int.Parse (_httpContextAccessor.HttpContext.User.FindFirstValue (ClaimTypes.NameIdentifier));
    private string _GetUserRole () => _httpContextAccessor.HttpContext.User.FindFirstValue (ClaimTypes.Role);

    /* Constructor */
    public CharacterService (IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor) {
      _mapper = mapper;
      _context = context;
      _httpContextAccessor = httpContextAccessor;
    }

    /* Public Methods  */
    /// <summary>
    /// Get all characters belongs to current user
    /// </summary>
    /// <returns>List of characters</returns>
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters () {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      string role = _GetUserRole ();
      var charList = role.Equals ("Admin") ?
        await _context.Characters.ToListAsync () :
        await _context.Characters.Where (c => c.UserId == _GetUserId ()).ToListAsync ();
      serviceResponse.Data = (charList.Select (c => _mapper.Map<GetCharacterDto> (c))).OrderBy (c => c.Id).ToList ();
      return serviceResponse;
    }

    /// <summary>
    /// Add new character for current user
    /// </summary>
    /// <param name="newCharacterDto"></param>
    /// <returns>List of characters belongs to current user</returns>

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter (AddCharacterDto newCharacterDto) {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      Character character = _mapper.Map<Character> (newCharacterDto);
      int userId = _GetUserId ();
      character.User = await _context.Users.FirstOrDefaultAsync (u => u.Id == userId);
      await _context.Characters.AddAsync (character);
      await _context.SaveChangesAsync ();

      var charList = await _context.Characters.Where (c => c.UserId == userId).ToListAsync ();
      // Convert character to Character Data Transfer object
      serviceResponse.Data = (charList.Select (c => _mapper.Map<GetCharacterDto> (c))).OrderBy (c => c.Id).ToList ();
      return serviceResponse;
    }

    /// <summary>
    /// Get a character by Id of current user
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Character found</returns>
    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById (int id) {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto> ();
      var charFound = await _context.Characters.FirstOrDefaultAsync (c => c.Id == id && c.UserId == _GetUserId ());
      if (charFound == null) {
        serviceResponse.Success = false;
        serviceResponse.Message = $"Character with id {id} not found!";
        return serviceResponse;
      }
      serviceResponse.Data = _mapper.Map<GetCharacterDto> (charFound);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter (int id, UpdateCharacterDto updatedCharacterDto) {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto> ();
      try {
        var charFound = await _context.Characters.FirstOrDefaultAsync (c => c.Id == updatedCharacterDto.Id && c.UserId == _GetUserId ());
        if (charFound == null) {
          serviceResponse.Success = false;
          serviceResponse.Message = $"Character with id {updatedCharacterDto.Id} not found!";
          return serviceResponse;
        }
        var updateCharacter = _mapper.Map<Character> (updatedCharacterDto);
        _context.Entry (updateCharacter).State = EntityState.Modified;
        await _context.SaveChangesAsync ();
        serviceResponse.Data = _mapper.Map<GetCharacterDto> (updateCharacter);
      } catch (Exception ex) {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter (int id) {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      try {
        var charFound = await _context.Characters.FirstOrDefaultAsync (c => c.Id == id && c.UserId == _GetUserId ());
        if (charFound == null) {
          serviceResponse.Success = false;
          serviceResponse.Message = $"Character with id {id} not found!";
          return serviceResponse;
        }
        _context.Characters.Remove (charFound);
        await _context.SaveChangesAsync ();
        var charList = await _context.Characters.Where (c => c.UserId == _GetUserId ()).ToListAsync ();
        serviceResponse.Data = (charList.Select (c => _mapper.Map<GetCharacterDto> (c))).OrderBy (c => c.Id).ToList ();
      } catch (Exception ex) {

        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    /* Private methods */
    private bool CharacterExists (int id) {
      return _context.Characters.Any (e => e.Id == id);
    }
  }
}