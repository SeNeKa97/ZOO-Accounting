using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data.Entity;
using ZOO_Accounting.ViewModel;
using ZOO_Accounting.Model.Concrete;

namespace ZOO_Accounting.View
{
    /// <summary>
    /// Логика взаимодействия для Animals.xaml
    /// </summary>
    public partial class Animals : Window
    {
        private IViewModel vm;
        
        public Animals(AnimalViewModel model)
        {
            vm = model;
            InitializeComponent();
            DataContext = vm;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            vm.CallCreate();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem != null){
                Animal animal = (Animal)grid.SelectedItem;
                vm.CallEdit(animal);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = (Animal)grid.SelectedItem;
            if (animal != null) {
                vm.RemoveItem(animal);
            }
        }
    }
}
