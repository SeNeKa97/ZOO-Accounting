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
using ZOO_Accounting.View.Updaters;
using System.Data.Entity;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity.Infrastructure;

namespace ZOO_Accounting.ViewModel
{
    public class UpdateInventoryTypeViewModel:IUpdater
    {
        private IRepository<InventoryType> repo;
        private InventoryType _invType;
        public UpdateInventoryTypeViewModel(InventoryType invType,EFDbContext context)
        {
            _invType = invType;
            repo = new EFRepository<InventoryType>(context);

            UpdateInventoryType window = new UpdateInventoryType(this);
            window.ShowDialog();
        }


        public object GetEditableItem() { return _invType; }
        public void Backup(object obj, object copy) {
            InventoryType inv = (InventoryType)obj;

            inv.Name = ((InventoryType)copy).Name;
        }
        public bool Update(object item){
            InventoryType type = (InventoryType)item;
            InventoryType copy = (InventoryType)_invType.Clone();

            _invType.Name = type.Name;
            try
            {
                repo.Update(_invType);
                repo.Save();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Backup(_invType, copy);
                repo.Update(_invType);
                return false;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
