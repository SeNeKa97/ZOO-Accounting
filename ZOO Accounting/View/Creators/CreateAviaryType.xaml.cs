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
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.ViewModel;

namespace ZOO_Accounting.View.Creators
{
    /// <summary>
    /// Логика взаимодействия для CreateAviaryType.xaml
    /// </summary>
    public partial class CreateAviaryType : Window
    {
        CreateAviaryTypeViewModel vm;
        public CreateAviaryType(CreateAviaryTypeViewModel model)
        {
            vm = model;
            DataContext = vm;
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate()) {
                AviaryType avType = new AviaryType(nameBox.Text);

                if (vm.Create(avType))
                    this.Close();
                else
                    MessageBox.Show("Тип вольера с таким названием уже существует!");
            }
        }

        private bool Validate()
        {
            if (!new ValidNameRegexChecker().Check(nameBox.Text))
            {
                MessageBox.Show("Введите правильное название для типа вольера");
                return false;
            }
            return true;
        }
    }
}
