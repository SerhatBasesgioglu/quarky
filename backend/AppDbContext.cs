using Microsoft.EntityFrameworkCore;

namespace Quarky;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options): base(options) { }
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<Concept> Concepts => Set<Concept>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Concept>()
            .HasMany(c => c.Dependencies)
            .WithMany()
            .UsingEntity(j => j.ToTable("ConceptDependencies"));
    }
}

