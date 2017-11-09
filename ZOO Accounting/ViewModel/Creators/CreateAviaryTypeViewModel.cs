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
using ZOO_Accounting.View.Creators;

namespace ZOO_Accounting.ViewModel
{
    public class CreateAviaryTypeViewModel:ICreator
    {
        EFDbContext _context;
        IRepository<AviaryType> repo;
        public CreateAviaryTypeViewModel(EFDbContext context)
        {
            repo = new EFRepository<AviaryType>(context);

            Window window = new CreateAviaryType(this);
            window.Show();
        }
        public bool Create(object item)
        {
            AviaryType  avType = (AviaryType)item;

            repo.Create(avType);
            try {
                repo.Save();
                return true;
            } catch (DbUpdateException ex) {
                repo.Remove(avType);
                return false;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
