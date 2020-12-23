using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {

  public class CharacterService : ICharacterService {

    private static List<Character> _characterList = new List<Character> {
      new Character (),
      new Character { Id = 1, Name = "Same" },
      new Character { Id = 2, Name = "Paul" },
      new Character { Id = 3, Name = "John" }
    };
    public async Task<List<Character>> AddCharacter (Character newCharacter) {
      _characterList.Add (newCharacter);
      return _characterList;
    }

    public async Task<List<Character>> GetAllCharacters () {
      return _characterList;
    }

    public async Task<Character> GetCharacterById (int id) {
      return _characterList.FirstOrDefault (c => c.Id == id);
    }
  }
}