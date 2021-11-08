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

namespace TelephoneNetwork.Windows
{
    /// <summary>
    /// Логика взаимодействия для SubscriberPageManager.xaml
    /// </summary>
    public partial class SubscriberPage : Page
    {
        List<SubscriberView> subscriberViews = new List<SubscriberView>(EntEF.Context.SubscriberView.ToList());
        public SubscriberPage()
        {
            InitializeComponent();
            lvSubscriber.ItemsSource = subscriberViews;
        }

        private void lvSubscriber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        private void btnAddSubscriber_Click(object sender, RoutedEventArgs e)
        {
            AddSubscriber addSubscriber = new AddSubscriber();
            addSubscriber.Show();
        }

        private void btnOpenSubscriber_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
