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
using ZOO_Accounting.View.Unauthorized;
using System.Windows;

namespace ZOO_Accounting.ViewModel
{
    public class AviaryViewModel:IViewModel
    {
        private EFDbContext _context;
        private IRepository<Aviary> repo;
        private Aviary _selected;
        private ObservableCollection<Animal> _animals;
        private ObservableCollection<Inventory> _inventories;
        public ObservableCollection<Animal> animals { 
            get {
                return _animals;
            }
            set {
                _animals = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Inventory> inventories { 
            get {
                return _inventories;
            }
            set {
                _inventories = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<Aviary> aviaries { get; set; }

        public AviaryViewModel(EFDbContext context)
        {
            _context = context;
            repo = new EFRepository<Aviary>(context);
            Refresh();

            Window window;
            if (Identity.IsAuthorized())
                window = new Aviaries(this);
            else
                window = new UnauthAviaries(this);
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
            throw new NotImplementedException();
        }

        public void CallCreate()
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(object item)
        {
            throw new NotImplementedException();
        }


        public void Refresh()
        {
            repo.Refresh();
            aviaries = new ObservableCollection<Aviary>(repo.GetItems());
        }

        public void SetSelectedItem(object item) {
            _selected = (Aviary)item;
            animals = new ObservableCollection<Animal>(_selected.Animals);
            inventories = new ObservableCollection<Inventory>(_selected.Inventories);
        }

        public void ResetSelected()
        {
            _selected = null;
            animals = null;
            inventories = null;
        }
    }
}
