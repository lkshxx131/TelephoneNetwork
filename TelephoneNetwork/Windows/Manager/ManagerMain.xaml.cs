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

namespace TelephoneNetwork.Windows.Manager
{
    /// <summary>
    /// Логика взаимодействия для ManagerMain.xaml
    /// </summary>
    public partial class ManagerMain : Window
    {
        public ManagerMain()
        {
            InitializeComponent();
        }

        private void btnSubscriber_Click(object sender, RoutedEventArgs e)
        {
            frmManager.Navigate(new SubscriberPage());
        }

        private void btnTariff_Click(object sender, RoutedEventArgs e)
        {
            frmManager.Navigate(new TariffManagerPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            SignWindow signWindow = new SignWindow();
            signWindow.Show();
            this.Close();
        }
    }
}
