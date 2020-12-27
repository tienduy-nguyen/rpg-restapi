using System.Linq;
using AutoMapper;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi {
  public class AutoMapperProfile : Profile {
    public AutoMapperProfile () {
      /* Map character */
      CreateMap<Character, GetCharacterDto> ()
        .ForMember (dto => dto.Skills, c => c.MapFrom (c => c.CharacterSkills.Select (cs => cs.Skill)));
      CreateMap<AddCharacterDto, Character> ();
      CreateMap<UpdateCharacterDto, Character> ();
      CreateMap<Character, UpdateCharacterDto> ();

      /* Map User */
      CreateMap<User, GetUserDto> ();
      CreateMap<UpdateUserDto, User> ();

      CreateMap<AddWeaponDto, Weapon> ();
      CreateMap<Weapon, GetWeaponDto> ();
      CreateMap<AddCharacterSkillDto, CharacterSkill> ();
      CreateMap<Skill, GetSkillDto> ();
      CreateMap<Character, HighscoreDto> ();

      /* Map Skill */
      CreateMap<Skill, GetSkillDto> ();
      CreateMap<AddSkillDto, Skill> ();
      CreateMap<UpdateSkillDto, Skill> ();
      CreateMap<Skill, UpdateSkillDto> ();
    }
  }
}