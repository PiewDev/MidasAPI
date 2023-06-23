using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Midas.Net.Database.ProductTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Products
{
    public class DbProductConfiguration : IEntityTypeConfiguration<DbProduct>
    {
        public void Configure(EntityTypeBuilder<DbProduct> builder)
        {

            builder.ToTable("products");

            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.Name)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(p => p.TypeId)
                .IsRequired();

            builder.Property(p => p.Stock)
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .IsRequired();

            builder.HasOne(p => p.Type)
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .IsRequired();
        }
    }
   
}
