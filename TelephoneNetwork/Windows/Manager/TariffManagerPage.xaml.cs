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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TelephoneNetwork.EF;

namespace TelephoneNetwork.Windows.Manager
{
    /// <summary>
    /// Логика взаимодействия для TariffManagerPage.xaml
    /// </summary>
    public partial class TariffManagerPage : Page
    {
        List<TariffPlan> tariffPlans = new List<TariffPlan>(EntEF.Context.TariffPlan.ToList());
        public TariffManagerPage()
        {
            InitializeComponent();
            lvTariffPlan.ItemsSource = tariffPlans.Where(i => i.IsDeleted == false);
        }

        private void lvTariffPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        private void btnAddTariff_Click(object sender, RoutedEventArgs e)
        {
            AddTariff addTariff = new AddTariff();
            addTariff.Show();
        }

        private void btnEditTariff_Click(object sender, RoutedEventArgs e)
        {
            if (lvTariffPlan.SelectedItem is TariffPlan tariffPlans)
            {
                EntEF.idTariff = tariffPlans.IdTariffPlan;
                EditTariff editTariff = new EditTariff();
                editTariff.Show();
                lvTariffPlan.ItemsSource = EntEF.Context.TariffPlan.ToList();
            }
            else
            {
                MessageBox.Show("Выберите тариф из списка.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeleteTariff_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Удалить выбранный тариф?", "Удаление тарифа", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if(lvTariffPlan.SelectedItem is TariffPlan tariffPlan)
                {
                    tariffPlan.IsDeleted = true;
                    EntEF.Context.SaveChanges();
                    MessageBox.Show("Тариф успешно удален", "Удаление тарифа", MessageBoxButton.OK, MessageBoxImage.Information);
                    lvTariffPlan.ItemsSource = EntEF.Context.TariffPlan.ToList();
                }
                else
                {
                    MessageBox.Show("Выберите тариф из списка", "Удаление трифа", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
