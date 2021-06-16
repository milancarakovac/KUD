using KUD.database.dao.mysql;
using KUD.database.dto;
using KUD.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KUD.Pages.Administrator
{
    sealed partial class AdminPage : Page
    {
        private Nalog LoggedAccount;
        private Osoba LoggedUser;

        public AdminPage()
        {
            this.InitializeComponent();
        }
        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavigationView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "home")
                {
                    NavigationView.SelectedItem = item;
                    break;
                }
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage),LoggedAccount);
            }
            else
            {
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
            }
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "home":
                    ContentFrame.Navigate(typeof(WelcomePage));
                    break;

                case "users":
                    ContentFrame.Navigate(typeof(AllAccountsPage));
                    break;

                case "yourInfo":
                    ContentFrame.Navigate(typeof(UserDataPage),new Account(LoggedUser.Ime,LoggedUser.Prezime,LoggedAccount.KorisnickoIme,LoggedAccount.Administrator,LoggedAccount.IdOsoba,LoggedAccount.IdNalog));
                    break;

                case "password":
                    ContentFrame.Navigate(typeof(PasswordChangePage), new Account(LoggedUser.Ime, LoggedUser.Prezime, LoggedAccount.KorisnickoIme, LoggedAccount.Administrator, LoggedAccount.IdOsoba, LoggedAccount.IdNalog));
                    break;

                case "settings":
                    ContentFrame.Navigate(typeof(SettingsPage), LoggedAccount);
                    break;

                case "logOut":
                    LoggedAccount = null;
                    LoggedUser = null;
                    ApplicationView.GetForCurrentView().ExitFullScreenMode();
                    this.Frame.Navigate(typeof(LogIn));
                    break;
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Nalog nalog)
            {
                ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
                LoggedAccount = nalog;
                OsobaDaoMySql osobaDao = new OsobaDaoMySql();
                LoggedUser = osobaDao.GetById(LoggedAccount.IdOsoba);
                UserInfoPart.Content = LoggedUser.Ime + " " + LoggedUser.Prezime;
                LogIn.changeTheme(LoggedAccount.Tema);
                ContentFrame.Navigate(typeof(WelcomePage));
            }
            else
            {
                LoggedAccount = null;
                this.Frame.Navigate(typeof(LogIn));
            }
            base.OnNavigatedTo(e);
        }
    }
}
