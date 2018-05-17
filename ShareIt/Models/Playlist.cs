using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ShareIt.Models
{
    [DataContract]
    [Table("playlists")]
    public class Playlist
    {
        [Column("playlistid")]
        [Key]
        public int PlaylistId { get; set; }
        [DataMember]
        [Column("ownerid")]
        public int OwnerId { get; set; }
        [Column("tracks")]
        public List<int> PlaylistTracks { get; set; }
        [DataMember]
        [NotMapped]
        public BindingList<BassTrack> PlaylistTracksSerialized { get; set; }
        [DataMember]
        [Column("playlistname")]
        public string PlaylistName { get; set; }

        public Playlist()
        {

        }

        public Playlist(int ownerid, BindingList<BassTrack> tracks, string playlistName)
        {
            this.PlaylistTracks = new List<int>() { };
            this.PlaylistTracksSerialized = new BindingList<BassTrack>() { };

            this.OwnerId = ownerid;
            foreach(BassTrack track in tracks)
            {
                this.PlaylistTracks.Add(track.TrackId);
            }
            foreach (BassTrack track in tracks)
            {
                this.PlaylistTracksSerialized.Add(track);
            }
            this.PlaylistName = playlistName;

            this.PlaylistId = PlaylistName.GetHashCode() + PlaylistTracks.GetHashCode();
        }

        public override string ToString()
        {
            return PlaylistName;
        }
    }
}