using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ZOO_Accounting.View.Updaters;
using ZOO_Accounting.Model.Entities;
using System.Data.Entity;
using System.Collections.ObjectModel;
using ZOO_Accounting.Model.Concrete;
using ZOO_Accounting.Model.Abstract;
using System.Windows;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace ZOO_Accounting.ViewModel
{
    public class UpdateAnimalViewModel : IUpdater
    {
        public ObservableCollection<AnimalKind> animalKinds {
            get {
                return new ObservableCollection<AnimalKind>(_kinds.GetItems());
            }
        }
        public IEnumerable<Ration> animalRations {
            get {
                return new ObservableCollection<Ration>( _rations.GetItems());
            }
        }

        private IRepository<AnimalKind> _kinds;
        private IRepository<Animal> _animals;
        private IRepository<Ration> _rations;
        private Animal _animal;

        public int? ration { get; set; }
        public int? kind { get; set; }
        public int sex { get; set; }
        public UpdateAnimalViewModel(Animal animal, EFDbContext context)
        {
            _animal = animal;

            _kinds = new EFRepository<AnimalKind>(context);
            _animals = new EFRepository<Animal>(context);
            _rations = new EFRepository<Ration>(context);
            
            sex = (int)animal.Sex;
            ration = animal.RationId;
            kind = animal.AnimalKindId;

            UpdateAnimal window = new UpdateAnimal(this);
            window.ShowDialog();
        }

        public Animal GetAnimal(){
            return _animal;
        }


        public bool Update(object item)
        {
            Animal animal = (Animal)item;
            Animal copy = (Animal)_animal.Clone();

            _animal.Name = animal.Name;
            _animal.RegistryCode = animal.RegistryCode;
            _animal.AnimalKindId = animal.AnimalKindId;
            _animal.BirthDate = animal.BirthDate;
            _animal.Sex = animal.Sex;
            _animal.RationId = animal.RationId;

            try
            {
                _animals.Update(_animal);
                _animals.Save();
                NotifyPropertyChanged();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Backup(_animal, copy);
                _animals.Update(_animal);
                return false;
            }
        }
        public void Backup(object obj, object copy)
        {
            Animal animal = (Animal)obj;
            Animal cpy = (Animal)copy;

            animal.Name = cpy.Name;
            animal.RegistryCode = cpy.RegistryCode;
            animal.AnimalKindId = cpy.AnimalKindId;
            animal.BirthDate = cpy.BirthDate;
            animal.Sex = cpy.Sex;
            animal.RationId = cpy.RationId;
        }
        public object GetEditableItem()
        {
            return _animal;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
