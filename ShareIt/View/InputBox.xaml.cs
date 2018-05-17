using System.Windows;
using ShareIt.View.MainPlayer;
using ShareIt.ViewModel;
using ShareIt.Models;
using System;

namespace ShareIt.View
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        SettingsViewModel SettingsVM = new SettingsViewModel();

        public InputBox()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {            

            SettingsVM.CreateNewProfile(new SettingsProfile(UserSettings.globalCurrentAccount.UserId, Profilename.Text,
                                                            BassPlayerControls.userSettings.SaveLocationPath.Text,
                                                            Array.ConvertAll(BassPlayerControls.userSettings.BassEqualaiser.EqualizerValues, x => (decimal)x)));

            
            BassPlayerControls.userSettings.ProfilesList.ItemsSource = SettingsVM.GetProfilesList();

            Close(); 
        }
    }
}
