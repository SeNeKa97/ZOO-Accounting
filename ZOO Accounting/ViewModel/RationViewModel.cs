using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ZOO_Accounting.Model.Concrete;
using ZOO_Accounting.Model.Abstract;
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.View;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Data.Entity.Validation;
using ZOO_Accounting.ViewModel;
using System.Windows;
using ZOO_Accounting.View.Unauthorized;



namespace ZOO_Accounting.ViewModel
{
    public class RationViewModel:IViewModel
    {
        private EFDbContext _context;
        private IRepository<Ration> repo;
        private ObservableCollection<Ration> _rations;
        public ObservableCollection<Ration> rations { 
            get {
                return _rations;
            }
            set {
                _rations = value;
                NotifyPropertyChanged();
            }
        }

        public RationViewModel(EFDbContext context) {
            _context = context;
            repo = new EFRepository<Ration>(_context);
            Refresh();

            Window window;
            if(Identity.IsAuthorized())
                window = new Rations(this);
            else
                window = new UnauthRations(this);
            window.ShowDialog();
        }



        public void Refresh() {
            repo.Refresh();
            rations = new ObservableCollection<Ration>(repo.GetItems());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "") {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void CallEdit(object item)
        {
            UpdateRationViewModel updateRation = new UpdateRationViewModel((Ration)item, _context);
            Refresh();
        }

        public void CallCreate()
        {
            CreateRationViewModel createRation = new CreateRationViewModel(_context);
            Refresh();
        }

        public void RemoveItem(object item)
        {
            Ration ration = (Ration)item;

            if (ration != null)
            {
                repo.Remove(ration);
                repo.Save();
                Refresh();
            }
        }
    }
}
