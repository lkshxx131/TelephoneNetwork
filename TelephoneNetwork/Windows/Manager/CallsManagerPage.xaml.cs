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
    /// Логика взаимодействия для CallsManagerPage.xaml
    /// </summary>
    public partial class CallsManagerPage : Page
    {
        List<CallsView> callsViews = new List<CallsView>(EntEF.Context.CallsView.ToList());
        public CallsManagerPage()
        {
            InitializeComponent();
            lvCalls.ItemsSource = callsViews;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }
    }
}
