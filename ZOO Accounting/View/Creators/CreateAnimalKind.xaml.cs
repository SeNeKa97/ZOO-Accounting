using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для CreateAnimalKind.xaml
    /// </summary>
    public partial class CreateAnimalKind : Window
    {
        CreateAnimalKindViewModel vm;
        public CreateAnimalKind(CreateAnimalKindViewModel model)
        {
            vm = model;
            DataContext = vm;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Validate()) {
                string name = nameTextBox.Text;
                EatingStrategy eatingStrategy = (EatingStrategy)eatingStrategyCombo.SelectedItem;
                Biome biome = (Biome)biomeCombo.SelectedValue;

                if (vm.Create(new AnimalKind(name, biome, eatingStrategy)))
                    this.Close();
                else
                    MessageBox.Show("Вид животных с таким названием уже существует!");
            }
                

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool Validate() {

            if (!new ValidNameRegexChecker().Check(nameTextBox.Text)) {
                MessageBox.Show("Введите правильное имя не меньше 2 символов");
                return false;
            } if (eatingStrategyCombo.SelectedItem == null){
                MessageBox.Show("Выберите тип питания");
                return false;
            } if (biomeCombo.SelectedItem == null){
                MessageBox.Show("Выберите биом");
                return false;
            }

            return true;
        }
    }
}
