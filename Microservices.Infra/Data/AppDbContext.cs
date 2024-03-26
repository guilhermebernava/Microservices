using Microservices.Domain.Entities;
using Microservices.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Microservices.Infra.Data;
public class AppDbContext : DbContext
{
    public DbSet<Recomendation> Recomendations { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RecomendationConfiguration());
    }
}

