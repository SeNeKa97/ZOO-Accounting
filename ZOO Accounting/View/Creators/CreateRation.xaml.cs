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
    /// Логика взаимодействия для CreateRation.xaml
    /// </summary>
    public partial class CreateRation : Window
    {
        private CreateRationViewModel vm;
        public CreateRation(CreateRationViewModel model)
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
            if (this.Validate())
            {
                string name = nameBox.Text;
                string ingredients = ingredientsBox.Text;

                if (vm.Create(new Ration(name, ingredients)))
                    this.Close();
                else
                    MessageBox.Show("Рацион с таким названием уже существует!");
            }
        }

        private bool Validate()
        {
            bool result = true;
            if (!new NonEmptyRegexChecker().Check(nameBox.Text))
            {
                MessageBox.Show("Введите название для рациона!");
                result = false;
            } if (!new NonEmptyRegexChecker().Check(ingredientsBox.Text))
            {
                MessageBox.Show("Введите ингредиенты!");
                result = false;
            }
            return result;
        }
    }
}
