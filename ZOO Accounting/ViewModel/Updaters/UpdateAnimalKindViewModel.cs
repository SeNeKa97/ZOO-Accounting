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
    public class UpdateAnimalKindViewModel:IUpdater
    {
        private AnimalKind _animalKind;
        private IRepository<AnimalKind> _animalKinds;
        private EFDbContext _context;

        public UpdateAnimalKindViewModel (AnimalKind animalKind, EFDbContext context)
	    {
            _animalKind = animalKind;
            _context = context;
            _animalKinds = new EFRepository<AnimalKind>(_context);

            UpdateAnimalKind window = new UpdateAnimalKind(this);
            window.ShowDialog();
    	}



        public bool Update(object item) {
            AnimalKind kind = (AnimalKind)item;
            AnimalKind copy = (AnimalKind)_animalKind.Clone();
            _animalKind.Name = kind.Name;
            _animalKind.EatingStrategy = kind.EatingStrategy;
            _animalKind.Biome = kind.Biome;

            try
            {
                _animalKinds.Update(_animalKind);
                _animalKinds.Save();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Backup(_animalKind, copy);
                _animalKinds.Update(_animalKind);
                return false;
            }
        }
        public void Backup(object obj, object copy)
        {
            AnimalKind kind = (AnimalKind)obj;
            AnimalKind cpy = (AnimalKind)copy;

            kind.Name = cpy.Name;
            kind.EatingStrategy = cpy.EatingStrategy;
            kind.Biome = cpy.Biome;
        }
        public object GetEditableItem(){
            return _animalKind;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
