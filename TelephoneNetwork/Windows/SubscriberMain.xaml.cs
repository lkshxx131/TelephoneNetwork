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
        SubscriberPage f;
        List<NumberView> numberViews = new List<NumberView>(EntEF.Context.NumberView.Where(i => i.IdSubscriber == EntEF.idSubscriber && i.StatusCode == "а").ToList());
        public SubscriberMain(SubscriberPage c)
        {
            InitializeComponent();
            f = c;
            lvSubscriberNumber.ItemsSource = numberViews;

            cmbGender.ItemsSource = EntEF.Context.Gender.Select(i => i.GenderName).ToList();
            cmbBenefit.ItemsSource = EntEF.Context.Benefit.Select(i => i.BenefitName).ToList();

            var subscriber = EntEF.Context.Subscriber.Where(i => i.IdSubscriber == EntEF.idSubscriber).FirstOrDefault();
            cmbGender.SelectedItem = EntEF.Context.Gender.Where(i => i.GenderCode == subscriber.GenderCode).Select(i => i.GenderName).FirstOrDefault();
            cmbBenefit.SelectedItem = EntEF.Context.Benefit.Where(i => i.BenefitCode == subscriber.BenefitCode).Select(i => i.BenefitName).FirstOrDefault();

            txbLastName.Text = subscriber.LastName;
            txbFirstName.Text = subscriber.FirstName;
            txbPatronymic.Text = subscriber.Patronymic;
            dpBirthDate.SelectedDate = subscriber.BirthDate;
            txbEmail.Text = subscriber.Email;
            txbAddress.Text = subscriber.Address;
            txbSeriesPassport.Text = subscriber.PassportSeries;
            txbNumberPassport.Text = subscriber.PassportNumber;
            txbBenefitCertificate.Text = subscriber.BenefitCertififcate;
        }

        public void Update()
        {
            lvSubscriberNumber.ItemsSource = EntEF.Context.NumberView.Where(i => i.IdSubscriber == EntEF.idSubscriber &&
                                                                            i.StatusCode == "а").ToList();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
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

            if (txbLastName.Text.Length > 50 || txbFirstName.Text.Length > 50 || txbPatronymic.Text.Length > 50
                || txbEmail.Text.Length > 100 || txbAddress.Text.Length > 150)
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
            MessageBox.Show("Изменения сохранены", "Уведомление",
                       MessageBoxButton.OK, MessageBoxImage.Information);

            f.Update();
        }

        private void OffSubscriber_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Отключить абонента?", "Отключение абонента",
                         MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var subscriber = EntEF.Context.Subscriber.Where(i => i.IdSubscriber == EntEF.idSubscriber).FirstOrDefault();
                subscriber.IsDeleted = true;

                EntEF.Context.SaveChanges();
                MessageBox.Show("Абонент успешно отключен", "Отключение абонента",
                           MessageBoxButton.OK, MessageBoxImage.Information);
            }

            f.Update();
            this.Close();
        }

        private void AddNumber_Click(object sender, RoutedEventArgs e)
        {
            AddNumber addNumber = new AddNumber(this);
            addNumber.ShowDialog();

            Update();
        }

        private void EditTariff_Click(object sender, RoutedEventArgs e)
        {
            if (lvSubscriberNumber.SelectedItem is NumberView number)
            {
                EntEF.idTariff = number.IdTariffPlan;
                EditNumber editNumber = new EditNumber(this);
                editNumber.ShowDialog();

                Update();
            }

            else
            {
                MessageBox.Show("Выберите номер из списка.", "Уведомление",
                           MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            Update();
        }

        private void OffNumber_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Отключить номер?", "Отключение номера",
                         MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (lvSubscriberNumber.SelectedItem is NumberView numberView)
                {
                    EntEF.idNumber = numberView.IdNumber;
                    var number = EntEF.Context.Number.Where(i => i.IdNumber == EntEF.idNumber).FirstOrDefault();
                    number.StatusCode = "н";

                    EntEF.Context.SaveChanges();
                    MessageBox.Show("Номер успешно отключен", "Отключение номера",
                               MessageBoxButton.OK, MessageBoxImage.Information);

                    Update();
                    lvSubscriberNumber.ItemsSource = numberViews;
                }

                else
                {
                    MessageBox.Show("Выберите номер из списка", "Отключение номера",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                Update();
                lvSubscriberNumber.ItemsSource = numberViews;
            }
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
