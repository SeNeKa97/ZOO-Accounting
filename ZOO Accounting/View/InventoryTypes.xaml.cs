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
    /// Логика взаимодействия для InventoryType.xaml
    /// </summary>
    public partial class InventoryTypes : Window
    {
        IViewModel vm;
        public InventoryTypes(InventoryTypeViewModel model)
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
            InventoryType item = (InventoryType)grid.SelectedItem;
            if (item != null)
                vm.CallEdit(item);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            InventoryType item = (InventoryType)grid.SelectedItem;
            if (item != null) {
                var result = MessageBox.Show("Если вы удалите вид инвентаря, весь инвентарь этого вида будет удален!", "Удаление вида инвентаря", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                    vm.RemoveItem(item);
            }
        }
    }
}
