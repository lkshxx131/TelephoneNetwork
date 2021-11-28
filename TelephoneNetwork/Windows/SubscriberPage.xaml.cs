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
        //List<SubscriberView> subscriberViews = new List<SubscriberView>(EntEF.Context.SubscriberView.Where(i => i.IsDeleted == false).ToList());
        public SubscriberPage()
        {
            InitializeComponent();
            Update();
            //lvSubscriber.ItemsSource = subscriberViews;

            //List<Gender> genders = EntEF.Context.Gender.ToList();
            //genders.Insert(0, new Gender() { GenderName = "Все" });

            //cmbGenderFiltr.ItemsSource = genders;
            //cmbGenderFiltr.DisplayMemberPath = "GenderName";
            //cmbGenderFiltr.SelectedIndex = 0;
        }

        public void Update()
        {
            Filtr();

            List<Gender> genders = EntEF.Context.Gender.ToList();
            genders.Insert(0, new Gender() { GenderName = "Все" });

            cmbGenderFiltr.ItemsSource = genders;
            cmbGenderFiltr.DisplayMemberPath = "GenderName";
            cmbGenderFiltr.SelectedIndex = 0;

            txbSearch.Text = null;

            lvSubscriber.ItemsSource = EntEF.Context.SubscriberView.Where(i => i.IsDeleted == false).ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        private void btnAddSubscriber_Click(object sender, RoutedEventArgs e)
        {
            AddSubscriber addSubscriber = new AddSubscriber(this);
            addSubscriber.Show(); 
        }

        private void btnOpenSubscriber_Click(object sender, RoutedEventArgs e)
        {
            if (lvSubscriber.SelectedItem is SubscriberView subscriber)
            {
                EntEF.idSubscriber = subscriber.IdSubscriber;
                SubscriberMain subscriberMain = new SubscriberMain(this);
                subscriberMain.Show();

                Update();
            }

            else
            {
                MessageBox.Show("Выберите абонента из списка.", "Уведомление",
                           MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            Update();
        }

        public void Filtr()
        {
            var list = EntEF.Context.SubscriberView.Where(i => i.IsDeleted != true).ToList()
                                                   .Where(i => i.LastName.ToLower().Contains(txbSearch.Text) ||
                                                   i.FirstName.ToLower().Contains(txbSearch.Text) ||
                                                   i.Patronymic.ToLower().Contains(txbSearch.Text) ||
                                                   i.PassportSeries.Contains(txbSearch.Text) ||
                                                   i.PassportNumber.Contains(txbSearch.Text)).ToList();

            lvSubscriber.ItemsSource = list;

            if (cmbGenderFiltr.SelectedIndex == 0)
            {
                lvSubscriber.ItemsSource = list;
            }
            else
            {
                var Gender = cmbGenderFiltr.SelectedItem as Gender;

                if (Gender != null)
                {
                    list = list.Where(i => i.GenderCode == Gender.GenderCode).ToList();
                    lvSubscriber.ItemsSource = list;
                }
            }
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filtr();
        }

        private void cmbFiltration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
