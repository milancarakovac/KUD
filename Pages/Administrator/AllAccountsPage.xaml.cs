using KUD.database.dao.mysql;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using KUD.Tables;
using System.Collections.ObjectModel;

namespace KUD.Pages.Administrator
{
    sealed partial class AllAccountsPage : Page
    {

        internal ObservableCollection<Account> AllAccounts { get; set; }
        public AllAccountsPage()
        {
            this.InitializeComponent();
            AllAccounts = GetAllAccounts();
        }
        private ObservableCollection<Account> GetAllAccounts()
        {
            var list = new ObservableCollection<Account>();
            var accounts = new NalogDaoMySql();
            var persons = new OsobaDaoMySql();
            foreach (var account in accounts.GetAll())
            {
                var person = persons.GetById(account.IdOsoba);
                var newAccount = new Account(person.Ime, person.Prezime, account.KorisnickoIme, account.Administrator, account.IdOsoba, account.IdNalog);
                list.Add(newAccount);
            }
            return list;
        }

        private void DeleteButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selected = dataGrid.SelectedItem as Account;
                AllAccounts.Remove(selected);
                (new OsobaDaoMySql()).Delete(selected.IdPerson);
                (new NalogDaoMySql()).Delete(selected.IdAccount);
                dataGrid.SelectedItem = null;
            }
        }
        private void EditButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
                this.Frame.Navigate(typeof(AccountDataPage), dataGrid.SelectedItem as Account);
        }

        private void AddAccountButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserDataPage), null);
        }
    }
}
