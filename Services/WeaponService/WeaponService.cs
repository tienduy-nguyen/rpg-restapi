using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services.WeaponService {
  public class WeaponService : IWeaponService {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private int _getUserId () => int.Parse (_httpContextAccessor.HttpContext.User.FindFirstValue (ClaimTypes.NameIdentifier));

    public WeaponService (DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
      _context = context;
      _mapper = mapper;
      _httpContextAccessor = httpContextAccessor;
    }
    public async Task<ServiceResponse<GetCharacterDto>> AddWeapon (AddWeaponDto newWeaponDto) {
      ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto> ();
      try {
        Character character = await _context.Characters
          .FirstOrDefaultAsync (c => c.Id == newWeaponDto.CharacterId && c.UserId == _getUserId ());
        if (character == null) {
          response.Success = false;
          response.Message = $"Character with '{newWeaponDto.CharacterId}' not found!";
          return response;
        }
        Weapon weapon = _mapper.Map<Weapon> (newWeaponDto);
        weapon.Character = character;
        await _context.Weapons.AddAsync (weapon);
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