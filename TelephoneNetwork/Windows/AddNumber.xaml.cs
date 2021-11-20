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
            if (string.IsNullOrWhiteSpace(txbNumber.Text))
            {
                MessageBox.Show("Обязательные поля не заполнены", "Уведомление",
                           MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (txbNumber.Text.Length > 11)
            {
                MessageBox.Show("Номер превышает допустимую длину (11 символов)",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            EntEF.Context.Number.Add(new Number
            {
                NumberName = txbNumber.Text,
                IdTariffPlan = cmbTariffPlan.SelectedIndex,
                IdSubscriber = EntEF.idSubscriber,
                RegDate = DateTime.Now,
                StatusCode = "а",
                Balance = 0
            });

            EntEF.Context.SaveChanges();
            MessageBox.Show("Номер успешно добавлен", "Добавление номера",
                       MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void txbNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Запрет на ввод всего, кроме перечисленных символов
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void txbNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbNumber.Text))
            {
                txbNumber.BorderBrush = Brushes.Red;
            }

            else
            {
                txbNumber.BorderBrush = Brushes.Aquamarine;
            }
        }
    }
}
