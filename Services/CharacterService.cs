using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {

  public class CharacterService : ICharacterService {

    private static List<CharacterDto> _characterList = new List<CharacterDto> {
      new CharacterDto (),
      new CharacterDto { Id = 1, Name = "Same" },
      new CharacterDto { Id = 2, Name = "Paul" },
      new CharacterDto { Id = 3, Name = "John" }
    };

    public async Task<ServiceResponse<List<CharacterDto>>> AddCharacter (AddCharacterDto newCharacterDto) {
      ServiceResponse<List<CharacterDto>> serviceResponse = new ServiceResponse<List<CharacterDto>> ();
      _characterList.Add (newCharacterDto);
      serviceResponse.Data = _characterList;
      return serviceReponse;
    }

    public async Task<List<CharacterDto>> GetAllCharacters () {
      return _characterList;
    }

    public async Task<CharacterDto> GetCharacterById (int id) {
      return _characterList.FirstOrDefault (c => c.Id == id);
    }
  }
}