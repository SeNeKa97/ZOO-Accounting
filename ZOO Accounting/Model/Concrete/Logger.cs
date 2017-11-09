using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZOO_Accounting.Model.Abstract;
using ZOO_Accounting.Model.Entities;

namespace ZOO_Accounting.Model.Concrete
{
    public static class Logger
    {
        public static void Log(string text)
        {
            using (EFDbContext context = new EFDbContext()) {
                IRepository<LogRecord> repo = new EFRepository<LogRecord>(context);
                LogRecord record = new LogRecord(DateTime.Now, text);
                repo.Create(record);
                repo.Save();
            }
        }
    }
}
