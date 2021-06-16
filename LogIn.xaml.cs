using KUD.database.dao.mysql;
using Windows.UI.Xaml.Controls;
using KUD.database;
using KUD.Pages.Administrator;
using KUD.Pages.User;
using Windows.UI.ViewManagement;
using Windows.Globalization;
using KUD.database.dto;
using Windows.UI.Xaml;

namespace KUD
{
    sealed partial class LogIn : Page
    {
        public static Nalog LoggedAccount;
        public static Frame RootFrame;
        public LogIn()
        {
            this.InitializeComponent();
            RootFrame = Window.Current.Content as Frame;
            ConnectionPool.Initialize();
            ApplicationView.PreferredLaunchViewSize = new Windows.Foundation.Size(500, 300);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ErrorMessageField.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            NalogDaoMySql nalogDao = new NalogDaoMySql();
            foreach (var user in nalogDao.GetAll())
            {
                if (user.KorisnickoIme == usernameBox.Text.Trim() && user.Lozinka == passwordBox.Password)
                {
                    passwordBox.Password = "";
                    LoggedAccount = user;
                    if (user.Administrator)
                    {
                        changeLanguage(user.Jezik);
                        this.Frame.Navigate(typeof(AdminPage), user);
                    }
                    else
                    {
                        changeLanguage(user.Jezik);
                        this.Frame.Navigate(typeof(DefaultUserPage), user);
                    }
                    return;
                }
            }
            ErrorMessageField.Visibility = Visibility.Visible;
        }

        private void PasswordBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                Button_Click(sender, e);
        }
        public static void changeLanguage(string language)
        {
            ApplicationLanguages.PrimaryLanguageOverride = language;
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().Reset();
            Windows.ApplicationModel.Resources.Core.ResourceContext.GetForViewIndependentUse().Reset();
            if (LoggedAccount.Administrator)
                RootFrame.Navigate(typeof(AdminPage), LoggedAccount);
            else
                RootFrame.Navigate(typeof(DefaultUserPage), LoggedAccount);
        }
        public static void changeTheme(string theme)
        {
            if (theme == "crna")
                RootFrame.RequestedTheme = ElementTheme.Dark;
            else
                RootFrame.RequestedTheme = ElementTheme.Light;
            if (LoggedAccount.Administrator)
                RootFrame.Navigate(typeof(AdminPage), LoggedAccount);
            else
                RootFrame.Navigate(typeof(DefaultUserPage), LoggedAccount);
        }
    }
}
