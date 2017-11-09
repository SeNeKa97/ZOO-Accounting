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
using ZOO_Accounting.Model.Concrete;
using System.ComponentModel;
using ZOO_Accounting.ViewModel.Factory;

namespace ZOO_Accounting.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void animalsWindow_Click(object sender, RoutedEventArgs e)
        {
            //Animals animals = new Animals();
            //animals.Show();
        }

        private void animalKindsWindow_Click(object sender, RoutedEventArgs e)
        {
            //AnimalKinds animalKinds = new AnimalKinds();
            //animalKinds.Show();
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e) {
            Application.Current.Shutdown();
        }

        private void AuthorizeCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AuthorizeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!Identity.IsAuthorized())
            {
                Authorize window = new Authorize();
                window.ShowDialog();
            }
            else Identity.Logout();
        }

        private void Click(object sender, RoutedEventArgs e) {
            Button button = (Button)sender;
            string tag = (string)button.Tag;

            //Window window = WindowFactory.CreateWindow(tag, Identity.IsAuthorized(), new EFDbContext());
            //window.ShowDialog();
            using (EFDbContext context = new EFDbContext())
            {
                ViewModelFactory.Create(tag, context);
            }
        }
    }
}
