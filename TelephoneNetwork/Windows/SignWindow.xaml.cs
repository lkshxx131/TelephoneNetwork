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
using TelephoneNetwork.Windows.Manager;
using TelephoneNetwork.Windows.Operator;
using TelephoneNetwork.EF;
using TelephoneNetwork.ClassHelper;

namespace TelephoneNetwork.Windows
{
    /// <summary>
    /// Логика взаимодействия для SignWindow.xaml
    /// </summary>
    public partial class SignWindow : Window
    {
        public SignWindow()
        {
            InitializeComponent();
        }

        private void btnSign_Click(object sender, RoutedEventArgs e)
        {
            var user = EntEF.Context.Employee.Where(i => i.Login == txbLogin.Text && i.Password == psbPassword.Password).FirstOrDefault();

            if (txbLogin.ToString() != "" && psbPassword.ToString() != "" && user != null)
            {
                ClassUserId.Instance.idEmployee = user.IdEmployee;
                switch (user.IdPosition)
                {
                    case 1:
                        ManagerMain managerMain = new ManagerMain();
                        managerMain.Show();
                        this.Close();

                        break;

                    case 2:
                        OperatorMain operatorMain = new OperatorMain();
                        operatorMain.Show();
                        this.Close();

                        break;
                }
            }

            else
            {
                MessageBox.Show("Введенные данные неккоректны", "Ошибка");
            }
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
