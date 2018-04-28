using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using Un4seen.Bass;
using ShareIt.Models;

namespace ShareIt.View.MainPlayer
{
    /// <summary>
    /// Interaction logic for BassPlayerControls.xaml
    /// </summary>
    public partial class BassPlayerControls : UserControl
    {
        public static BassPlayer MediaPlayer;

        private DispatcherTimer timer;

        public BassPlayerControls()
        {
            InitializeComponent();

            MediaPlayer = new BassPlayer();
            MediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;

            timer = new DispatcherTimer();

            //Регистриуем плеер для Анализатора спектра
            spectrumAnalyser.RegisterSoundPlayer(MediaPlayer);

            //Присваеваем текущее значение звука (GigaKostil)
            VolumeSlider.Value = Bass.BASS_GetVolume();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            //
            if(!MediaPlayer.IsPlaying && TrackListView.BassTrackViewModel.TracksList.Count != 0)
            {
                MediaPlayer.OpenFile((TrackListView.BassTrackViewModel.TracksList[0]).TrackPath);
                MediaPlayer.Play();
                return;
            }
            //
            MediaPlayer.Play();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            MediaPlayer.PlayNext(TrackListView.BassTrackViewModel.TracksList);
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            timer.Stop();

            ProgresSlider.Maximum = MediaPlayer.StreamFileLength;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ProgresSlider.Value = MediaPlayer.GetStreamCurrentTime();
            MediaPlayer.CheckCurrentStreamActive();
        }

        private void ProgresSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MediaPlayer.SetCurrentStreamPosition(ProgresSlider.Value);
        }

        private void VolumeSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MediaPlayer.SetChannelVolume((float)VolumeSlider.Value);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            UserSettings userSettings = new UserSettings();
            userSettings.Show();
        }

        private void Backward_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.PlayPrev(TrackListView.BassTrackViewModel.TracksList);
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.PlayNext(TrackListView.BassTrackViewModel.TracksList);
        }
    }
}
