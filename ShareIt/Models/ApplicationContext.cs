using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareIt.Models
{
    class ApplicationContext : DbContext
    {

        public DbSet<Playlist> PlaylistSet { get; set; }
        public DbSet<User> UserDbContext { get; set; }
        public DbSet<UserAccount> UserAccountDbContext { get; set; }
        public DbSet<SettingsProfile> SettingsProfileDbContext { get; set; }
        public DbSet<BassTrack> TracksList { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreatedAsync();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=shareit;Username=postgres;Password=password");
        }
    }
}
