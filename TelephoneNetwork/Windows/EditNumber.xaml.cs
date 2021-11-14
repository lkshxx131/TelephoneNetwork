using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TelephoneNetwork.EF;

namespace TelephoneNetwork.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditNumber.xaml
    /// </summary>
    public partial class EditNumber : Window
    {
        public EditNumber()
        {
            InitializeComponent();

            cmbTariffPlan.ItemsSource = EntEF.Context.TariffPlan.Select(i => i.TariffName).ToList();

            var tariff = EntEF.Context.TariffPlan.Where(i => i.IdTariffPlan == EntEF.idTariff).FirstOrDefault();
            cmbTariffPlan.SelectedItem = EntEF.Context.TariffPlan.Where(i => i.IdTariffPlan == tariff.IdTariffPlan).Select(i => i.TariffName).FirstOrDefault();

            cmbTariffPlan.Text = tariff.TariffName;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveTariff_Click(object sender, RoutedEventArgs e)
        {
            var tariff = EntEF.Context.Number.Where(i => i.IdTariffPlan == EntEF.idTariff).FirstOrDefault();
            tariff.IdTariffPlan = EntEF.Context.TariffPlan.Where(i => i.TariffName == cmbTariffPlan.SelectedItem.ToString()).Select(i => i.IdTariffPlan).FirstOrDefault();

            EntEF.Context.SaveChanges();
            MessageBox.Show("Тариф изменен", "Изменение тарифа", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
