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

namespace ZOO_Accounting.View.Updaters
{
    /// <summary>
    /// Логика взаимодействия для UpdateAviaryType.xaml
    /// </summary>
    public partial class UpdateAviaryType : Window
    {
        UpdateAviaryTypeViewModel vm;
        AviaryType _type;
        public UpdateAviaryType(UpdateAviaryTypeViewModel model)
        {
            vm = model;
            DataContext = vm;

            _type = (AviaryType)vm.GetEditableItem();
            nameBox.Text = _type.Name;
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate()) {
                _type.Name = nameBox.Text;

                if (vm.Update(_type))
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
