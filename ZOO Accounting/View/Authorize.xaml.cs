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
using ZOO_Accounting.Helpers;
using ZOO_Accounting.Model.Concrete;

namespace ZOO_Accounting.View
{
    /// <summary>
    /// Логика взаимодействия для Authorize.xaml
    /// </summary>
    public partial class Authorize : Window
    {
        public Authorize()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
                if (Identity.Login(nameBox.Text, passBox.Text)) {
                    this.Close();
                } else
                    MessageBox.Show("Пользователь не найден");
        }

        protected bool Validate() {
            if (!new ValidNameRegexChecker().Check(nameBox.Text)) {
                MessageBox.Show("Введите правильное позволенное имя");
                return false;
            } if (!new NonEmptyRegexChecker().Check(passBox.Text)) {
                MessageBox.Show("Введите пароль");
                return false;
            }
            return true;
        }
    }
}
