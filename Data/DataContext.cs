using Microsoft.EntityFrameworkCore;
using Rpg_Restapi.Data;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Data {
  public class DataContext : DbContext {
    public DataContext (DbContextOptions<DataContext> options) : base (options) { }
    public DbSet<Character> Characters { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Weapon> Weapons { get; set; }

    protected override void OnModelCreating (ModelBuilder modelBuilder) {
      modelBuilder.Entity<User> ()
        .Property (user => user.Role).HasDefaultValue ("Player");

      // Utilities.CreatePasswordHash ("1234567", out byte[] passwordHash, out byte[] passwordSalt);
    }

  }
}