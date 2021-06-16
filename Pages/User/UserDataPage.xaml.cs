using KUD.database.dao.mysql;
using KUD.database.dto;
using KUD.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KUD.Pages.User
{
    sealed partial class UserDataPage : Page
    {
        private Account SelectedAccount;
        public UserDataPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (e.Parameter is Account account)
            {
                SelectedAccount = account;
                SetDefaultValues();
            }
            else
            {
                this.Frame.Navigate(typeof(WelcomePage));
            }
            base.OnNavigatedTo(e);
        }
        private void SetDefaultValues()
        {
            var PersonDao = new OsobaDaoMySql();
            var Person = PersonDao.GetById(SelectedAccount.IdPerson);
            FirstName.Text = Person.Ime;
            LastName.Text = Person.Prezime;
            Jmbg.Text = Person.Jmbg;
            Email.Text = Person.Email;
            DateOfBirth.Date = Person.DatumRodjenja;
            PhoneNumber.Text = Person.BrojTelefona;
            Username.Text = SelectedAccount.Username;
        }
        private void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var person = new Osoba(SelectedAccount.IdPerson, FirstName.Text, LastName.Text, Jmbg.Text, PhoneNumber.Text, Email.Text, DateOfBirth.Date.DateTime);
            if ((new OsobaDaoMySql()).Update(person) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
            var account = new Nalog(SelectedAccount.IdAccount, Username.Text, "", false, SelectedAccount.IdPerson);
            if ((new NalogDaoMySql()).Update(account) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
            UpdateSelectedAccount(person, account);
            this.Frame.Navigate(typeof(UserDataPage), SelectedAccount);
        }
        private void UpdateSelectedAccount(Osoba person, Nalog account)
        {
            SelectedAccount.FirstName = person.Ime;
            SelectedAccount.LastName = person.Prezime;
            SelectedAccount.IdAccount = account.IdNalog;
            SelectedAccount.IdPerson = account.IdOsoba;
            SelectedAccount.Username = account.KorisnickoIme;
        }
    }
}
