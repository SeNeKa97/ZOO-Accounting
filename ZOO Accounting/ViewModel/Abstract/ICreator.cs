using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_Accounting.ViewModel
{
    public interface ICreator:INotifyPropertyChanged
    {
        bool Create(object item);
    }
}
