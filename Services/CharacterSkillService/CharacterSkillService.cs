using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public class CharacterSkillService : ICharacterSkillService {
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    private int _getUserId () => int.Parse (
      _httpContextAccessor.HttpContext.User.FindFirstValue (ClaimTypes.NameIdentifier));
    public CharacterSkillService (DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
      _context = context;
      _mapper = mapper;
      _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill (AddCharacterSkillDto newCharacterSkillDto) {
      ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto> ();

      try {
        var character = await _context.Characters
          .Include (c => c.Weapon)
          .Include (c => c.CharacterSkills)
          .ThenInclude (cs => cs.Skill)
          .FirstOrDefaultAsync (c => c.Id == newCharacterSkillDto.CharacterId && c.UserId == _getUserId ());
        if (character == null) {
          response.Success = false;
          response.Message = $"Character with id '{newCharacterSkillDto.CharacterId}' not found!";
          return response;
        }
        Skill skill = await _context.Skills.FirstOrDefaultAsync (s => s.Id == newCharacterSkillDto.SkillId);
        if (skill == null) {
          response.Success = false;
          response.Message = $"Skill with id '{newCharacterSkillDto.SkillId}' not found!";
          return response;
        }

        CharacterSkill charSkill = _mapper.Map<CharacterSkill> (newCharacterSkillDto);
        charSkill.Character = character;
        charSkill.Skill = skill;

        await _context.CharacterSkills.AddAsync (charSkill);
        await _context.SaveChangesAsync ();
        response.Data = _mapper.Map<GetCharacterDto> (character);

      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }

      return response;
    }
  }
}