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
    /// Логика взаимодействия для EditTariff.xaml
    /// </summary>
    public partial class EditTariff : Window
    {
        public EditTariff()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txbNameTariff_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbDescriptionTariff_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbCostTariff_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveTariff_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
