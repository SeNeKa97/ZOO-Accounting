using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using ZOO_Accounting.View.Creators;
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.Model.Concrete;
using ZOO_Accounting.Model.Abstract;
using System.Data.Entity.Infrastructure;
using System.Runtime.CompilerServices;

namespace ZOO_Accounting.ViewModel
{
    public class CreateAnimalViewModel : ICreator
    {
        public ObservableCollection<AnimalKind> animalKinds { 
            get {
                return new ObservableCollection<AnimalKind>(_animalKinds.GetItems());
            } 
        }
        public ObservableCollection<Ration> animalRations {
            get {
                return new ObservableCollection<Ration>(_animalRations.GetItems());
            } 
        }
        private IRepository<AnimalKind> _animalKinds;
        private IRepository<Ration> _animalRations;
        private IRepository<Animal> _animals;
        EFDbContext _context;

        public CreateAnimalViewModel(EFDbContext context) {
            _context = context;

            _animalKinds = new EFRepository<AnimalKind>(_context);
            _animalRations = new EFRepository<Ration>(_context);
            _animals = new EFRepository<Animal>(_context);

            CreateAnimal window = new CreateAnimal(this);
            window.ShowDialog();
        }
        

        

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool Create(object item)
        {
            Animal animal = (Animal)item;

            try
            {
                _animals.Create(animal);
                _animals.Save();
                Logger.Log(string.Format("Животное {0} добавлено в зоопарк!", animal.RegistryCode));
                return true;
            }
            catch (DbUpdateException ex)
            {
                //Backup(animal, copy);
                _animals.Remove(animal);
                return false;
            }
        }
    }
}
