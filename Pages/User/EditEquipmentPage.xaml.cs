using KUD.database.dao.mysql;
using KUD.database.dto;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KUD.Pages.User
{
    sealed partial class EditEquipmentPage:Page
    {
        private Oprema SelectedEquipment;
        private bool isUpdate;
        public EditEquipmentPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (e.Parameter is Oprema equipment)
            {
                SelectedEquipment = equipment;
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
            SerialNumber.Text = "";
            Description.Text = "";
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SetDefaultValues()
        {
            var opremaDao = new OpremaDaoMySql();
            var oprema = opremaDao.GetById(SelectedEquipment.IdOprema);
            Name.Text = oprema.Naziv;
            SerialNumber.Text = oprema.SerijskiBroj;
            Description.Text = oprema.Opis;
            ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if(Name.Text.Length<1 || SerialNumber.Text.Length<1) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
            if (isUpdate)
            {
                var equipment = new Oprema(SelectedEquipment.IdOprema, Name.Text, SerialNumber.Text, Description.Text, false, SelectedEquipment.Zaduzen);
                if ((new OpremaDaoMySql()).Update(equipment) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                this.Frame.Navigate(typeof(EquipmentPage));
            }
            else
            {
                var equipment = new Oprema(0, Name.Text, SerialNumber.Text, Description.Text, false, false);
                if ((new OpremaDaoMySql()).Insert(equipment) != 0) { ErrorMessage.Visibility = Windows.UI.Xaml.Visibility.Visible; return; }
                this.Frame.Navigate(typeof(EquipmentPage));
            }
        }
    }
}
