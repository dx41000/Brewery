using Brewery.Datalayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Datalayer.Context;

public partial class BreweryContext : DbContext
{

    public BreweryContext()
    {
    }


    public BreweryContext(DbContextOptions<BreweryContext> options) : base(options)
    {
    }
    
    public virtual DbSet<Bar> Bars { get; set; }
    public virtual DbSet<Beer> Beers { get; set; }
    public virtual DbSet<Entities.Brewery> Breweries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Bar>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.ToTable("Bars");
            entity.Property(e => e.Name).IsRequired();
        });
        
        modelBuilder.Entity<Beer>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.ToTable("Beers");
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.PercentageAlcoholByVolume).IsRequired();

            entity.HasMany(x => x.Bars)
                .WithMany(x => x.Beers);

            entity.HasMany(x => x.Breweries)
                .WithMany(x => x.Beers);
        });
        
        modelBuilder.Entity<Entities.Brewery>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.ToTable("Breweries");
            entity.Property(e => e.Name).IsRequired();
            
        });



    }
}