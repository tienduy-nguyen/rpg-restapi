using System.Collections.Generic;
using System.Threading.Tasks;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public interface ICharacterService {
    Task<List<Character>> GetAllCharacters ();
    Task<Character> GetCharacterById (int id);
    Task<List<Character>> AddCharacter (Character newCharacter);

  }
}