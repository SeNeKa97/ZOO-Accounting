using System;
using System.Collections.Generic;
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
    public class UpdateAviaryTypeViewModel:IUpdater
    {
        AviaryType _type;
        IRepository<AviaryType> repo;
        public UpdateAviaryTypeViewModel(AviaryType item,EFDbContext context)
        {
            _type = item;
            repo = new EFRepository<AviaryType>(context);

            Window window = new UpdateAviaryType(this);
            window.ShowDialog();
        }
        public bool Update(object item)
        {
            AviaryType type = (AviaryType)item;
            AviaryType copy = (AviaryType)_type.Clone();

            _type.Name = type.Name;

            try
            {
                repo.Update(_type);
                repo.Save();
                return true;
            }
            catch (DbUpdateException ex) {
                Backup(_type, copy);
                repo.Update(_type);
                return false;
            }
        }

        public void Backup(object obj, object copy)
        {
            AviaryType item = (AviaryType)obj;
            AviaryType cpy = (AviaryType)copy;

            item.Name = cpy.Name;
        }

        public object GetEditableItem()
        {
            return _type;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
