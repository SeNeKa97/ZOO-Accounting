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
using ZOO_Accounting.View.Updaters;
using System.Windows;
using ZOO_Accounting.View.Unauthorized;

namespace ZOO_Accounting.ViewModel
{
    public class InventoryTypeViewModel:IViewModel
    {
        private ObservableCollection<InventoryType> _inventoryTypes;
        public ObservableCollection<InventoryType> inventoryTypes {
            get {
                return _inventoryTypes;
            } set {
                _inventoryTypes = value;
                NotifyPropertyChanged();
            }
        }
        private IRepository<InventoryType> repo;
        private EFDbContext _context;

        public InventoryTypeViewModel(EFDbContext context)
        {
            _context = context;
            repo = new EFRepository<InventoryType>(context);
            Refresh();

            Window window;
            if(Identity.IsAuthorized())
                window = new InventoryTypes(this);
            else
                window = new UnauthInventoryTypes(this);
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
            UpdateInventoryTypeViewModel update = new UpdateInventoryTypeViewModel((InventoryType)item, _context);
            Refresh();
        }

        public void CallCreate()
        {
            CreateInventoryTypeViewModel createIntType = new CreateInventoryTypeViewModel(_context);
            Refresh();
        }

        public void RemoveItem(object item)
        {
            if (item != null)
            {
                repo.Remove((InventoryType)item);
                repo.Save();
                Refresh();
            }
        }

        public void Refresh()
        {
            repo.Refresh();
            inventoryTypes = new ObservableCollection<InventoryType>(repo.GetItems());
        }
    }
}
