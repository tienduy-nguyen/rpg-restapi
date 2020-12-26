using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
  public class FightService : IFightService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public FightService (DataContext context, IMapper mapper) {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ServiceResponse<FightResultDto>> Fight (FightRequestDto request) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<List<HighscoreDto>>> GetHighScore () {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<AttackResultDto>> SkillAttack (SkillAttackDto request) {
      throw new System.NotImplementedException ();
    }

    public async Task<ServiceResponse<AttackResultDto>> WeaponAttack (WeaponAttackDto request) {
      throw new System.NotImplementedException ();
    }
  }
}