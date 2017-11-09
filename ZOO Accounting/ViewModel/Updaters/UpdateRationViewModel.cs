using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZOO_Accounting.Model.Abstract;
using ZOO_Accounting.Model.Concrete;
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.View.Updaters;

namespace ZOO_Accounting.ViewModel
{
    public class UpdateRationViewModel:IUpdater
    {
        private IRepository<Ration> repo;
        private Ration _ration;

        public UpdateRationViewModel(Ration ration, EFDbContext context)
        {
            repo = new EFRepository<Ration>(context);
            _ration = ration;

            UpdateRation window = new UpdateRation(this);
            window.ShowDialog();
        }



        public bool Update(object item)
        {
            Ration rat = (Ration)item;
            Ration copy = (Ration)_ration.Clone();
            _ration.Name = rat.Name;
            _ration.Ingredients = rat.Ingredients;

            try
            {
                repo.Update(_ration);
                repo.Save();
                NotifyPropertyChanged();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Backup(_ration, copy);
                return false;
            }
        }
        public void Backup(object obj, object copy)
        {
            Ration item = (Ration)obj;
            Ration cpy = (Ration)copy;

            item.Name = cpy.Name;
            item.Ingredients = cpy.Ingredients;
        }
        public object GetEditableItem()
        {
            return _ration;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
