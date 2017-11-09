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

namespace ZOO_Accounting.ViewModel
{
    public class CreateInventoryTypeViewModel:ICreator
    {
        private IRepository<InventoryType> repo;
        public CreateInventoryTypeViewModel(EFDbContext context)
        {
            repo = new EFRepository<InventoryType>(context);

            CreateInventoryType window = new CreateInventoryType(this);
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
            InventoryType inv = (InventoryType)item;
            repo.Create(inv);

            try
            {
                repo.Save();
                NotifyPropertyChanged();
                return true;
            }
            catch (DbUpdateException ex)
            {
                repo.Remove(inv);
                return false;
            }
        }
    }
}
