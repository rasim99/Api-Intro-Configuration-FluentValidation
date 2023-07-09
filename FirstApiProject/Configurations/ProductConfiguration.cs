using FirstApiProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstApiProject.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(20);
            builder.Property(p => p.SalePrice).IsRequired(true).HasDefaultValue(50);
            builder.Property(p => p.CostPrice).IsRequired(true).HasDefaultValue(10);
           // builder.Property(p => p.CreatedDate).HasDefaultValueSql("getutcdate()");
        }
    }
    
}
