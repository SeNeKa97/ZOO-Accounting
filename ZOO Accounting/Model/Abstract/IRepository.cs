using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_Accounting.Model.Abstract
{
    public interface IRepository<TEntity>:IDisposable
    {
        void Create(TEntity item);
        IEnumerable<TEntity> GetItems();
        TEntity GetItem(int id);
        void Update(TEntity item);
        void Remove(int id);
        void Remove(TEntity item);
        void Save();
        void Refresh();
    }
}
