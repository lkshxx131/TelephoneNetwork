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

namespace TelephoneNetwork.Windows.Operator
{
    /// <summary>
    /// Логика взаимодействия для TariffOperatorPage.xaml
    /// </summary>
    public partial class TariffOperatorPage : Page
    {
        List<TariffPlan> tariffPlans = new List<TariffPlan>(EntEF.Context.TariffPlan.ToList());
        public TariffOperatorPage()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            txbSearch.Text = null;

            lvTariffPlan.ItemsSource = tariffPlans.Where(i => i.IsDeleted == false).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var list = EntEF.Context.TariffPlan.Where(i => i.IsDeleted != true).ToList();

            lvTariffPlan.ItemsSource = list.Where(i => i.TariffName.ToLower().Contains(txbSearch.Text) ||
                                                  i.Description.ToLower().Contains(txbSearch.Text.ToLower()));

            if (txbSearch.Text == "")
            {
                lvTariffPlan.ItemsSource = list;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
