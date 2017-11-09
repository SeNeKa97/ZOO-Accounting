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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Collections.ObjectModel;
using ZOO_Accounting.ViewModel;
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.Model.Concrete;

namespace ZOO_Accounting.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AnimalKinds : Window
    {
        IViewModel vm;
        public AnimalKinds(AnimalKindViewModel model)
        {
            vm = model;
            DataContext = vm;

            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.CallCreate();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem != null)
            {
                var result = MessageBox.Show("При удалении вида животных, все животные этого вида будут удалены!", "Удаление вида животных", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                    vm.RemoveItem((AnimalKind)grid.SelectedItem);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            object item = (object)grid.SelectedItem;
            if (item != null)
                vm.CallEdit(item);
        }
    }
}
