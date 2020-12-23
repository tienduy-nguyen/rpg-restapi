using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {

  public class CharacterService : ICharacterService {
    private IMapper _mapper;
    public CharacterService (IMapper mapper) {
      _mapper = mapper;
    }
    private static List<Character> _characterList = new List<Character> {
      new Character (),
      new Character { Id = 1, Name = "Same" },
      new Character { Id = 2, Name = "Paul" },
      new Character { Id = 3, Name = "John" }
    };
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters () {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      serviceResponse.Data = (_characterList.Select (c => _mapper.Map<GetCharacterDto> (c))).ToList ();
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter (AddCharacterDto newCharacterDto) {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      Character character = _mapper.Map<Character> (newCharacterDto);
      character.Id = _characterList.Max (c => c.Id) + 1;
      _characterList.Add (character);
      serviceResponse.Data = (_characterList.Select (c => _mapper.Map<GetCharacterDto> (c))).ToList ();
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById (int id) {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto> ();
      var charFound = _characterList.FirstOrDefault (c => c.Id == id);
      if (charFound == null) {
        serviceResponse.Success = false;
        serviceResponse.Message = $"Character with id '{id}' not found!";
        return serviceResponse;
      }
      serviceResponse.Data = _mapper.Map<GetCharacterDto> (charFound);
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter (UpdateCharacterDto updatedCharacterDto) {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto> ();
      try {
        var charFound = _characterList.FirstOrDefault (c => c.Id == updatedCharacterDto.Id);
        if (charFound == null) {
          serviceResponse.Success = false;
          serviceResponse.Message = $"Character with id '{updatedCharacterDto.Id}' not found!";
          return serviceResponse;
        }
        var index = _characterList.FindIndex (c => c.Id == charFound.Id);
        var charUpdated = _mapper.Map<Character> (updatedCharacterDto);
        _characterList[index] = charUpdated;
        serviceResponse.Data = _mapper.Map<GetCharacterDto> (charUpdated);
      } catch (Exception ex) {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdatePartialCharacter (int id, JsonPatchDocument<UpdateCharacterDto> patchCharacterDto) {
      ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto> ();
      try {
        var charFound = _characterList.FirstOrDefault (c => c.Id == id);
        if (charFound == null) {
          serviceResponse.Success = false;
          serviceResponse.Message = $"Character with id '{id}' not found!";
          return serviceResponse;
        }
        var index = _characterList.FindIndex (c => c.Id == charFound.Id);
        var charFoundDto = _mapper.Map<UpdateCharacterDto> (charFound);
        patchCharacterDto.ApplyTo (charFoundDto);
        _characterList[index] = _mapper.Map<Character> (charFoundDto);
        serviceResponse.Data = _mapper.Map<GetCharacterDto> (charFoundDto);
      } catch (Exception ex) {
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter (int id) {
      ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>> ();
      try {
        var charFound = _characterList.FirstOrDefault (c => c.Id == id);
        if (charFound == null) {
          serviceResponse.Success = false;
          serviceResponse.Message = $"Character with id '{id}' not found!";
          return serviceResponse;
        }
        _characterList.RemoveAll (c => c.Id == id);
        serviceResponse.Data = (_characterList.Select (c => _mapper.Map<GetCharacterDto> (c))).ToList ();
      } catch (Exception ex) {

        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
      return serviceResponse;
    }
  }
}