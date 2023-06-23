using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Log
{
    public class DbLogConfiguration : IEntityTypeConfiguration<DbLog>
    {
        public void Configure(EntityTypeBuilder<DbLog> builder)
        {
            builder.ToTable("logs");

            builder.HasKey(log => log.Id);

            builder.Property(log => log.Guid)
                .HasMaxLength(50);

            builder.Property(log => log.LogText)
                .HasMaxLength(int.MaxValue);

            builder.Property(log => log.Origin)
                .HasMaxLength(255);

            builder.Property(log => log.Uri)
                .HasMaxLength(500);

            builder.Property(log => log.Header)
            .HasMaxLength(int.MaxValue);

            builder.Property(log => log.Type)
                .HasMaxLength(50);

            builder.Property(log => log.UserOrigin)
                .HasMaxLength(500);

            builder.Property(log => log.StatusCode)
                .HasMaxLength(100);

            builder.Property(log => log.Body)
                .HasMaxLength(int.MaxValue);
        }
    }
}
