using KUD.database.dao.mysql;
using KUD.database.dto;
using Windows.Globalization;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KUD
{
    sealed partial class SettingsPage : Page
    {
        private Nalog LoggedAccount;
        public SettingsPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Nalog nalog)
            {
                LoggedAccount = nalog;
                if (LoggedAccount.Jezik == "sr-Latn") LatinicLanguageRadioButton.IsChecked = true;
                    else CyrillicLanguageRadioButton.IsChecked = true;
                if (LoggedAccount.Tema == "crna") DarkThemeRadioButton.IsChecked = true;
                    else LightThemeRadioButton.IsChecked = true;
            }
            else
            {
                LoggedAccount = null;
                this.Frame.Navigate(typeof(LogIn));
            }
            base.OnNavigatedTo(e);
        }

        private void LatinicRadioButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ApplicationLanguages.PrimaryLanguageOverride != "sr-Latn")
            {
                LoggedAccount.Jezik = "sr-Latn";
                (new NalogDaoMySql()).Update(LoggedAccount);
                LogIn.LoggedAccount = LoggedAccount;
                LogIn.changeLanguage("sr-Latn");
            }
        }

        private void CyrillicRadioButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ApplicationLanguages.PrimaryLanguageOverride != "sr-cyrl")
            {
                LoggedAccount.Jezik = "sr-cyrl";
                new NalogDaoMySql().Update(LoggedAccount);
                LogIn.LoggedAccount = LoggedAccount;
                LogIn.changeLanguage("sr-cyrl");
            }
        }

        private void DarkThemeRadioButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoggedAccount.Tema = "crna";
            new NalogDaoMySql().Update(LoggedAccount);
            LogIn.LoggedAccount = LoggedAccount;
            LogIn.changeTheme("crna");
        }

        private void LightThemeRadioButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoggedAccount.Tema = "svijetla";
            new NalogDaoMySql().Update(LoggedAccount);
            LogIn.LoggedAccount = LoggedAccount;
            LogIn.changeTheme("svijetla");
        }
    }
}
