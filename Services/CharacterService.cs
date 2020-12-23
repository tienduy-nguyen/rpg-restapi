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
      new Character { Id = 2, Name = "John" }
    };
    public Task<List<Character>> AddCharacter (Character newCharacter) {
      _characterList.Add (newCharacter);
      return Task.FromResult<List<Character>> (_characterList);
    }

    public Task<List<Character>> GetAllCharacters () {
      return Task.FromResult<List<Character>> (_characterList);
    }

    public Task<Character> GetCharacterById (int id) {
      return Task.FromResult<Character> (_characterList.FirstOrDefault (c => c.Id == id));
    }
  }
}