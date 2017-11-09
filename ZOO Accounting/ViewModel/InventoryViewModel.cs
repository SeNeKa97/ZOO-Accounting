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
using System.Windows;
using ZOO_Accounting.View.Unauthorized;

namespace ZOO_Accounting.ViewModel
{
    public class InventoryViewModel:IViewModel
    {
        private EFDbContext _context;
        private IRepository<Inventory> repo;
        public ObservableCollection<Inventory> _inventories;
        public ObservableCollection<Inventory> inventories
        {
            get
            {
                return _inventories;
            }
            set {
                _inventories = value;
                NotifyPropertyChanged();
            }
        }

        public InventoryViewModel(EFDbContext context)
        {
            _context = context;
            repo = new EFRepository<Inventory>(context);
            Refresh();

            Window window;
            if (Identity.IsAuthorized())
                window = new Inventories(this);
            else
                window = new UnauthInventories(this);
            window.ShowDialog();
        }
        

        public void CallEdit(object item)
        {
            if (item != null)
            {
                UpdateInventoryViewModel update = new UpdateInventoryViewModel((Inventory)item, _context);
                Refresh();
            }
        }

        public void CallCreate()
        {
            CreateInventoryViewModel create = new CreateInventoryViewModel(_context);
            Refresh();
        }

        public void RemoveItem(object item)
        {
            if (item != null)
            {
                repo.Remove((Inventory)item);
                repo.Save();
                Refresh();
            }
                
        }


        public void Refresh()
        {
            repo.Refresh();
            inventories = new ObservableCollection<Inventory>(repo.GetItems());
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
