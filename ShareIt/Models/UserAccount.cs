namespace ShareIt.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserAccountContext : DbContext
    {
        // Your context has been configured to use a 'User' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ShareIt.Models.User' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'User' 
        // connection string in the application configuration file.
        public UserAccountContext()
            : base("name=UserAccount")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<UserAccount> Users;
    }

    public class UserAccount
    {
        private int userId { get; set; }
        private string Login { get; set; }
        private string Password { get; set; }

        private UserAccount(string login, string password)
        {
            
            this.Login = login;
            this.Password = password;

            this.userId = Login.GetHashCode();
        }
    }
}