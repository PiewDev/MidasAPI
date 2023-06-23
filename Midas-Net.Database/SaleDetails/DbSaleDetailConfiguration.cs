using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.SaleDetails
{
    public class DbSaleDetailConfiguration : IEntityTypeConfiguration<DbSaleDetail>
    {
        public void Configure(EntityTypeBuilder<DbSaleDetail> builder)
        {

            builder.ToTable("sale_details");

            builder.HasKey(sd => sd.SaleDetailId);

            builder.Property(sd => sd.Quantity)
                .IsRequired();

            builder.Property(sd => sd.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(sd => sd.TotalPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.HasOne(sd => sd.Sale)
                .WithMany(s => s.SaleDetails)
                .HasForeignKey(sd => sd.SaleId)
                .IsRequired();

            builder.HasOne(sd => sd.Product)
                .WithMany()
                .HasForeignKey(sd => sd.ProductId)
                .IsRequired();
        }
    }
}
