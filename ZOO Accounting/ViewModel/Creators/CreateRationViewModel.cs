using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ZOO_Accounting.Model.Concrete;
using ZOO_Accounting.Model.Abstract;
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.View.Creators;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Data.Entity.Infrastructure;

namespace ZOO_Accounting.ViewModel
{
    public class CreateRationViewModel:ICreator
    {
        private IRepository<Ration> _rations;
        public CreateRationViewModel(EFDbContext context)
        {
            _rations = new EFRepository<Ration>(context);

            CreateRation window = new CreateRation(this);
            window.ShowDialog();
        }

        public bool CreateRation(string name, string ingredients) {
            Ration ration = new Ration(name, ingredients);
            _rations.Create(ration);

            try {
                _rations.Save();
                NotifyPropertyChanged();
                return true;
            } catch (DbUpdateException ex) {
                _rations.Remove(ration);
                return false;
            }
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
            Ration ration = (Ration)item;
            _rations.Create(ration);

            try
            {
                _rations.Save();
                NotifyPropertyChanged();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _rations.Remove(ration);
                return false;
            }
        }
    }
}
