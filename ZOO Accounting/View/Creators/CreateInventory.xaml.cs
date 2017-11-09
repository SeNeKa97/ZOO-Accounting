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
    /// Логика взаимодействия для CreateInventory.xaml
    /// </summary>
    public partial class CreateInventory : Window
    {
        CreateInventoryViewModel vm;
        public CreateInventory(CreateInventoryViewModel model)
        {
            vm = model;
            DataContext=vm;
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate()) { 
                string regCode=regCodeField.Text;
                InventoryType inv = (InventoryType)inventoryTypeCombo.SelectedItem;

                if (vm.Create(new Inventory(regCode, inv.Id))) {
                    this.Close();
                }
                else {
                    MessageBox.Show("Инвентарь с таким номером уже существует!");
                }
            }
        }

        protected bool Validate()
        {
            bool result = true;
            if (!new TenCharRegexChecker().Check(regCodeField.Text)) {
                MessageBox.Show("Введите правильный инвентарный номер!");
                result = false;
            }
            if (inventoryTypeCombo.SelectedItem == null) {
                MessageBox.Show("Выберите тип инвентаря!");
                result = false;
            }
            return result;
        }
    }
}
