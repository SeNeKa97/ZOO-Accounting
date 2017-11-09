using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.Model.Concrete;
using ZOO_Accounting.Model.Abstract;
using ZOO_Accounting.ViewModel;
using ZOO_Accounting.View;
using System.Windows;
using ZOO_Accounting.View.Unauthorized;

namespace ZOO_Accounting.ViewModel
{
    public class AnimalKindViewModel : IViewModel
    {
        private ObservableCollection<AnimalKind> _animalKinds;
        public ObservableCollection<AnimalKind> animalKinds {
            get {
                return _animalKinds;
            }
            set {
                _animalKinds = value;
                NotifyPropertyChanged();
            }
        }
        private IRepository<AnimalKind> repo;
        private EFDbContext _context;
        public AnimalKindViewModel(EFDbContext context)
        {
            this._context = context;
            repo = new EFRepository<AnimalKind>(_context);
            animalKinds = new ObservableCollection<AnimalKind>(repo.GetItems());

            Window window;
            if (Identity.IsAuthorized())
                window = new AnimalKinds(this);
            else
                window = new UnauthAnimalKinds(this);
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
            AnimalKind animalKind = (AnimalKind)item;
            if (animalKind != null)
            {
                UpdateAnimalKindViewModel updateAnimalKind = new UpdateAnimalKindViewModel(animalKind, _context);
                Refresh();
            }
        }

        public void CallCreate()
        {
            CreateAnimalKindViewModel createAnimalKind = new CreateAnimalKindViewModel(_context);
            Refresh();
        }

        public void RemoveItem(object item)
        {
            if (item != null)
            {
                repo.Remove((AnimalKind)item);
                repo.Save();
                Refresh();
            }
        }

        public void Refresh()
        {
            repo.Refresh();
            animalKinds = new ObservableCollection<AnimalKind>(repo.GetItems());
        }
    }
}
