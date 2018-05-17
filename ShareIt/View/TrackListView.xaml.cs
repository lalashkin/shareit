using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ShareIt.Models;
using ShareIt.ViewModel;


namespace ShareIt.View
{
    /// <summary>
    /// Interaction logic for TrackListView.xaml
    /// </summary>
    public partial class TrackListView : UserControl
    {
        public BassTrackVM BassTrackViewModel = new BassTrackVM();
        public static int currentTrackIndex;


        public TrackListView()
        {
            InitializeComponent();
            this.DataContext = BassTrackViewModel;
        }


        private void TracksList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    BassTrackViewModel.AddTrack(path);
                }
            }
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            currentTrackIndex = TracksList.Items.IndexOf(TracksList.SelectedItem);

            if(TracksList.SelectedItems.Count == 1)
            {
                BassTrackViewModel.PlayTrack((BassTrack)TracksList.Items[currentTrackIndex]);
            }
        }

        private void AddPlaylist_Click(object sender, RoutedEventArgs e)
        {
            PlaylistNameInput nameInput = new PlaylistNameInput();
            nameInput.Show();
            PlaylistNameHolder.Content = PlaylistViewModel.CurrentPlaylist;
        }

        private void OpenPlaylist_Click(object sender, RoutedEventArgs e)
        {
            PlaylistViewModel PlaylistVM = new PlaylistViewModel();
            PlaylistVM.OpenPlaylist();
            PlaylistNameHolder.Content = PlaylistViewModel.CurrentPlaylist;

        }

        private void ResetPlaylist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BassTrackVM.TracksList.Clear();
                PlaylistViewModel.CurrentPlaylist = null;
                PlaylistNameHolder.Content = "Default";
            }
            catch(System.NullReferenceException)
            {
                return;
            }
        }

        private void RemoveTrack_Click(object sender, RoutedEventArgs e)
        {
            object dataItem = ((FrameworkElement)sender).DataContext;
            BassTrackVM.TracksList.Remove((BassTrack)dataItem);
        }
    }
}
