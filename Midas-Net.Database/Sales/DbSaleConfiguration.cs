using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Sales
{
    public class DbSaleConfiguration : IEntityTypeConfiguration<DbSale>
    {
        public void Configure(EntityTypeBuilder<DbSale> builder)
        {

            builder.ToTable("sales");

            builder.HasKey(s => s.SaleId);

            builder.Property(s => s.Date)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(s => s.IsCancelled)
                .IsRequired();

            builder.HasMany(s => s.SaleDetails)
                .WithOne(sd => sd.Sale)
                .HasForeignKey(sd => sd.SaleId)
                .IsRequired();
        }
    }
}
