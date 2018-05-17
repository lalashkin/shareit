using System.Windows;
using ShareIt.Models;
using ShareIt.ViewModel;
using ShareIt.View;
using ShareIt.View.MainPlayer;
using System.ComponentModel;


namespace ShareIt
{
    /// <summary>
    /// Interaction logic for UserSettings.xaml
    /// </summary>
    public partial class UserSettings : Window
    {
        public static User globalCurrentAccount;
        public static SettingsProfile globalCurrentSettingsProfile;
        
        public UserSettings()
        {
            InitializeComponent();
        }


        //TODO: Почини блэд
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Заасинхронить это дело
            AccountViewModel AccountVM = new AccountViewModel();
            SettingsViewModel SettingsVM = new SettingsViewModel();

            AccountVM.SetSession(AccountVM.Authorize(LoginField.Text, PasswordField.Password));

            PasswordField.Password = "";

            if (globalCurrentAccount == null)
            {
                return;
            }

            UsernameHolder.Content = globalCurrentAccount.UserName;

            SettingsVM.ActivateSettingsProfile(SettingsVM.GetSelectedProfile(globalCurrentAccount));

            ProfilesList.ItemsSource = SettingsVM.GetProfilesList();
            ProfilesList.SelectedIndex = ProfilesList.Items.IndexOf(globalCurrentSettingsProfile);

            MessageBox.Show(ProfilesList.Items.IndexOf(globalCurrentSettingsProfile).ToString());

            //Выбор в зависимости от профиля


            LoginGrid.Visibility = Visibility.Collapsed;
            SavedProfiles.Visibility = Visibility.Visible;
        }

        private void SaveSettingsProfile_Click(object sender, RoutedEventArgs e)
        {
            InputBox box = new InputBox();
            box.Show();
        }

        private void EditSettingsProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteSettingsProfile_Click(object sender, RoutedEventArgs e)
        {
            SettingsViewModel SettingsVM = new SettingsViewModel();
            SettingsVM.DeleteCurrentProfile((SettingsProfile)ProfilesList.SelectedItem);
            ProfilesList.ItemsSource = SettingsVM.GetProfilesList();
        }

        private void EditSaveLocation_Click(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.SaveFileDialog FileDialog = new System.Windows.Forms.SaveFileDialog())
            {

            }
        }

        private void BassEqualaiser_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BassPlayerControls.MediaPlayer.ApplyParamsEQ(BassEqualaiser.NumberOfBands, BassEqualaiser.EqualizerValues);
        }

        private void ProfilesList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(ProfilesList.IsDropDownOpen)
            {
                SettingsViewModel SettingsVM = new SettingsViewModel();

                SettingsVM.SetSelectedProfile((SettingsProfile)ProfilesList.SelectedItem);
                SettingsVM.ActivateSettingsProfile((SettingsProfile)ProfilesList.SelectedItem);

                ProfilesList.IsDropDownOpen = false;
            }
        }

        private void LogOutSettings_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you shure you want to log out?", "Log out", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                AccountViewModel AccountVM = new AccountViewModel();
                AccountVM.EndSession();

                SavedProfiles.Visibility = Visibility.Collapsed;
                LoginGrid.Visibility = Visibility.Visible;
            }
            else
            {
                return;
            }
        }

        private void ProfilesList_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
        }

        private void CancelSettings_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void OkSettings_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }   
}
