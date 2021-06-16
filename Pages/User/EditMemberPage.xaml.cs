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
    sealed partial class EditMemberPage : Page
    {
        private Member SelectedMember;
        private bool isUpdate;
        public EditMemberPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (e.Parameter is Member member)
            {
                SelectedMember = member;
                isUpdate = true;
                SetDefaultValues();
            }
            else
            {
                isUpdate = false;
                SetEmptyValues();
            }
            base.OnNavigatedTo(e);
        }

        private void SetEmptyValues()
        {
            FirstName.Text = "";
            LastName.Text = "";
            Jmbg.Text = "";
            Email.Text = "";
            DateOfBirth.Date = DateTimeOffset.Now.DateTime;
            PhoneNumber.Text = "";
            Group.Text = "";
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SetDefaultValues()
        {
            var memberDao = new ClanDaoMySql();
            var member = memberDao.GetById(SelectedMember.IdPerson);
            FirstName.Text = member.Ime;
            LastName.Text = member.Prezime;
            Jmbg.Text = member.Jmbg;
            Email.Text = member.Email;
            DateOfBirth.Date = member.DatumRodjenja;
            PhoneNumber.Text = member.BrojTelefona;
            Group.Text = member.UzrasnaGrupa;
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        private void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (isUpdate)
            {
                var member = new Clan(SelectedMember.IdPerson, FirstName.Text, LastName.Text, Jmbg.Text, PhoneNumber.Text, Email.Text, DateOfBirth.Date.DateTime, Group.Text, false);
                if ((new ClanDaoMySql()).Update(member) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                this.Frame.Navigate(typeof(MembersPage));
            }
            else
            {
                var member = new Clan(0, FirstName.Text, LastName.Text, Jmbg.Text, PhoneNumber.Text, Email.Text, DateOfBirth.Date.DateTime, Group.Text, false);
                if ((new ClanDaoMySql()).Insert(member) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                this.Frame.Navigate(typeof(MembersPage));
            }
        }
    }
}
