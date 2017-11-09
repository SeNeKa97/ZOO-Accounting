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
using ZOO_Accounting.View.Creators;
using System.Data.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity.Infrastructure;
using System.Windows;

namespace ZOO_Accounting.ViewModel
{
    public class CreateInventoryViewModel:ICreator
    {
        private IRepository<Inventory> _invRepo;
        private IRepository<InventoryType> _typesRepo;
        public ObservableCollection<InventoryType> inventoryTypes {
            get {
                return new ObservableCollection<InventoryType>(_typesRepo.GetItems());
            }
        }

        public CreateInventoryViewModel(EFDbContext context)
        {
            _invRepo = new EFRepository<Inventory>(context);
            _typesRepo = new EFRepository<InventoryType>(context);

            Window window = new CreateInventory(this);
            window.ShowDialog();
        }
        public bool Create(object item)
        {
            Inventory inv = (Inventory)item;

            _invRepo.Create(inv);

            try {
                _invRepo.Save();
                NotifyPropertyChanged();
                return true;
            }
            catch (DbUpdateException ex) {
                _invRepo.Remove(inv);
                return false;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
