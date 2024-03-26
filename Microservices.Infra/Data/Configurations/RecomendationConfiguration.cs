using Microservices.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Infra.Data.Configurations;
public class RecomendationConfiguration : IEntityTypeConfiguration<Recomendation>
{
    public void Configure(EntityTypeBuilder<Recomendation> builder)
    {
        builder.HasKey(_ => _.Id);
        builder.Property(_ => _.DeletedAt);
        builder.Property(r => r.Clothes)
               .IsRequired();
        builder.Property(r => r.Food)
               .IsRequired();
    }
}