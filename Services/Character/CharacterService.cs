using System;
using System.Collections.Generic;
using System.Linq;
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
    private IMapper _mapper;
    private DataContext _context;
    private IHttpContextAccessor _httpcontextAccessor;
    public CharacterService (IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessort) {
      _mapper = mapper;
      _context = context;
      _httpcontextAccessor = httpContextAccessort;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters () {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      var charList = await _context.Characters.ToListAsync ();
      serviceResponse.Data = (charList.Select (c => _mapper.Map<GetCharacterDto> (c))).ToList ();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter (AddCharacterDto newCharacterDto) {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      Character character = _mapper.Map<Character> (newCharacterDto);
      _context.Characters.Add (character);
      await _context.SaveChangesAsync ();

      var charList = await _context.Characters.ToListAsync ();
      serviceResponse.Data = (charList.Select (c => _mapper.Map<GetCharacterDto> (c))).ToList ();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById (int id) {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto> ();
      var charFound = await _context.Characters.FindAsync (id);
      if (charFound == null) {
        serviceResponse.Success = false;
        serviceResponse.Message = $"Character with id '{id}' not found!";
        return serviceResponse;
      }
      serviceResponse.Data = _mapper.Map<GetCharacterDto> (charFound);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter (int id, UpdateCharacterDto updatedCharacterDto) {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto> ();
      if (!CharacterExists (updatedCharacterDto.Id)) {
        serviceResponse.Success = false;
        serviceResponse.Message = $"Character with id '{updatedCharacterDto.Id}' not found!";
        return serviceResponse;
      }
      var updateCharacter = _mapper.Map<Character> (updatedCharacterDto);
      _context.Entry (updateCharacter).State = EntityState.Modified;
      try {
        await _context.SaveChangesAsync ();
        var charUpdated = _mapper.Map<Character> (updatedCharacterDto);
        serviceResponse.Data = _mapper.Map<GetCharacterDto> (charUpdated);
      } catch (Exception ex) {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter (int id) {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      try {
        var charFound = await _context.Characters.FindAsync (id);
        if (charFound == null) {
          serviceResponse.Success = false;
          serviceResponse.Message = $"Character with id '{id}' not found!";
          return serviceResponse;
        }
        _context.Remove (charFound);
        await _context.SaveChangesAsync ();
        var charList = await _context.Characters.ToListAsync ();
        serviceResponse.Data = (charList.Select (c => _mapper.Map<GetCharacterDto> (c))).ToList ();
      } catch (Exception ex) {

        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    private bool CharacterExists (int id) {
      return _context.Characters.Any (e => e.Id == id);
    }
  }
}