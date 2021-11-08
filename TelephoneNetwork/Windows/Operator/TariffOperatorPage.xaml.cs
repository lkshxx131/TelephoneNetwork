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
            lvTariffPlan.ItemsSource = tariffPlans;
        }

        private void lvTariffPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }
    }
}
