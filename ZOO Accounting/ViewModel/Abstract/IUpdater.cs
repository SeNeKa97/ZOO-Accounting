using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_Accounting.ViewModel
{
    interface IUpdater:INotifyPropertyChanged
    {
        bool Update(object item);
        void Backup(object obj, object copy);
        object GetEditableItem();
    }
}
