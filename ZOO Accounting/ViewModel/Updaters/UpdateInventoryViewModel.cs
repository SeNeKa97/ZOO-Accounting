using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZOO_Accounting.Model.Abstract;
using ZOO_Accounting.Model.Concrete;
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.View.Updaters;

namespace ZOO_Accounting.ViewModel
{
    public class UpdateInventoryViewModel:IUpdater
    {
        Inventory _inventory;
        IRepository<Inventory> repo;
        IRepository<InventoryType> _invType;
        public int type { get; set; }
        public ObservableCollection<InventoryType> _invTypes { 
            get { 
                return new ObservableCollection<InventoryType>(_invType.GetItems()); 
            } 
        }
        public UpdateInventoryViewModel(Inventory inventory, EFDbContext context)
        {
            _inventory = inventory;
            repo = new EFRepository<Inventory>(context);
            _invType = new EFRepository<InventoryType>(context);

            type = _inventory.InventoryTypeId;

            Window window = new UpdateInventory(this);
            window.ShowDialog();
        }
        public bool Update(object item)
        {
            Inventory inv = (Inventory)item;
            Inventory copy = (Inventory)_inventory.Clone();

            _inventory.InventoryTypeId = inv.InventoryTypeId;
            _inventory.InventoryType = inv.InventoryType;
            _inventory.RegistryCode = inv.RegistryCode;

            try
            {
                repo.Update(_inventory);
                repo.Save();
                return true;
            }
            catch (DbUpdateException ex) {
                Backup(_inventory, copy);
                repo.Update(_inventory);
                return false;
            }
        }

        public void Backup(object obj, object copy)
        {
            Inventory inv = (Inventory)obj;
            Inventory cpy = (Inventory)copy;

            inv.InventoryTypeId = cpy.InventoryTypeId;
            inv.InventoryType = cpy.InventoryType;
            inv.RegistryCode = cpy.RegistryCode;
        }

        public object GetEditableItem()
        {
            return _inventory;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
