namespace ShareIt.Models
{
    using System;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Linq;

    public class PlaylistContext : DbContext
    {
        // Your context has been configured to use a 'Playlist' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ShareIt.Models.Playlist' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Playlist' 
        // connection string in the application configuration file.
        public PlaylistContext()
            : base("name=Playlist")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<Playlist> PlaylistSet;
    }

    public class Playlist
    {
        public string PlaylistName { get; set; }
        public BindingList<BassTrack> TracksList { get; set; }
    }
}