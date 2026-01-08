using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderingDomain.Models;
using OrderingDomain.ValueObjects;

namespace OrderingInfrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasConversion(productId => productId.Value, dbId => ProductId.Of(dbId)); //extrapts and wraps around strongly-typed ID

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

        }
    }
}
