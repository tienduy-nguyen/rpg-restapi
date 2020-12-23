using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public interface ICharacterService {
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters ();
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById (int id);
    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter (AddCharacterDto newCharacterDto);
    Task<ServiceResponse<GetCharacterDto>> UpdateCharacter (UpdateCharacterDto updatedCharacter);
    Task<ServiceResponse<GetCharacterDto>> UpdatePartialCharacter (int id, JsonPatchDocument<UpdateCharacterDto> updatedCharacter);
    Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter (int id);
  }
}