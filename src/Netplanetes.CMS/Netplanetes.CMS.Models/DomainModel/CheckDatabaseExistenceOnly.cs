using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Models.DomainModel
{
    public class CheckDatabaseExistenceOnly<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
            if (context.Database.Exists())
            {
                return;
            }
            throw new ApplicationException(string.Format("Database {0} does not exist.", context.Database.Connection.Database));
        }

    }
}
