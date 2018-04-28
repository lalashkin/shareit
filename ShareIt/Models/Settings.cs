namespace ShareIt.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SettingsContext : DbContext
    {
        // Your context has been configured to use a 'Settings' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ShareIt.Models.Settings' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Settings' 
        // connection string in the application configuration file.
        public SettingsContext()
            : base("name=Settings")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    public class Settings
    {

    }
}