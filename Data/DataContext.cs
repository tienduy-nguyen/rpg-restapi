using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Rpg_Restapi.Data;
using Rpg_Restapi.Models;
using Rpg_Restapi.Utilities;

namespace Rpg_Restapi.Data {
  public class DataContext : DbContext {
    public DataContext (DbContextOptions<DataContext> options) : base (options) { }
    public DbSet<Character> Characters { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Weapon> Weapons { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<CharacterSkill> CharacterSkills { get; set; }

    protected override void OnModelCreating (ModelBuilder modelBuilder) {

      modelBuilder.Entity<User> (entity => {
        entity.HasIndex (u => u.Username).IsUnique ();
        entity.Property (user => user.Role).HasDefaultValue ("Player");
      });

      modelBuilder.Entity<Skill> ().HasData (
        new Skill { Id = 1, Name = "Fireball", Damage = 30 },
        new Skill { Id = 2, Name = "Frenzy", Damage = 20 },
        new Skill { Id = 3, Name = "Blizzard", Damage = 50 }
      );

      modelBuilder.Entity<Weapon> ().HasData (
        new Weapon { Uuid = Guid.NewGuid (), Name = "The Master Sword", Damage = 20 },
        new Weapon { Uuid = Guid.NewGuid (), Name = "Crystal Wand", Damage = 5 }
      );

      modelBuilder.Entity<CharacterSkill> ()
        .HasKey (cs => new { cs.CharacterId, cs.SkillId });

      Security.CreatePasswordHash ("1234567", out byte[] passwordHash, out byte[] passwordSalt);

      modelBuilder.Entity<User> ().HasData (
        new User { Id = 1, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Username = "user1" },
        new User { Id = 2, PasswordHash = passwordHash, PasswordSalt = passwordSalt, Username = "user2" }
      );

      modelBuilder.Entity<Character> ().HasData (
        new Character {
          Id = 1,
            Name = "Frodo",
            Class = RpgClass.Knight,
            HitPoints = 100,
            Strength = 15,
            Defense = 10,
            Intelligence = 10,
            UserId = 1
        },
        new Character {
          Id = 2,
            Name = "Raistlin",
            Class = RpgClass.Mage,
            HitPoints = 100,
            Strength = 5,
            Defense = 5,
            Intelligence = 20,
            UserId = 2
        }
      );

      modelBuilder.Entity<CharacterSkill> ().HasData (
        new CharacterSkill { CharacterId = 1, SkillId = 2 },
        new CharacterSkill { CharacterId = 2, SkillId = 1 },
        new CharacterSkill { CharacterId = 2, SkillId = 3 }
      );

    }

  }
}