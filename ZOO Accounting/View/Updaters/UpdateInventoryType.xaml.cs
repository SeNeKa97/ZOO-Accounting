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
    /// Логика взаимодействия для UpdateInventoryType.xaml
    /// </summary>
    public partial class UpdateInventoryType : Window
    {
        private IUpdater vm;
        private InventoryType _invType;
        public UpdateInventoryType(UpdateInventoryTypeViewModel model)
        {
            vm = model;
            DataContext = vm;

            _invType = (InventoryType)vm.GetEditableItem();
            InitializeComponent();
            nameBox.Text = _invType.Name;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Validate()) {
                _invType.Name = nameBox.Text;

                if (vm.Update(_invType))
                    this.Close();
                else
                    MessageBox.Show("Тип инвентаря с таким названием уже существует!");
            }
        }

        protected bool Validate() {
            if (!new ValidNameRegexChecker().Check(nameBox.Text)){
                MessageBox.Show("Введите правильное название вида инвентаря!");
                return false;
            }
            return true;
        }
    }
}
