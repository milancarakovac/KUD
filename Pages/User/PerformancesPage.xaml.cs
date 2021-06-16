using KUD.database.dao.mysql;
using KUD.database.dto;
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
    sealed partial class PerformancesPage:Page
    {
        internal ObservableCollection<Performance> AllPerformances { get; set; }
        public PerformancesPage()
        {
            InitializeComponent();
            AllPerformances = GetAllPerformances();
        }

        private ObservableCollection<Performance> GetAllPerformances()
        {
            var list = new ObservableCollection<Performance>();
            var dao = new NastupDaoMySql();
            foreach (var nastup in dao.GetAll())
                list.Add(new Performance(nastup.Datum.ToString("dd.MM.yyyy."),nastup.Mjesto,nastup.Naziv,nastup.IdNastup));
            return list;
        }

        private void EditButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
                this.Frame.Navigate(typeof(EditPerformancePage), dataGrid.SelectedItem as Performance);
        }
        private void DeleteButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selected = dataGrid.SelectedItem as Performance;
                AllPerformances.Remove(selected);
                (new NastupDaoMySql()).Delete(selected.Id);
                dataGrid.SelectedItem = null;
            }
        }

        private void AddPerformanceButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditPerformancePage), null);
        }
    }
}
