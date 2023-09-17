using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ireview.Infrastructure.Contexts.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        DbContext context;
        public DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public TEntity Find(string id)
        {
            return dbSet.Find(id);
        }

        public void Create(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
            context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }

    }
}
