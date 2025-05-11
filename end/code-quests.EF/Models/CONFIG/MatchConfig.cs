using code_quests.Core.entities;
using code_quests.Core.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_quests.EF.Models.CONFIG
{
    public class MatchConfig : IEntityTypeConfiguration<MatchEntity>
    {
        public void Configure(EntityTypeBuilder<MatchEntity> builder)
        {
            builder.ToTable("Match");

            builder.HasKey(m => m.ID);
            builder.Property(m => m.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(m => m.title)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.competition)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(m => m.date)
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(m => m.status)
                .HasConversion<byte>()
                .HasDefaultValue(Status.Replay)
                .IsRequired();
        }
    }
}
