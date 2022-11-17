using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Domain.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd().HasColumnName("id");
            builder.Property(x => x.Name).IsRequired().HasColumnName("mame");
            builder.Property(x => x.Description).IsRequired().HasColumnName("description");
            builder.Property(x => x.Cost).IsRequired().HasColumnName("cost");
            builder.Property(x => x.CategoryId).IsRequired().HasColumnName("fk_Order_Category_CategoryId");

            builder.HasIndex(x => x.Id);
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
        }
    }
}