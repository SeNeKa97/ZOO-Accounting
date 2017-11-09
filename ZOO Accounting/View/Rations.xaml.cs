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
    /// Логика взаимодействия для Rations.xaml
    /// </summary>
    public partial class Rations : Window
    {
        IViewModel vm;
        public Rations(RationViewModel model)
        {
            vm = model;
            this.DataContext = vm;
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            vm.CallCreate();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            Ration ration = (Ration)grid.SelectedItem;
            if (ration != null)
                vm.CallEdit(ration);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("При удалении рациона, все животные, которым назначен рацион, будут удалены!", "Удаление рациона", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
                vm.RemoveItem((Ration)grid.SelectedItem);
        }
    }
}
