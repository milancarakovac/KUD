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
    sealed partial class EditPerformancePage : Page
    {
        private Performance SelectedPerformance;
        private bool isUpdate;
        public EditPerformancePage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (e.Parameter is Performance nastup)
            {
                SelectedPerformance = nastup;
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
            Name.Text = "";
            Location.Text = "";
            Date.Date = DateTimeOffset.Now.DateTime;
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SetDefaultValues()
        {
            Name.Text = SelectedPerformance.Naziv;
            Location.Text = SelectedPerformance.Mjesto;
            Date.Date = DateTime.Parse(SelectedPerformance.Datum);
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
        private void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Name.Text.Length < 1 || Location.Text.Length < 1) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
            if (isUpdate)
            {
                var nastup = new Nastup(SelectedPerformance.Id, Date.Date.DateTime, Location.Text, Name.Text);
                if ((new NastupDaoMySql()).Update(nastup) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                this.Frame.Navigate(typeof(PerformancesPage));
            }
            else
            {
                var nastup = new Nastup(0, Date.Date.DateTime, Location.Text, Name.Text);
                if ((new NastupDaoMySql()).Insert(nastup) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                this.Frame.Navigate(typeof(PerformancesPage));
            }
        }
    }
}
