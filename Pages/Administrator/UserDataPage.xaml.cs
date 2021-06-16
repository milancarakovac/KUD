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

namespace KUD.Pages.Administrator
{
    sealed partial class UserDataPage : Page
    {
        private Account SelectedAccount;
        private bool isUpdate;
        public UserDataPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (e.Parameter is Account account)
            {
                SelectedAccount = account;
                SetDefaultValues();
                isUpdate = true;
            }
            else
            {
                SetEmptyValues();
                isUpdate = false;
            }
            base.OnNavigatedTo(e);
        }

        private void SetEmptyValues()
        {
            FirstName.Text = "";
            LastName.Text = "";
            Jmbg.Text = "";
            Email.Text = "";
            DateOfBirth.Date = DateTime.Now;
            PhoneNumber.Text = "";
            Username.Text = "";
            Administrator.IsChecked = false;
            Password.Visibility = Windows.UI.Xaml.Visibility.Visible;
            PasswordConfirmation.Visibility = Windows.UI.Xaml.Visibility.Visible;
            PassText.Visibility = Windows.UI.Xaml.Visibility.Visible;
            PassConfText.Visibility = Windows.UI.Xaml.Visibility.Visible;
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
            Administrator.IsChecked = SelectedAccount.Administrator;
            Password.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            PasswordConfirmation.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            PassText.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            PassConfText.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }
        private void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (isUpdate)
            {
                    var person = new Osoba(SelectedAccount.IdPerson, FirstName.Text, LastName.Text, Jmbg.Text, PhoneNumber.Text, Email.Text, DateOfBirth.Date.DateTime);
                    if ((new OsobaDaoMySql()).Update(person) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                    var account = new Nalog(SelectedAccount.IdAccount, Username.Text, "", (bool)Administrator.IsChecked, SelectedAccount.IdPerson);
                    if((new NalogDaoMySql()).Update(account)!=0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                    UpdateSelectedAccount(person, account);
                    this.Frame.Navigate(typeof(UserDataPage), SelectedAccount);
            }
            else
            {
                if (Password.Password == PasswordConfirmation.Password)
                {
                    var person = new Osoba(0, FirstName.Text, LastName.Text, Jmbg.Text, PhoneNumber.Text, Email.Text, DateOfBirth.Date.DateTime);
                    if ((new OsobaDaoMySql()).Insert(person) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                    var account = new Nalog(0, Username.Text, Password.Password, (bool)Administrator.IsChecked, (new OsobaDaoMySql()).GetAll().Last().IdOsoba);
                    if ((new NalogDaoMySql()).Insert(account) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                    this.Frame.Navigate(typeof(AllAccountsPage));
                }else ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void UpdateSelectedAccount(Osoba person, Nalog account)
        {
            SelectedAccount.Administrator = account.Administrator;
            SelectedAccount.FirstName = person.Ime;
            SelectedAccount.LastName = person.Prezime;
            SelectedAccount.IdAccount = account.IdNalog;
            SelectedAccount.IdPerson = account.IdOsoba;
            SelectedAccount.Username = account.KorisnickoIme;
        }
    }
}
