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
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.ViewModel;

namespace ZOO_Accounting.View
{
    /// <summary>
    /// Логика взаимодействия для Inventory.xaml
    /// </summary>
    public partial class Inventories : Window
    {
        private IViewModel vm;
        public Inventories(InventoryViewModel model)
        {
            vm = model;
            DataContext = vm;

            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            vm.CallCreate();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            Inventory selectedItem = (Inventory)grid.SelectedItem;

            if(selectedItem!=null)
                vm.CallEdit(selectedItem);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem != null)
                vm.RemoveItem(grid.SelectedItem);
        }
    }
}
