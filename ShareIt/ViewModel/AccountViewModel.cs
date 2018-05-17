using ShareIt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShareIt.ViewModel
{
    class AccountViewModel 
    {
        public static bool IsLoggedIn = false;

        UserAccount currentUser;

        public AccountViewModel()
        {
           
        }
        
        public bool Authorize(string username, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (db.UserAccountDbContext.Any(name => name.Login == username))
                {
                    currentUser = db.UserAccountDbContext.Where(name => name.Login == username).Single(); //Я идиот

                    if (currentUser.Password == password)
                    {
                        IsLoggedIn = true;

                        return true;
                    }
                    else
                    {
                        //Wrong password
                        currentUser = null;
                        return false;
                    }
                }
                else
                {
                    //Wrong Username
                    return false;
                }

            }
                
        }

        public void SetSession(bool authSucces)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (authSucces)
                {
                    UserSettings.globalCurrentAccount = GetActiveUser();
                }
                else
                {
                    MessageBox.Show("Cannot log in, check your credintals!", "Ooops", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
                
        }

        private User GetActiveUser()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.UserDbContext.Where(uid => uid.UserId == currentUser.UserId).Single();
            }
        }

        public void EndSession()
        {
            IsLoggedIn = false;
            UserSettings.globalCurrentAccount = null;
        }
    }
}
