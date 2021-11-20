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
        private void SaveSubscriber_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbLastName.Text) ||
                string.IsNullOrWhiteSpace(txbFirstName.Text) ||
                string.IsNullOrWhiteSpace(txbSeriesPassport.Text) ||
                string.IsNullOrWhiteSpace(txbNumberPassport.Text))
            {
                MessageBox.Show("Обязательные поля не заполнены", "Уведомление",
                           MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(cmbGender.Text) ||
                string.IsNullOrWhiteSpace(cmbBenefit.Text))
            {
                MessageBox.Show("Выдвижные списки не заполнены(Пол/льгота)", "Уведомление",
                           MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (txbLastName.Text.Length > 50 && txbFirstName.Text.Length > 50 && txbPatronymic.Text.Length > 50
                && txbEmail.Text.Length > 100 && txbAddress.Text.Length > 150)
            {
                MessageBox.Show("Введенные данные превышают допустимую длину",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (txbSeriesPassport.Text.Length != 4 && txbNumberPassport.Text.Length != 6)
            {
                MessageBox.Show("Введенные данные не соответствуют допустимым значениям (серия/номер паспорта)",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

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
            MessageBox.Show("Абонент успешно добавлен", "Уведомление",
                       MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void txbLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //запрет на ввод всего, кроме букв и пробелов
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }

        private void txbFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //запрет на ввод всего, кроме букв и пробелов
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }

        private void txbPatronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //запрет на ввод всего, кроме букв и пробелов
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }

        private void txbSeriesPassport_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Запрет на ввод всего, кроме цифр
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void txbNumberPassport_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Запрет на ввод всего, кроме цифр
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void txbBenefitCertificate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Запрет на ввод всего, кроме цифр
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void txbEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Можно вводить буквы, цифры, спец.символы
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && "@.".IndexOf(e.Text) < 0;
        }

        private void txbAddress_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Можно вводить буквы, цифры, спец.символы
            e.Handled = (!Char.IsLetter(e.Text, 0) && !(Char.IsDigit(e.Text, 0))) && ".,".IndexOf(e.Text) < 0;
        }

        private void txbLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbLastName.Text))
            {
                txbLastName.BorderBrush = Brushes.Red;
            }

            else
            {
                txbLastName.BorderBrush = Brushes.Aquamarine;
            }
        }

        private void txbFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbFirstName.Text))
            {
                txbFirstName.BorderBrush = Brushes.Red;
            }

            else
            {
                txbFirstName.BorderBrush = Brushes.Aquamarine;
            }
        }

        private void txbSeriesPassport_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbSeriesPassport.Text))
            {
                txbSeriesPassport.BorderBrush = Brushes.Red;
            }
            else
            {
                txbSeriesPassport.BorderBrush = Brushes.Aquamarine;
            }
        }

        private void txbNumberPassport_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbNumberPassport.Text))
            {
                txbNumberPassport.BorderBrush = Brushes.Red;
            }
            else
            {
                txbNumberPassport.BorderBrush = Brushes.Aquamarine;
            }
        }
    }

}
