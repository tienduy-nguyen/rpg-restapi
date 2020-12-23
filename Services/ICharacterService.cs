using System.Collections.Generic;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public interface ICharacterService {
    List<Character> GetAllCharacters ();
    Character GetCharacterById (int id);
    List<Character> AddCharacter (Character newCharacter);

  }
}