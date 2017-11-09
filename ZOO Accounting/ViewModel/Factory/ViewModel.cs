using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZOO_Accounting.Model.Concrete;

namespace ZOO_Accounting.ViewModel.Factory
{
    public static class ViewModelFactory
    {
        public static IViewModel Create(string tag, EFDbContext context) {
            IViewModel model;
            switch (tag)
            {
                case "animal":
                    model = new AnimalViewModel(context);
                    break;
                case "animalKind":
                    model = new AnimalKindViewModel(context);
                    break;
                case "ration":
                    model = new RationViewModel(context);
                    break;
                case "inventory":
                    model = new InventoryViewModel(context);
                    break;
                case "inventoryType":
                    model = new InventoryTypeViewModel(context);
                    break;
                case "aviaryType":
                    model = new AviaryTypeViewModel(context);
                    break;
                case "aviary":
                    model = new AviaryViewModel(context);
                    break;
                default:
                    model = null;
                    break;
            }

            return model;
        }
    }
}
