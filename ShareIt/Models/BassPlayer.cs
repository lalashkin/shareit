using ShareIt.View;
using System;
using System.ComponentModel;
using System.IO;
using Un4seen.Bass;
using WPFSoundVisualizationLib;

namespace ShareIt.Models
{
    public class BassPlayer : ISoundPlayer, ISpectrumPlayer
    {

        #region constructors

        public BassPlayer()
        {
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
        }

        #endregion

        #region variables

        private bool isPlaying;

        public bool IsPlaying
        {
            get { return isPlaying; }
            protected set
            {
                bool oldValue = isPlaying;
                isPlaying = value;
                if (oldValue != isPlaying)
                    NotifyPropertyChanged("IsPlaying");
            }
        }

        private int activeStreamHandle;

        public int ActiveStreamHandle
        {
            get { return activeStreamHandle; }
            protected set
            {
                int oldValue = activeStreamHandle;
                activeStreamHandle = value;
                if (oldValue != activeStreamHandle)
                {
                    NotifyPropertyChanged("ActiveStreamHandle");
                }
            }
        }

        private bool canPlay;

        public bool CanPlay
        {
            get { return canPlay; }
            protected set
            {
                bool oldValue = canPlay;
                canPlay = value;
                if (oldValue != canPlay)
                    NotifyPropertyChanged("CanPlay");
            }
        }

        private bool canPause;

        public bool CanPause
        {
            get { return canPause; }
            protected set
            {
                bool oldValue = canPause;
                canPause = value;
                if (oldValue != canPause)
                    NotifyPropertyChanged("CanPause");
            }
        }

        private bool canStop;

        public bool CanStop
        {
            get { return canStop; }
            protected set
            {
                bool oldValue = canStop;
                canStop = value;
                if (oldValue != canStop)
                    NotifyPropertyChanged("CanStop");
            }
        }

        private double streamFileLength;
        
        public double StreamFileLength
        {
            get { return streamFileLength; }
            protected set
            {
                double oldValue = streamFileLength;
                streamFileLength = value;
                if (oldValue != streamFileLength)
                    NotifyPropertyChanged("StreamFileLength");
            }
        }

        #endregion

        #region functions

        public void Stop()
        {
            if(ActiveStreamHandle != 0)
            {
                Bass.BASS_ChannelStop(ActiveStreamHandle);
                Bass.BASS_ChannelSetPosition(ActiveStreamHandle, 0);
            }
            IsPlaying = false;
            CanPlay = true;
            CanStop = false;
            CanPause = true;
        }

        public void Pause()
        {
            if(IsPlaying && CanPause)
            {
                Bass.BASS_ChannelPause(ActiveStreamHandle);
                IsPlaying = false;
                CanPlay = true;
                CanStop = true;
                CanPause = false;
            }
        }

        public void Play()
        {
            if(CanPlay)
            {
                PlayCurrentStream();
                IsPlaying = true;
                CanPlay = false;
                CanStop = true;
                CanPause = true;
            }
        }

        private void PlayCurrentStream()
        {
            if (ActiveStreamHandle != 0)
            {
                Bass.BASS_ChannelPlay(ActiveStreamHandle, false);
            }
        }

        public void SetCurrentStreamPosition(double value)
        {
            if(ActiveStreamHandle != 0)
            {
                Bass.BASS_ChannelSetPosition(ActiveStreamHandle, value);
            }
        }

        public void CheckCurrentStreamActive()
        {
            if (isPlaying && Bass.BASS_ChannelIsActive(ActiveStreamHandle) != BASSActive.BASS_ACTIVE_PLAYING)
            {
                MediaEnded(this, new EventArgs());
            }
        }

        public double GetStreamCurrentTime()
        {
           return Bass.BASS_ChannelBytes2Seconds(ActiveStreamHandle, Bass.BASS_ChannelGetPosition(ActiveStreamHandle));
        }

        public void SetChannelVolume(float value)
        {
            /* 
             * 
             * Работет весело, но не так как я хочу :c
             * 
             * 
            
            BASS_FX_VOLUME_PARAM volume = new BASS_FX_VOLUME_PARAM(value, -1, 0, 0);
            int fxVolumeHandle = Bass.BASS_ChannelSetFX(ActiveStreamHandle, BASSFXType.BASS_FX_VOLUME, 1);
            Bass.BASS_FXSetParameters(fxVolumeHandle, volume);

            */
            Bass.BASS_SetVolume(value);
        }

        public bool OpenFile(string path)
        {
            Stop();

            if(ActiveStreamHandle != 0)
            {
                Bass.BASS_StreamFree(ActiveStreamHandle);
            }

            if(File.Exists(path))
            {
                ActiveStreamHandle = Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_DEFAULT);

                if (ActiveStreamHandle != 0)
                {
                    //Текущая частота для Аналогового спектра
                    BASS_CHANNELINFO info = new BASS_CHANNELINFO();
                    Bass.BASS_ChannelGetInfo(ActiveStreamHandle, info);
                    sampleFrequency = info.freq;

                    //Размер текущей композиции в секундах
                    StreamFileLength = Bass.BASS_ChannelBytes2Seconds(ActiveStreamHandle,
                                                                      Bass.BASS_ChannelGetLength(ActiveStreamHandle));
                    //Event Media Opened
                    MediaOpened(this, new EventArgs());

                    CanPlay = true;
                    return true;
                }
                else
                {
                    ActiveStreamHandle = 0;
                    CanPlay = false;
                }
            }
            return false;
        }

        public void PlayNext(BindingList<BassTrack> tracksList)
        {
            if (tracksList.Count != 0)
            {

                try
                {
                    TrackListView.currentTrackIndex += 1;
                    OpenFile(tracksList[TrackListView.currentTrackIndex].TrackPath);
                }
                catch (ArgumentOutOfRangeException)
                {
                    //log exceptt
                    TrackListView.currentTrackIndex = 0;
                }
                finally
                {
                    OpenFile(tracksList[TrackListView.currentTrackIndex].TrackPath);
                    Play();
                }

            }
        }

        public void PlayPrev(BindingList<BassTrack> tracksList)
        {
            if (tracksList.Count != 0)
            {

                try
                {
                    TrackListView.currentTrackIndex -= 1;
                    OpenFile(tracksList[TrackListView.currentTrackIndex].TrackPath);
                }
                catch (ArgumentOutOfRangeException)
                {
                    //log except
                    TrackListView.currentTrackIndex = tracksList.Count - 1;
                }
                finally
                {
                    OpenFile(tracksList[TrackListView.currentTrackIndex].TrackPath);
                    Play();
                }

            }
        }

        #endregion

        #region events

        public event EventHandler MediaOpened;
        public event EventHandler MediaEnded;

        #endregion

        #region FTTFrequency

        private readonly int fftDataSize = (int)FFTDataSize.FFT2048;
        private readonly int maxFFT = (int)(BASSData.BASS_DATA_AVAILABLE | BASSData.BASS_DATA_FFT2048);
        private int sampleFrequency = 44100;

        public int GetFFTFrequencyIndex(int frequency)
        {
            return Utils.FFTFrequency2Index(frequency, fftDataSize, sampleFrequency);
        }

        public bool GetFFTData(float[] fftDataBuffer)
        {
            return (Bass.BASS_ChannelGetData(ActiveStreamHandle, fftDataBuffer, maxFFT)) > 0;
        }

        #endregion

        #region INotifyPropChange

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
