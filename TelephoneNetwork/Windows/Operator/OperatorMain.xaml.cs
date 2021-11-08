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

namespace TelephoneNetwork.Windows.Operator
{
    /// <summary>
    /// Логика взаимодействия для OperatorMain.xaml
    /// </summary>
    public partial class OperatorMain : Window
    {
        public OperatorMain()
        {
            InitializeComponent();
        }

        private void btnSubscriber_Click(object sender, RoutedEventArgs e)
        {
            frmOperator.Navigate(new SubscriberPage());
        }

        private void btnTariff_Click(object sender, RoutedEventArgs e)
        {
            frmOperator.Navigate(new TariffOperatorPage());
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            SignWindow signWindow = new SignWindow();
            signWindow.Show();
            this.Close();
        }
    }
}
