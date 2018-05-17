using System.ComponentModel;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ShareIt.Models
{
    [DataContract]
    [Table("tracks")]
    public class BassTrack : INotifyPropertyChanged
    {

        #region props
        //Название

        [DataMember]
        [Key]
        [Column("trackid")]
        public int TrackId { get; set; }

        [DataMember]
        [Column("songname")]
        public string Name { get; set; }

        //Автор
        [DataMember]
        [Column("songauthor")]
        public string SongAuthor { get; set; }

        //Продолжительность
        [DataMember]
        [Column("duration")]
        public string Duration { get; set; }

        //Путь к файлу
        [DataMember]
        [Column("trackpath")]
        public string TrackPath { get; set; }

        //Битрейт
        [DataMember]
        [Column("bitrate")]
        public string Bitrate { get; set; }

        /*
        public ImageSource TrackCover
        {
            get { return TrackCover;  }
            protected set
            {
                TrackCover = value;
                NotifyPropertyChanged("TrackCover");
            }
        }*/

        #endregion

        #region constructor

        public BassTrack()
        { }

        public BassTrack(string path)
        {
            TagLib.File audioFile = TagLib.File.Create(path);

            if (audioFile.Tag.Title != null)
            {
                this.Name = audioFile.Tag.Title;
            }
            else
            {
                this.Name = new FileInfo(path).Name;
            }

            this.SongAuthor = audioFile.Tag.JoinedPerformers;
            this.Duration = audioFile.Properties.Duration.ToString("mm\\:ss");
            this.Bitrate = audioFile.Properties.AudioBitrate.ToString() + " Kb/s";
            this.TrackPath = path;
            this.TrackId = this.Name.GetHashCode() + this.Duration.GetHashCode();
        }

        #endregion

        #region functions

        public override string ToString()
        {
            return this.Name;
        }

        #endregion

        #region NotifyPropChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

    }
}
