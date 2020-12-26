using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

    /* Public methods */
    public async Task<ServiceResponse<AttackResultDto>> WeaponAttack (WeaponAttackDto request) {
      ServiceResponse<AttackResultDto> response = new ServiceResponse<AttackResultDto> ();
      try {
        Character attacker = await _context.Characters
          .Include (c => c.Weapon)
          .FirstOrDefaultAsync (c => c.Id == request.AttackerId);
        if (attacker == null) {
          response.Success = false;
          response.Message = $"Attacker with id '{request.AttackerId}' not found!";
          return response;
        }
        Character opponent = await _context.Characters
          .FirstOrDefaultAsync (c => c.Id == request.OpponentId);
        if (opponent == null) {
          response.Success = false;
          response.Message = $"Opponent with id '{request.OpponentId}' not found!";
          return response;
        }
        int damage = _DoWeaponAttack (attacker, opponent);
        if (opponent.HitPoints <= 0) {
          response.Message = $"{opponent.Name} has been defeated!";
        }
        _context.Characters.Update (opponent);
        await _context.SaveChangesAsync ();
        var attackResultDto = new AttackResultDto {
          Attacker = attacker.Name,
          AttackerHP = attacker.HitPoints,
          Opponent = attacker.Name,
          OpponentHP = opponent.HitPoints,
          Damage = damage
        };
        response.Data = attackResultDto;

      } catch (System.Exception ex) {

        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }
    public async Task<ServiceResponse<AttackResultDto>> SkillAttack (SkillAttackDto request) {
      throw new System.NotImplementedException ();

    }

    public async Task<ServiceResponse<List<HighscoreDto>>> GetHighScore () {
      throw new System.NotImplementedException ();
    }
    public async Task<ServiceResponse<FightResultDto>> Fight (FightRequestDto request) {
      throw new System.NotImplementedException ();
    }

    /* Private helpers methods */
    /// <summary>
    /// Weapon attack between 2 characters
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="opponent"></param>
    /// <returns>Damage affect to opponent</returns>
    private static int _DoWeaponAttack (Character attacker, Character opponent) {
      // Random value between 0 and the Strength of attacker
      int damage = attacker.Weapon.Damage + (new Random ().Next (attacker.Strength));
      damage -= new Random ().Next (opponent.Defense);
      if (damage > 0) {
        opponent.HitPoints -= damage;
      }
      if (damage < 0) damage = 0;
      return damage;
    }

    /// <summary>
    /// Skill attack between 2 characters
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="opponent"></param>
    /// <param name="characterSkill"></param>
    /// <returns>Damage affect to opponent</returns>
    private static int _DoSkillAttack (Character attacker, Character opponent, CharacterSkill characterSkill) {
      int damage = characterSkill.Skill.Damage + (new Random ().Next (attacker.Intelligence));
      damage -= new Random ().Next (opponent.Defense);
      if (damage > 0) {
        opponent.HitPoints -= damage;
      }
      if (damage < 0) damage = 0;
      return damage;
    }

  }
}