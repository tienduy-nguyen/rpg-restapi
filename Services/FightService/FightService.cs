using System;
using System.Collections.Generic;
using System.Linq;
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
          response.Message = $"Attacker with id {request.AttackerId} not found!";
          return response;
        }
        Character opponent = await _context.Characters
          .FirstOrDefaultAsync (c => c.Id == request.OpponentId);
        if (opponent == null) {
          response.Success = false;
          response.Message = $"Opponent with id {request.OpponentId} not found!";
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
      ServiceResponse<AttackResultDto> response = new ServiceResponse<AttackResultDto> ();

      try {
        Character attacker = await _context.Characters
          .Include (c => c.CharacterSkills).ThenInclude (cs => cs.Skill)
          .FirstOrDefaultAsync (c => c.Id == request.AttackerId);
        if (attacker == null) {
          response.Success = false;
          response.Message = $"Attacker with id {request.AttackerId} not found!";
          return response;
        }
        Character opponent = await _context.Characters
          .FirstOrDefaultAsync (c => c.Id == request.OpponentId);
        if (opponent == null) {
          response.Success = false;
          response.Message = $"Opponent with id {request.OpponentId} not found!";
          return response;
        }
        CharacterSkill characterSkill =
          attacker.CharacterSkills.FirstOrDefault (cs => cs.Skill.Id == request.SkillId);
        if (characterSkill == null) {
          response.Success = false;
          response.Message = $"{attacker.Name} doesn't know that skill.";
          return response;
        }

        int damage = _DoSkillAttack (attacker, opponent, characterSkill);
        if (opponent.HitPoints <= 0) {
          response.Message = $"{opponent.Name} has been defeated";
        }
        _context.Characters.Update (opponent);
        await _context.SaveChangesAsync ();
        response.Data = new AttackResultDto {
          Attacker = attacker.Name,
          AttackerHP = attacker.HitPoints,
          Opponent = opponent.Name,
          OpponentHP = opponent.HitPoints,
          Damage = damage
        };
      } catch (System.Exception ex) {

        response.Success = false;
        response.Message = ex.Message;
      }
      return response;

    }
    public async Task<ServiceResponse<FightResultDto>> Fight (FightRequestDto request) {
      ServiceResponse<FightResultDto> response = new ServiceResponse<FightResultDto> {
        Data = new FightResultDto ()
      };
      try {
        List<Character> characters = await _context.Characters
          .Include (c => c.Weapon)
          .Include (c => c.CharacterSkills).ThenInclude (cs => cs.Skill)
          .Where (c => request.CharacterIds.Contains (c.Id)).ToListAsync ();

        bool defeated = false;
        // The while loop stops when the first character is defeated
        while (!defeated) {
          foreach (Character attacker in characters) {
            List<Character> opponents = characters.Where (c => c.Id != attacker.Id).ToList ();
            // Get random opponent to attack, random index from 0 to count item of list opponent
            Character opponent = opponents[new Random ().Next (0, opponents.Count)];

            int damage = 0;
            string attackUsed = string.Empty;
            // Random between 0 & 1, if == 0, choose weapon
            bool useWeapon = new Random ().Next (0, 2) == 0;
            if (useWeapon) {
              attackUsed = attacker.Weapon.Name;
              damage = _DoWeaponAttack (attacker, opponent);
            } else {
              int randomSkill = new Random ().Next (0, attacker.CharacterSkills.Count);
              attackUsed = attacker.CharacterSkills[randomSkill].Skill.Name;
              damage = _DoSkillAttack (attacker, opponent, attacker.CharacterSkills[randomSkill]);
            }

            response.Data.Log.Add ($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage.");

            if (opponent.HitPoints <= 0) {
              defeated = true;
              attacker.Victories++;
              opponent.Defeats++;
              response.Data.Log.Add ($"{opponent.Name} has been defeated!");
              response.Data.Log.Add ($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
              break;
            }
          }
        }
        // Increase count fights & reset Hitpoints
        characters.ForEach (c => {
          c.Fights++;
          c.HitPoints = 100;
        });
        _context.Characters.UpdateRange (characters);
        await _context.SaveChangesAsync ();

      } catch (System.Exception ex) {
        response.Message = ex.Message;
        response.Success = false;

      }
      return response;
    }

    public async Task<ServiceResponse<List<HighscoreDto>>> GetHighscore () {
      List<Character> characters = await _context.Characters
        .Where (c => c.Fights > 0)
        .OrderByDescending (c => c.Victories)
        .ThenBy (c => c.Defeats)
        .ToListAsync ();

      var response = new ServiceResponse<List<HighscoreDto>> {
        Data = characters.Select (c => _mapper.Map<HighscoreDto> (c)).ToList ()
      };

      return response;
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