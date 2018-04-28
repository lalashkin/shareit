using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Data.Entity;
using System.Windows.Media.Imaging;

namespace ShareIt.Models
{
    public class BassTrackContext : DbContext
    {
        public BassTrackContext()
            : base("name=track")
        { }

        public DbSet<BassTrack> TracksList;
    }

    public class BassTrack : INotifyPropertyChanged
    {

        #region variables
        //Название

        private string name;

        public string Name
        {
            get { return name; }
            protected set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        //Автор

        private string songAuthor;

        public string SongAuthor
        {
            get { return songAuthor; }
            protected set
            {
                songAuthor = value;
                NotifyPropertyChanged("SongAuthor");
            }
        }

        //Продолжительность

        private string duration;

        public string Duration
        {
            get { return duration; }
            protected set
            {
                duration = value;
                NotifyPropertyChanged("Duration");
            }
        }

        //Путь к файлу

        private string trackPath;

        public string TrackPath
        {
            get { return trackPath; }
            protected set
            {
                trackPath = value;
                NotifyPropertyChanged("TrackPath");
            }
        }

        //Битрейт

        private string bitrate;

        public string Bitrate
        {
            get { return bitrate; }
            protected set
            {
                bitrate = value;
                NotifyPropertyChanged("Bitrate");
            }
        }

        private ImageSource trackCover;

        public ImageSource TrackCover
        {
            get { return trackCover;  }
            protected set
            {
                trackCover = value;
                NotifyPropertyChanged("TrackCover");
            }
        }

        #endregion

        #region constructor

        public BassTrack()
        {

        }

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
