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

namespace ZOO_Accounting.View.Unauthorized
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class UnauthAnimalKinds : Window
    {
        private IViewModel vm;
        public UnauthAnimalKinds(AnimalKindViewModel model)
        {
            vm = model;
            DataContext = vm;

            InitializeComponent();
        }
    }
}
