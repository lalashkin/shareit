namespace ShareIt.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("useraccounts")]
    public class UserAccount
    {
        [Key]
        [Column("userid")]
        public int UserId { get; set; }
        [MaxLength(100), Required, Column("login")]
        public string Login { get; set; }
        [MaxLength(50), Required, Column("password")]
        public string Password { get; set; }

        public UserAccount()
        { }

        public UserAccount(string login, string password)
        {
            this.Login = login;
            this.Password = password;

            this.UserId = Login.GetHashCode();
        }
    }
}