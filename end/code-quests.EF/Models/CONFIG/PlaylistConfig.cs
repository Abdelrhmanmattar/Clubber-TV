using code_quests.Core.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace code_quests.EF.Models.CONFIG
{
    public class PlaylistConfig : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.ToTable("Playlist");
            builder.HasKey(p => new { p.userID, p.matchID });
            builder.Property(p => p.userID).IsRequired();
            builder.Property(p => p.matchID).IsRequired();

            builder.HasOne<UserApp>(p => p.userApp)
                   .WithMany(u => u.Playlists)
                   .HasForeignKey(p => p.userID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<MatchEntity>(p => p.match)
                   .WithMany(m => m.Playlists)
                   .HasForeignKey(p => p.matchID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
