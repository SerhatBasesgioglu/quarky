using Microsoft.EntityFrameworkCore;

namespace Quarky;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options): base(options) { }
    public DbSet<Note> Notes => Set<Note>();
}
