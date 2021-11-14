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
    /// Логика взаимодействия для SubscriberManager.xaml
    /// </summary>
    public partial class SubscriberMain : Window
    {
        List<NumberView> numberViews = new List<NumberView>(EntEF.Context.NumberView.Where(i => i.IdSubscriber == EntEF.idSubscriber && i.StatusCode == "а").ToList());
        public SubscriberMain()
        {
            InitializeComponent();
            lvSubscriberNumber.ItemsSource = numberViews;

            cmbBenefit.ItemsSource = EntEF.Context.Benefit.Select(i => i.BenefitName).ToList();
            cmbGender.ItemsSource = EntEF.Context.Gender.Select(i => i.GenderName).ToList();
            
            var subscriber = EntEF.Context.Subscriber.Where(i => i.IdSubscriber == EntEF.idSubscriber).FirstOrDefault();
            cmbBenefit.SelectedItem = EntEF.Context.Benefit.Where(i => i.BenefitCode == subscriber.BenefitCode).Select(i => i.BenefitName).FirstOrDefault();
            cmbGender.SelectedItem = EntEF.Context.Gender.Where(i => i.GenderCode == subscriber.GenderCode).Select(i => i.GenderName).FirstOrDefault();

            txbLastName.Text = subscriber.LastName;
            txbFirstName.Text = subscriber.FirstName;
            txbPatronymic.Text = subscriber.Patronymic;
            cmbGender.Text = subscriber.Gender.GenderName;
            dpBirthDate.SelectedDate = subscriber.BirthDate;
            txbEmail.Text = subscriber.Email;
            txbAddress.Text = subscriber.Address;
            txbSeriesPassport.Text = subscriber.PassportSeries;
            txbNumberPassport.Text = subscriber.PassportNumber;
            cmbBenefit.Text = subscriber.Benefit.BenefitName;
            txbBenefitCertificate.Text = subscriber.BenefitCertififcate;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var subscriber = EntEF.Context.Subscriber.Where(i => i.IdSubscriber == EntEF.idSubscriber).FirstOrDefault();
            subscriber.LastName = txbLastName.Text;
            subscriber.FirstName = txbFirstName.Text;
            subscriber.Patronymic = txbPatronymic.Text;
            subscriber.GenderCode = EntEF.Context.Gender.Where(i => i.GenderName == cmbGender.SelectedItem.ToString()).Select(i => i.GenderCode).FirstOrDefault();
            subscriber.BirthDate = dpBirthDate.DisplayDate;
            subscriber.Email = txbEmail.Text;
            subscriber.Address = txbAddress.Text;
            subscriber.PassportSeries = txbSeriesPassport.Text;
            subscriber.PassportNumber = txbNumberPassport.Text;
            subscriber.BenefitCode = EntEF.Context.Benefit.Where(i => i.BenefitName == cmbBenefit.SelectedItem.ToString()).Select(i => i.BenefitCode).FirstOrDefault();
            subscriber.BenefitCertififcate = txbBenefitCertificate.Text;

            EntEF.Context.SaveChanges();
            MessageBox.Show("Изменения сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void OffSubscriber_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Отключить абонента?", "Отключение абонента", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var subscriber = EntEF.Context.Subscriber.Where(i => i.IdSubscriber == EntEF.idSubscriber).FirstOrDefault();
                subscriber.IsDeleted = true;
                EntEF.Context.SaveChanges();
                MessageBox.Show("Абонент успешно отключен", "Отключение абонента", MessageBoxButton.OK, MessageBoxImage.Information);

            }    
        }

        private void AddNumber_Click(object sender, RoutedEventArgs e)
        {
            AddNumber addNumber = new AddNumber();
            addNumber.Show();
        }

        private void EditTariff_Click(object sender, RoutedEventArgs e)
        {
            if (lvSubscriberNumber.SelectedItem is NumberView number)
            {
                EntEF.idTariff = number.IdTariffPlan;
                EditNumber editNumber = new EditNumber();
                editNumber.Show();
            }
            else
            {
                MessageBox.Show("Выберите номер из списка.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OffNumber_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Отключить номер?", "Отключение номера", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (lvSubscriberNumber.SelectedItem is NumberView numberView)
                {
                    EntEF.idNumber = numberView.IdNumber;
                    var number = EntEF.Context.Number.Where(i => i.IdNumber == EntEF.idNumber).FirstOrDefault();
                    number.StatusCode = "н";
                    EntEF.Context.SaveChanges();
                    MessageBox.Show("Номер успешно отключен", "Отключение номера", MessageBoxButton.OK, MessageBoxImage.Information);
                    lvSubscriberNumber.ItemsSource = numberViews;
                }
                else
                {
                    MessageBox.Show("Выберите номер из списка", "Отключение номера", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                lvSubscriberNumber.ItemsSource = numberViews;
            }
        }  
    }
}
