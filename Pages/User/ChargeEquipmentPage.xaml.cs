using KUD.database.dao.mysql;
using KUD.database.dto;
using KUD.Tables;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace KUD.Pages.User
{
    sealed partial class ChargeEquipmentPage : Page
    {
        private ObservableCollection<EquipmentCharge> AllCharges;
        public ChargeEquipmentPage()
        {
            InitializeComponent();
            AllCharges = GetAllCharges();
            ChargeError.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            DischargeError.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            foreach (var member in (new ClanDaoMySql()).GetAll())
                if (!member.Obrisan)
                    MemberCB.Items.Add(member);
            foreach (var equipment in (new OpremaDaoMySql()).GetAll())
                if (!equipment.Obrisan && !equipment.Zaduzen)
                    EquipmentCB.Items.Add(equipment);
        }

        private ObservableCollection<EquipmentCharge> GetAllCharges()
        {
            var list = new ObservableCollection<EquipmentCharge>();
            foreach(var data in (new ClanOpremaDaoMySql()).GetAll())
            {
                var member = (new ClanDaoMySql()).GetById(data.IdClan);
                var equipment = (new OpremaDaoMySql()).GetById(data.IdOprema);
                list.Add(new EquipmentCharge(member.ToString(), equipment.ToString(), data.DatumOd, data.DatumDo, data.Razduzeno, data.IdClanOprema));
            }
            return list;
        }

        private void ChargeButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var member = MemberCB.SelectedItem as Clan;
            var equipment = EquipmentCB.SelectedItem as Oprema;
            if (member != null && equipment != null)
            {
                var charge = new ClanOprema(0, member.IdOsoba, equipment.IdOprema, DateTime.Today, DateTime.MinValue, false);
                var dao = new ClanOpremaDaoMySql();
                equipment.Zaduzen = true;
                (new OpremaDaoMySql()).Update(equipment);
                dao.Insert(charge);
                ChargeError.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                AllCharges.Add(new EquipmentCharge(member.ToString(), equipment.ToString(),charge.DatumOd, charge.DatumDo, false, dao.GetAll().Last().IdClanOprema));
                this.Frame.Navigate(typeof(ChargeEquipmentPage));
            }
            else
            {
                ChargeError.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void DischargeButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var charge = dataGrid.SelectedItem as EquipmentCharge;
            if (charge != null && !charge.Discharged)
            {
                DischargeError.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                var dao = new ClanOpremaDaoMySql();
                var data = dao.GetById(charge.Id);
                var daoOprema = new OpremaDaoMySql();
                var equipment = daoOprema.GetById(data.IdOprema);
                equipment.Zaduzen = false;
                daoOprema.Update(equipment);
                EquipmentCB.Items.Add(equipment);
                data.Razduzeno = true;
                data.DatumDo = DateTime.Now;
                dao.Update(data);
                AllCharges.RemoveAt(dataGrid.SelectedIndex);
                AllCharges.Insert(dataGrid.SelectedIndex, new EquipmentCharge((new ClanDaoMySql()).GetById(data.IdClan).ToString(), equipment.ToString(), data.DatumOd, data.DatumDo, true, data.IdClanOprema));
            }
            else
            {
                DischargeError.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
    }
}
