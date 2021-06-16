using KUD.database.dao.mysql;
using KUD.database.dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace KUD.Pages.User
{
    sealed partial class EquipmentPage : Page
    {
        internal ObservableCollection<Oprema> AllEquipment { get; set; }
        public EquipmentPage()
        {
            InitializeComponent();
            AllEquipment = GetAllEquipment();
        }
        private ObservableCollection<Oprema> GetAllEquipment()
        {
            var list = new ObservableCollection<Oprema>();
            var allEquipment = (new OpremaDaoMySql()).GetAll();
            foreach (var equipment in allEquipment)
            {
                if (!equipment.Obrisan)
                    list.Add(equipment);
            }
            return list;
        }
        private void EditButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
                this.Frame.Navigate(typeof(EditEquipmentPage), dataGrid.SelectedItem as Oprema);
        }
        private void DeleteButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var selected = dataGrid.SelectedItem as Oprema;
                AllEquipment.Remove(selected);
                (new OpremaDaoMySql()).Delete(selected.IdOprema);
                dataGrid.SelectedItem = null;
            }
        }

        private void AddEquipmentButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EditEquipmentPage), null);
        }

        private void ChargeEquipmentButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ChargeEquipmentPage));
        }
    }
}
