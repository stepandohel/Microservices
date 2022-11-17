using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Domain.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityAlwaysColumn().ValueGeneratedOnAdd().HasColumnName("id");
            builder.Property(x => x.Name).IsRequired().HasColumnName("mame");
            builder.Property(x => x.Description).IsRequired().HasColumnName("description");

            builder.HasIndex(x => x.Id);
        }
    }
}
