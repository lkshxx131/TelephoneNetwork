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

namespace TelephoneNetwork.Windows.Manager
{
    /// <summary>
    /// Логика взаимодействия для EditTariff.xaml
    /// </summary>
    public partial class EditTariff : Window
    {
        TariffManagerPage f;
        public EditTariff(TariffManagerPage c)
        {
            InitializeComponent();
            f = c;

            var tariffPlans = EntEF.Context.TariffPlan.Where(i => i.IdTariffPlan == EntEF.idTariff).FirstOrDefault();

            txbNameTariff.Text = tariffPlans.TariffName;
            txbDescriptionTariff.Text = tariffPlans.Description;
            txbCostTariff.Text = tariffPlans.Cost.ToString();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SaveTariff_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbNameTariff.Text) ||
               string.IsNullOrWhiteSpace(txbCostTariff.Text))
            {
                MessageBox.Show("Обязательные поля не заполнены", "Уведомление",
                           MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (txbNameTariff.Text.Length > 60)
            {
                MessageBox.Show("Название тарифа превышает допустимую длину (60 символов)",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            var tariffPlans = EntEF.Context.TariffPlan.Where(i => i.IdTariffPlan == EntEF.idTariff).FirstOrDefault();
            tariffPlans.TariffName = txbNameTariff.Text;
            tariffPlans.Description = txbDescriptionTariff.Text;
            tariffPlans.Cost = Convert.ToDecimal(txbCostTariff.Text);

            EntEF.Context.SaveChanges();
            MessageBox.Show("Изменения сохранены", "Уведомление",
                MessageBoxButton.OK, MessageBoxImage.Information);

            f.Update();
            this.Close();
        }

        private void txbNameTariff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //запрет на ввод всего, кроме букв и пробелов
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }
        private void txbDescriptionTariff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Можно вводить буквы, цифры, спец.символы
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && ".,".IndexOf(e.Text) < 0;
        }
        private void txbCostTariff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Запрет на ввод всего, кроме цифр
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void txbNameTariff_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbNameTariff.Text))
            {
                txbNameTariff.BorderBrush = Brushes.Red;
            }

            else
            {
                txbNameTariff.BorderBrush = Brushes.Aquamarine;
            }
        }

        private void txbCostTariff_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbCostTariff.Text))
            {
                txbCostTariff.BorderBrush = Brushes.Red;
            }

            else
            {
                txbCostTariff.BorderBrush = Brushes.Aquamarine;
            }
        }
    }
}
