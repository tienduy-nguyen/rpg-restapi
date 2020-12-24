using Microsoft.EntityFrameworkCore;
using Rpg_Restapi.Models;

namespace Rpg_Restapi.Data {
  public class DataContext : DbContext {
    public DataContext (DbContextOptions<DataContext> options) : base (options) { }
    public DbSet<Character> Characters { get; set; }
    public DbSet<User> Users { get; set; }

  }
}