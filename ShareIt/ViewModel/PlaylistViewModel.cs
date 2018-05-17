using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using ShareIt.Models;

namespace ShareIt.ViewModel
{
    class PlaylistViewModel
    {
        public static Playlist CurrentPlaylist;

        public PlaylistViewModel()
        {

        }

        public async void AddPlaylist(Playlist playlist, bool sendToServer)
        {
            if (UserSettings.globalCurrentAccount != null && !sendToServer)
            {
                CurrentPlaylist = playlist;

                using (ApplicationContext db = new ApplicationContext())
                {
                    await db.PlaylistSet.AddAsync(playlist);
                    await db.SaveChangesAsync();
                }
            }
            
            SaveFileDialog dialog = new SaveFileDialog();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Playlist));

            dialog.Filter = " JSON |*.json| All types| *.*";
            dialog.FileName = playlist.PlaylistName;

            dialog.ShowDialog();

            if(dialog.FileName != "")
            {
                using (System.IO.FileStream fs = (System.IO.FileStream)dialog.OpenFile())
                {
                    jsonFormatter.WriteObject(fs, playlist);
                }
            }
        }

        public void OpenPlaylist()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Playlist));

            dialog.Filter = "JSON |*.json| All types| *.*";

            dialog.ShowDialog();

            try
            {
                using (System.IO.FileStream fs = (System.IO.FileStream)dialog.OpenFile())
                {
                    Playlist Temp = (Playlist)jsonFormatter.ReadObject(fs);
                    CurrentPlaylist = Temp;
                    BassTrackVM.TracksList.Clear();

                    foreach (BassTrack track in Temp.PlaylistTracksSerialized)
                    {
                        BassTrackVM TrackVM = new BassTrackVM();
                        TrackVM.AddTrack(track);
                    }

                }
            }
            catch(InvalidOperationException)
            {
                MessageBox.Show("Error?");
                return;
            }
            catch(System.NullReferenceException)
            {
                MessageBox.Show("File is empty, or don't corresponds to the playlist format!", "Playlist opening error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            
        }
    }
}
