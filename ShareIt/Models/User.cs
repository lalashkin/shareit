namespace ShareIt.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("userdata")]
    public class User
    {
        [Key]
        [Column("userid")]
        public int UserId { get; set; }
        [Column("username")]
        public string UserName { get; set; }
        [Column("selectedprofile")]
        public string SelectedProfile { get; set; }
    }
}