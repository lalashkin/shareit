using System.Windows;
using ShareIt.ViewModel;
using ShareIt.Models;


namespace ShareIt.View
{
    /// <summary>
    /// Interaction logic for PlaylistNameInput.xaml
    /// </summary>
    public partial class PlaylistNameInput : Window
    {
        public PlaylistNameInput()
        {
            InitializeComponent();
            DataContext = AccountViewModel.IsLoggedIn;
        }

        private void SubmitName_Click(object sender, RoutedEventArgs e)
        {
            PlaylistViewModel PlaylistVM = new PlaylistViewModel();

            if(UserSettings.globalCurrentAccount != null)
            {
                PlaylistVM.AddPlaylist(new Playlist(UserSettings.globalCurrentAccount.UserId, BassTrackVM.TracksList, this.PlaylistName.Text), SendToServer.IsChecked ?? false);
            }
            else
            {
                PlaylistVM.AddPlaylist(new Playlist(0, BassTrackVM.TracksList, this.PlaylistName.Text), SendToServer.IsChecked ?? false);
            }

            Close();
        }
    }
}
