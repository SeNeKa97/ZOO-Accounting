using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_Accounting.ViewModel
{
    public interface IViewModel:INotifyPropertyChanged
    {
        void CallEdit(object item);
        void CallCreate();
        void RemoveItem(object item);
        void Refresh();
    }
}
