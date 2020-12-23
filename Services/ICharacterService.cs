using System.Collections.Generic;
using System.Threading.Tasks;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public interface ICharacterService {
    Task<ServiceResponse<List<CharacterDto>>> GetAllCharacters ();
    Task<ServiceResponse<CharacterDto>> GetCharacterById (int id);
    Task<ServiceResponse<List<CharacterDto>>> AddCharacter (Character newCharacter);

  }
}