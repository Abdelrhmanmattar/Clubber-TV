using code_quests.Core.entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using code_quests.EF.Models.CONFIG;

namespace code_quests.EF.Models.DATA
{
    public class AppDbContext: IdentityDbContext<UserApp>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

            try
            {
                var dbcreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbcreator != null)
                {
                    if (!dbcreator.CanConnect())
                        dbcreator.Create();
                    if (!dbcreator.HasTables())
                        dbcreator.CreateTables();
                }

            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }

        public DbSet<MatchEntity> Matches { get; set; }
        public DbSet<Playlist> playlists { get; set; }
        public DbSet<UserApp> userApps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserAppConfig());
            builder.ApplyConfiguration(new PlaylistConfig());
            builder.ApplyConfiguration(new MatchConfig());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
    }
}
