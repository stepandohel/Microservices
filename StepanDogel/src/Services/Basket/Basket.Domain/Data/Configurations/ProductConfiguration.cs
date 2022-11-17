using Basket.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Domain.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd();
            builder.Property(x => x.Price).IsRequired();

            builder.HasMany(x => x.Orders).WithMany(x => x.Products);

            builder.HasIndex(x => x.Id);
        }
    }
}
