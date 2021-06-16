using KUD.database.dao.mysql;
using KUD.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace KUD.Pages.User
{
    sealed partial class MembersPage : Page
    {
        internal ObservableCollection<Member> AllMembers { get; set; }
        public MembersPage()
        {
            InitializeComponent();
            AllMembers = GetAllMembers();
        }
        private ObservableCollection<Member> GetAllMembers()
        {
            var list = new ObservableCollection<Member>();
            var members = (new ClanDaoMySql()).GetAll();
            foreach (var member in members)
            {
                if (!member.Obrisan)
                    list.Add(new Member(member.Ime, member.Prezime, member.UzrasnaGrupa, member.IdOsoba));
            }
            return list;
        }
        private void EditButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
                this.Frame.Navigate(typeof(EditMemberPage), dataGrid.SelectedItem as Member);
        }
        private void DeleteButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selected = dataGrid.SelectedItem as Member;
                AllMembers.Remove(selected);
                (new ClanDaoMySql()).Delete(selected.IdPerson);
                dataGrid.SelectedItem = null;
            }
        }

        private void AddMemberButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditMemberPage), null);
        }
    }
}
