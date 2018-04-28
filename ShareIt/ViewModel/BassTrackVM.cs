using System.ComponentModel;
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

        protected BindingList<BassTrack> tracksList = new BindingList<BassTrack> { };

        public BindingList<BassTrack> TracksList
        {
            get { return tracksList; }
            protected set
            {
                tracksList = value;
                //InotifyPropChanged?
            }
        }

        #endregion

        #region functions

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
