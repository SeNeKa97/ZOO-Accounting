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
using ZOO_Accounting.Model.Entities;
using System.Text.RegularExpressions;
using ZOO_Accounting.Model.Concrete;
using ZOO_Accounting.Helpers;

namespace ZOO_Accounting.View.Updaters
{
    /// <summary>
    /// Логика взаимодействия для UpdateAnimal.xaml
    /// </summary>
    public partial class UpdateAnimal : Window
    {
        private Animal _animal;
        private IUpdater vm;

        public UpdateAnimal(UpdateAnimalViewModel model)
        {
            InitializeComponent();
            this._animal = (Animal)model.GetEditableItem();

            vm = model;
            
            animalName.Text = _animal.Name;
            registryCode.Text = _animal.RegistryCode;
            animalKindId.SelectedValue = _animal.AnimalKindId;
            animalBirthDate.SelectedDate = _animal.BirthDate;
            //animalSex.IsChecked = animal.Sex;
            ration.SelectedValue = _animal.RationId;

            DataContext = vm;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (validateFields())
            {
                _animal.Name = animalName.Text;
                _animal.RegistryCode = registryCode.Text;
                _animal.AnimalKindId = (int)animalKindId.SelectedValue;
                _animal.BirthDate = (DateTime)animalBirthDate.SelectedDate;
                _animal.Sex = (Sex)animalSex.SelectedItem;
                _animal.RationId = (int)ration.SelectedValue;

                if(vm.Update(_animal))
                    this.Close();
                else
                    MessageBox.Show("Животное с таким регистрационным кодом уже существует!");
            }
        }

        private bool validateFields()
        {
            bool result = true;
            if (!new TenCharRegexChecker().Check(registryCode.Text)){
                MessageBox.Show("Неправильный регистрационный номер");
                result = false;
            } if (!new ValidNameRegexChecker().Check(animalName.Text)) {
                MessageBox.Show("Введите правильное имя не меньше 2 символов");
                result = false;
            } if (ration.SelectedItem == null)
            {
                MessageBox.Show("Выберите рацион!");
                result = false;
            } if (animalKindId.SelectedItem == null)
            {
                MessageBox.Show("Выберите вид животного!");
                result = false;
            } if (animalSex.SelectedItem == null)
            {
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
