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

namespace TelephoneNetwork.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddSubscriber.xaml
    /// </summary>
    public partial class AddSubscriber : Window
    {
        public AddSubscriber()
        {
            InitializeComponent();
            cmbGender.ItemsSource = EntEF.Context.Gender.Select(i => i.GenderName).ToList();
            cmbBenefit.ItemsSource = EntEF.Context.Benefit.Select(i => i.BenefitName).ToList();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txbLastName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbPatronymic_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txbBirthDate_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbSeriesPassport_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbNumberPassport_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txbAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbBenefit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txbBenefitCertificate_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveSubscriber_Click(object sender, RoutedEventArgs e)
        {
            EntEF.Context.Subscriber.Add(new Subscriber
            {
                LastName = txbLastName.Text,
                FirstName = txbFirstName.Text,
                Patronymic = txbPatronymic.Text,
                GenderCode = EntEF.Context.Gender.Where(i => i.GenderName == cmbGender.SelectedItem.ToString()).Select(i => i.GenderCode).FirstOrDefault(),
                BirthDate = dpBirthDate.DisplayDate,
                Email = txbEmail.Text,
                PassportSeries = txbSeriesPassport.Text,
                PassportNumber = txbNumberPassport.Text,
                Address = txbAddress.Text,
                RegDate = DateTime.Now,
                BenefitCode = EntEF.Context.Benefit.Where(i => i.BenefitName == cmbBenefit.SelectedItem.ToString()).Select(i => i.BenefitCode).FirstOrDefault(),
                BenefitCertififcate = txbBenefitCertificate.Text
            });
            EntEF.Context.SaveChanges();
            MessageBox.Show("Абонент успешно добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }

}
