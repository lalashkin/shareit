namespace ShareIt.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("settingsprofiles")]
    public class SettingsProfile
    {
        [Key]
        [Column("profilename")]
        public string ProfileName { get; set; }
        [Column("ownerid")]
        public int OwnerId { get; set; }
        [Column("playlistpath")]
        public string PlaylistPath { get; set; }
        [Column("equalaiservalues")]
        public decimal[] EqualaiserValues { get; set; }
        //Тут значения настроек

        public SettingsProfile()
        {

        }

        public SettingsProfile(int uid, string profileName, string playlistPath, decimal[] eqValues)
        {
            EqualaiserValues = eqValues;
            PlaylistPath = playlistPath;
            ProfileName = profileName;
            OwnerId = uid;
        }

        public override string ToString()
        {
            return ProfileName;
        }
    }
}