using ShareIt.Models;
using ShareIt.View.MainPlayer;
using System;
using System.ComponentModel;
using System.Linq;

namespace ShareIt.ViewModel
{
    class SettingsViewModel
    {

        public BindingList<SettingsProfile> ProfilesList;


        public SettingsViewModel()
        { }


        public BindingList<SettingsProfile> GetProfilesList()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                ProfilesList = new BindingList<SettingsProfile>(db.SettingsProfileDbContext.Where(uid => uid.OwnerId == UserSettings.globalCurrentAccount.UserId).ToList());
                return ProfilesList;
            }
        }

        public void ActivateSettingsProfile(SettingsProfile profile)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                UserSettings.globalCurrentSettingsProfile = db.SettingsProfileDbContext
                    .Where(dbProfile => dbProfile.ProfileName == profile.ProfileName)
                    .Single();

                BassPlayerControls.userSettings.BassEqualaiser.EqualizerValues = Array.ConvertAll(UserSettings.globalCurrentSettingsProfile.EqualaiserValues, x=> (float)x);

                BassPlayerControls.userSettings.SaveLocationPath.Text = UserSettings.globalCurrentSettingsProfile.PlaylistPath;
            }

        }

        public SettingsProfile GetSelectedProfile(User user)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.SettingsProfileDbContext.Where(profile => profile.ProfileName == user.SelectedProfile).Single();
            }
        }

        public void SetSelectedProfile(SettingsProfile profile)
        {
            UserSettings.globalCurrentAccount.SelectedProfile = profile.ProfileName;

            using (ApplicationContext db = new ApplicationContext())
            {
                db.UserDbContext.Update(UserSettings.globalCurrentAccount);
                db.SaveChanges();
            }
        }

        public void CreateNewProfile(SettingsProfile settings)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.SettingsProfileDbContext.Add(settings);
                db.SaveChanges();
            }

        }

        public void EditCurrentProfile()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //Edit
            }
        }

        public void DeleteCurrentProfile(SettingsProfile profile)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.SettingsProfileDbContext.Remove(profile);
                db.SaveChanges();
            }
        }
    }
}
