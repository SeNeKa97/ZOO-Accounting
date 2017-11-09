using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ZOO_Accounting.View;
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.Model.Abstract;
using ZOO_Accounting.Model.Concrete;
using System.Data.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZOO_Accounting.ViewModel;
using System.Windows;
using ZOO_Accounting.View.Unauthorized;

namespace ZOO_Accounting.ViewModel
{
    public class AnimalViewModel : IViewModel
    {
        private ObservableCollection<Animal> _animals;
        public ObservableCollection<Animal> animals { 
            get {
                return _animals;
            }
            set{
                _animals = value;
                NotifyPropertyChanged();
            }
        }
        
        private IRepository<Animal> repo;

        EFDbContext _context;

        public AnimalViewModel(EFDbContext context) {
            _context = context;
            repo = new EFRepository<Animal>(context);
            Refresh();

            Window window;
            if(Identity.IsAuthorized())
                window = new Animals(this);
            else
                window = new UnauthAnimals(this);
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

        public void CallEdit(object item)
        {
            Animal animal = (Animal)item;
            if (animal != null)
            {
                UpdateAnimalViewModel updateAnimal = new UpdateAnimalViewModel(animal, _context);
                Refresh();
            }
        }

        public void CallCreate()
        {
            CreateAnimalViewModel createAnimal = new CreateAnimalViewModel(_context);
            Refresh();
        }

        public void RemoveItem(object item)
        {
            Animal animal = (Animal)item;
            if (animal != null)
            {
                try
                {
                    repo.Remove(animal);
                    repo.Save();
                    Logger.Log(string.Format("Животное {0} было удалено из зоопарка", animal.RegistryCode));
                }
                catch (InvalidOperationException ex)
                {
                    return;
                }
                Refresh();
            }
        }

        public void Refresh()
        {
            repo.Refresh();
            animals = new ObservableCollection<Animal>(repo.GetItems());
        }
    }
}
