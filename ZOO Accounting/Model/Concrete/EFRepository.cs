using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZOO_Accounting.Model.Abstract;
using System.Data.Entity;
using System.Windows;

namespace ZOO_Accounting.Model.Concrete
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private EFDbContext context;
        private DbSet<TEntity> dbSet;
        private bool disposed = false;

        public EFRepository(EFDbContext context) {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public void Create(TEntity item) {
            dbSet.Add(item);  
        }

        public IEnumerable<TEntity> GetItems() {
            return dbSet;
        }

        public TEntity GetItem(int id) {
            return dbSet.Find(id);
        }

        public void Update(TEntity item) {
            dbSet.Attach(item);
            context.Entry(item).State = EntityState.Modified;
        }


        public void Remove(int id) {
            TEntity item = dbSet.Find(id);
            Remove(item);
        }

        public void Remove(TEntity item)
        {
            if (item != null)
                dbSet.Remove(item);
        }

        public void Save() {
            context.SaveChanges();
        }

        public virtual void Dispose(bool disposing) {
            if (!this.disposed){
                if (disposing){
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Refresh()
        {
            this.dbSet = context.Set<TEntity>();
        }
    }
}
