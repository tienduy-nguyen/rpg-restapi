using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;
namespace Rpg_Restapi.Services {
  public class SkillService : ISkillService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public SkillService (DataContext context, IMapper mapper) {
      _context = context;
      _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills () {

      ServiceResponse<List<GetSkillDto>> response = new ServiceResponse<List<GetSkillDto>> ();
      var skillList = await _context.Skills.ToListAsync ();
      response.Data = skillList.Select (s => _mapper.Map<GetSkillDto> (s)).ToList ();
      return response;
    }

    public async Task<ServiceResponse<GetSkillDto>> GetSkillById (int id) {
      ServiceResponse<GetSkillDto> response = new ServiceResponse<GetSkillDto> ();
      try {
        var skill = await _context.Skills.FirstOrDefaultAsync (s => s.Id == id);
        if (skill == null) {
          response.Success = false;
          response.Message = $"Skill with id {id} not found";
          return response;
        }
        response.Data = _mapper.Map<GetSkillDto> (skill);

      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    public async Task<ServiceResponse<List<GetSkillDto>>> AddSkill (AddSkillDto newSkillDto) {
      ServiceResponse<List<GetSkillDto>> response = new ServiceResponse<List<GetSkillDto>> ();
      try {
        var skill = _mapper.Map<Skill> (newSkillDto);
        await _context.Skills.AddAsync (skill);
        await _context.SaveChangesAsync ();
        var skillList = await _context.Skills.ToListAsync ();
        response.Data = skillList.Select (s => _mapper.Map<GetSkillDto> (s)).ToList ();

      } catch (System.Exception ex) {

        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    public async Task<ServiceResponse<GetSkillDto>> UpdateSkill (int id, UpdateSkillDto updateSkillDto) {
      ServiceResponse<GetSkillDto> response = new ServiceResponse<GetSkillDto> ();
      try {
        var skill = await _context.Skills.FirstOrDefaultAsync (s => s.Id == id);
        if (skill == null) {
          response.Success = false;
          response.Message = $"Skill with id {id} not found";
          return response;
        }
        var updateSkill = _mapper.Map<Skill> (updateSkillDto);
        _context.Entry (updateSkill).State = EntityState.Modified;
        await _context.SaveChangesAsync ();
        response.Data = _mapper.Map<GetSkillDto> (updateSkill);

      } catch (System.Exception ex) {

        response.Success = false;
        response.Message = ex.Message;
      }

      return response;
    }

    public async Task<ServiceResponse<List<GetSkillDto>>> DeleteSkill (int id) {
      ServiceResponse<List<GetSkillDto>> response = new ServiceResponse<List<GetSkillDto>> ();
      try {
        var skill = await _context.Skills.FirstOrDefaultAsync (s => s.Id == id);
        if (skill == null) {
          response.Success = false;
          response.Message = $"Skill with id {id} not found";
          return response;
        }
        _context.Skills.Remove (skill);
        await _context.SaveChangesAsync ();
        var skillList = await _context.Skills.ToListAsync ();
        response.Data = skillList.Select (s => _mapper.Map<GetSkillDto> (s)).ToList ();

      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;
      }

      return response;
    }
  }
}