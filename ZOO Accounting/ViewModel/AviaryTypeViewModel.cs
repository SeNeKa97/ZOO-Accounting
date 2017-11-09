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
    public class AviaryTypeViewModel:IViewModel
    {
        private ObservableCollection<AviaryType> _aviaryTypes;
        public ObservableCollection<AviaryType> aviaryTypes {
            get
            {
                return _aviaryTypes;
            }
            set
            {
                _aviaryTypes = value;
                NotifyPropertyChanged();
            }
        }
        public IRepository<AviaryType> repo;
        public EFDbContext _context;

        public AviaryTypeViewModel(EFDbContext context)
        {
            _context = context;
            repo = new EFRepository<AviaryType>(context);

            Refresh();

            Window window;
            if (Identity.IsAuthorized())
                window = new AviaryTypes(this);
            else
                window = new UnauthAviaryTypes(this);
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
            if (item != null)
            {
                UpdateAviaryTypeViewModel update = new UpdateAviaryTypeViewModel((AviaryType)item, _context);
                Refresh();
            }
        }

        public void CallCreate()
        {
            CreateAviaryTypeViewModel create = new CreateAviaryTypeViewModel(_context);
        }

        public void RemoveItem(object item)
        {
            if (item != null) {
                repo.Remove((AviaryType)item);
                repo.Save();
                Refresh();
            }
        }

        public void Refresh()
        {
            repo.Refresh();
            aviaryTypes = new ObservableCollection<AviaryType>(repo.GetItems());
        }
    }
}
