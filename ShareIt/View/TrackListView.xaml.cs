using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public static BassTrackVM BassTrackViewModel = new BassTrackVM();
        public static int currentTrackIndex;

        public TrackListView()
        {
            InitializeComponent();
            this.DataContext = BassTrackViewModel;
        }

        //Interaction Functions (Маня,Инкапсулируй)

        private void TracksList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string path in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    try
                    {
                        if (TagLib.File.Create(path).MimeType == "taglib/mp3" ||
                            TagLib.File.Create(path).MimeType == "taglib/flac" ||
                            TagLib.File.Create(path).MimeType == "taglib/wav")
                        {
                            BassTrackViewModel.AddTrack(new BassTrack(path));
                        }
                    }
                    catch (TagLib.UnsupportedFormatException)
                    {
                        return;
                    }

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
    }
}
