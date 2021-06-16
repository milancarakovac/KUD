using KUD.database.dao.mysql;
using KUD.database.dto;
using KUD.Tables;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KUD
{
    sealed partial class PasswordChangePage : Page
    {
        private Account SelectedAccount;
        private Nalog Account;
        public PasswordChangePage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Account account)
            {
                SelectedAccount = account;
                Account = (new NalogDaoMySql()).GetById(SelectedAccount.IdAccount);
                ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                SelectedAccount = null;
                this.Frame.Navigate(typeof(PasswordChangePage));
            }
            base.OnNavigatedTo(e);
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (OldPassword.Password != Account.Lozinka)
            {
                ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible;
                return;
            }
            else if (Password.Password != PasswordConfirmation.Password)
            {
                ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible;
                return;
            }
            else
            {
                Account.Lozinka = Password.Password;
                if ((new NalogDaoMySql()).Update(Account) != 0) ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else
                {
                    ApplicationView.GetForCurrentView().ExitFullScreenMode();
                    LogIn.RootFrame.Navigate(typeof(LogIn));
                }
            }
        }
    }
}

