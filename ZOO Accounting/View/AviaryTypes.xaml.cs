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
    /// Логика взаимодействия для AviaryType.xaml
    /// </summary>
    public partial class AviaryTypes : Window
    {
        IViewModel vm;
        public AviaryTypes(AviaryTypeViewModel model)
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
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            AviaryType selected = (AviaryType)grid.SelectedItem;
            if (selected != null) {
                var result = MessageBox.Show("Удаление типа вольера приведет к удалению всех вольеров этого типа, за чем последует удаление всех животных и инвентаря в этих вольерах!", "Удаление вольера", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                    vm.RemoveItem(selected);
            }
        }
    }
}
