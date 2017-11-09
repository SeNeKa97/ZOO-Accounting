using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ZOO_Accounting.Model.Entities;
using ZOO_Accounting.Model.Concrete;
using ZOO_Accounting.Model.Abstract;
using System.Data.Entity.Infrastructure;
using ZOO_Accounting.View.Creators;

namespace ZOO_Accounting.ViewModel
{
    public class CreateAnimalKindViewModel:ICreator
    {
        private EFDbContext _context;
        private IRepository<AnimalKind> _animalKinds;

        public CreateAnimalKindViewModel(EFDbContext context) {
            _context = context;
            _animalKinds = new EFRepository<AnimalKind>(_context);

            CreateAnimalKind window = new CreateAnimalKind(this);
            window.ShowDialog();
        }

        

        public bool AddAnimalKind(string name, EatingStrategy eatingStrategy, Biome biome) {
            AnimalKind kind = new AnimalKind(name, biome, eatingStrategy);
            _animalKinds.Create(kind);

            try{
                _animalKinds.Save();
                NotifyPropertyChanged();
                return true;
            } catch (DbUpdateException ex) {
                _animalKinds.Remove(kind);
                return false;
            }
        }

        protected void Backup(AnimalKind obj, AnimalKind copy) {
            obj.Name = copy.Name;
            obj.EatingStrategy = copy.EatingStrategy;
            obj.Biome = copy.Biome;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Create(object item)
        {
            AnimalKind kind = (AnimalKind)item;

            _animalKinds.Create(kind);

            try
            {
                _animalKinds.Save();
                NotifyPropertyChanged();
                return true;
            }
            catch (DbUpdateException ex)
            {
                _animalKinds.Remove(kind);
                return false;
            }
        }
    }
}
