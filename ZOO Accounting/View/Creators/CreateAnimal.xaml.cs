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
    /// Логика взаимодействия для CreateAnimal.xaml
    /// </summary>
    public partial class CreateAnimal : Window
    {
        CreateAnimalViewModel vm;
        public CreateAnimal(CreateAnimalViewModel model) {
            InitializeComponent();
            vm = model;
            DataContext = vm;
        }


        private void okButton_Click(object sender, RoutedEventArgs e) {
            if (validateFields()) {
                string name = animalName.Text;
                string regCode = registryCode.Text;
                int kindId = (int)animalKindId.SelectedValue;
                DateTime birthDate = (DateTime)animalBirthDate.SelectedDate;
                Sex sex = (Sex)animalSex.SelectedItem;
                int rationId = (int)ration.SelectedValue;

                //if(vm.AddAnimal(name, regCode, birthDate, kindId, sex, rationId))
                if(vm.Create(new Animal(name, regCode, kindId, birthDate, sex, rationId)))
                    this.Close();
                else
                    MessageBox.Show("Животное с таким регистрационным кодом уже существует!");
            }

        }

        private bool validateFields()
        {
            bool result = true;
            if (!new TenCharRegexChecker().Check(registryCode.Text)) {
                MessageBox.Show("Неправильный регистрационный номер");
                result = false;
            } if (!new ValidNameRegexChecker().Check(animalName.Text)) {
                MessageBox.Show("Введите правильное имя не меньше 2 символов");
                result = false;
            } if (ration.SelectedItem == null) {
                MessageBox.Show("Выберите рацион!");
                result = false;
            } if (animalKindId.SelectedItem == null) {
                MessageBox.Show("Выберите вид животного!");
                result = false;
            } if (animalSex.SelectedItem == null) {
                MessageBox.Show("Выберите пол!");
                result = false;
            }
            return result;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
