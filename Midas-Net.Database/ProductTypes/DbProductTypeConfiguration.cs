using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.ProductTypes
{
    public class DbProductTypeConfiguration : IEntityTypeConfiguration<DbProductType>
    {
        public void Configure(EntityTypeBuilder<DbProductType> builder)
        {

            builder.ToTable("product_types");

            builder.HasKey(pt => pt.ProductTypeId);

            builder.Property(pt => pt.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(pt => pt.IsDeleted)
                .IsRequired();
        }
    }
}
