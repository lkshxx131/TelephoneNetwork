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
    /// Логика взаимодействия для AddNumber.xaml
    /// </summary>
    public partial class AddNumber : Window
    {
        public AddNumber()
        {
            InitializeComponent();
            cmbTariffPlan.ItemsSource = EntEF.Context.TariffPlan.Select(i => i.TariffName).ToList();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveTariff_Click(object sender, RoutedEventArgs e)
        {
            
            EntEF.Context.Number.Add(new Number
            {
                NumberName = txbNumber.Text,
                IdTariffPlan = cmbTariffPlan.SelectedIndex,
                //IdTariffPlan = Convert.ToInt32(cmbTariffPlan.Text),
                IdSubscriber = EntEF.idSubscriber,
                RegDate = DateTime.Now,
                StatusCode = "а",
                Balance = 0
            });
            EntEF.Context.SaveChanges();
            MessageBox.Show("Номер успешно добавлен", "Добавление номера", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
