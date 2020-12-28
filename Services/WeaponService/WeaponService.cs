using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rpg_Restapi.Data;
using Rpg_Restapi.Dtos;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Services {
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

    public async Task<ServiceResponse<List<GetWeaponDto>>> GetAllWeapons () {
      ServiceResponse<List<GetWeaponDto>> response = new ServiceResponse<List<GetWeaponDto>> ();
      var weaponList = await _context.Weapons.ToListAsync ();
      response.Data = weaponList.Select (w => _mapper.Map<GetWeaponDto> (w)).ToList ();
      response.Success = true;
      return response;
    }

    public async Task<ServiceResponse<GetWeaponDto>> CreateNewWeapon (CreateWeaponDto newWeaponDto) {
      ServiceResponse<GetWeaponDto> response = new ServiceResponse<GetWeaponDto> ();
      try {
        Weapon weapon = _mapper.Map<Weapon> (newWeaponDto);
        weapon.Id = Guid.NewGuid ();
        _context.Weapons.Add (weapon);
        await _context.SaveChangesAsync ();
        response.Data = _mapper.Map<GetWeaponDto> (weapon);

      } catch (System.Exception ex) {
        response.Success = false;
        response.Message = ex.Message;

      }
      return response;
    }
  }
}