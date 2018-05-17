using System.ComponentModel;
using System.Windows;
using ShareIt.Models;
using ShareIt.View.MainPlayer;

namespace ShareIt.ViewModel
{
    public class BassTrackVM
    {
        #region constructors

        public BassTrackVM()
        { }

        #endregion

        #region variables

        protected static BindingList<BassTrack> tracksList = new BindingList<BassTrack> { };

        public static BindingList<BassTrack> TracksList
        {
            get { return tracksList; }
            set
            {
                tracksList = value;
                //InotifyPropChanged?
            }
        }

        #endregion

        #region functions

        public void AddTrack(string path)
        {
            try
            {
                if (TagLib.File.Create(path).MimeType == "taglib/mp3" ||
                    TagLib.File.Create(path).MimeType == "taglib/flac" ||
                    TagLib.File.Create(path).MimeType == "taglib/wav")
                {
                    tracksList.Add(new BassTrack(path));
                }
            }
            catch (TagLib.UnsupportedFormatException)
            {
                return;
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("No such file found at:\n " + path, "File not found!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
           
        }

        public void AddTrack(BassTrack track)
        {
            tracksList.Add(track);
        }

        public void PlayTrack(BassTrack track)
        {
            BassPlayerControls.MediaPlayer.OpenFile(track.TrackPath);
            BassPlayerControls.MediaPlayer.Play();
        }

        #endregion
    }
}
