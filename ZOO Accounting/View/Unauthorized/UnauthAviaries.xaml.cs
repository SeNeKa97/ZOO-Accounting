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
using ZOO_Accounting.ViewModel;

namespace ZOO_Accounting.View.Unauthorized
{
    /// <summary>
    /// Логика взаимодействия для Aviary.xaml
    /// </summary>
    public partial class UnauthAviaries : Window
    {
        private AviaryViewModel vm;
        public UnauthAviaries(AviaryViewModel model)
        {
            vm = model;
            DataContext = vm;

            InitializeComponent();
        }

        private void Row_Click(object sender, MouseButtonEventArgs e)
        {
            object selectedItem = ((DataGridRow)sender).Item;

            if (selectedItem != null)
            {
                vm.SetSelectedItem(selectedItem);
                animalsGrid.Items.Refresh();
                inventoriesGrid.Items.Refresh();
            }
        }

        private void grid_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.ResetSelected();
        }
    }
}
