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

namespace ZOO_Accounting.View.Updaters
{
    /// <summary>
    /// Логика взаимодействия для UpdateAnimalKind.xaml
    /// </summary>
    public partial class UpdateAnimalKind : Window
    {
        private IUpdater vm;
        private AnimalKind _animalKind;
        public UpdateAnimalKind(UpdateAnimalKindViewModel model)
        {
            vm = model;
            this.DataContext = vm;
            _animalKind = (AnimalKind)vm.GetEditableItem();
            InitializeComponent();
            nameTextBox.Text = _animalKind.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Validate())
            {
                _animalKind.Name = nameTextBox.Text;
                _animalKind.EatingStrategy = (EatingStrategy)eatingStrategyCombo.SelectedItem;
                _animalKind.Biome = (Biome)biomeCombo.SelectedValue;

                if (vm.Update(_animalKind))
                    this.Close();
                else
                    MessageBox.Show("Вид животных с таким названием уже существует!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool Validate()
        {

            if (!new ValidNameRegexChecker().Check(nameTextBox.Text)) {
                MessageBox.Show("Введите правильное имя не меньше 2 символов");
                return false;
            } if (eatingStrategyCombo.SelectedItem == null) {
                MessageBox.Show("Выберите тип питания");
                return false;
            } if (biomeCombo.SelectedItem == null) {
                MessageBox.Show("Выберите биом");
                return false;
            }

            return true;
        }
    }
}
