using BicycleStore.Core.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleStore.Core.Repositories
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class, IGuidKey, new()
    {
        private DbContext database;
        private DbSet<T> table;

        public AbstractRepository(DbContext dbContext)
        {
            database = dbContext;
            table = database.Set<T>();
        }

        public void CreateOrUpdate(T entity, Guid id = default(Guid))
        {
            var target = new T
            {
                Id = id
            };
            if(target.Id != default(Guid))
            {
                var entry = table.Attach(target);
                foreach (var item in entity.GetType().GetProperties())
                    entry.Entity
                        .GetType()
                        .GetProperty(item.Name)
                        .SetValue(entry.Entity,item.GetValue(entity, null), null);

                entry.State = EntityState.Modified;
            }
            
            else
                database.Set<T>().Add(entity);
           
          
            
        }

        
        public void Delete(T entity)
        {
            table.Remove(entity);
            SaveChanges();
        }

        public void Delete(Guid id)
        {
            table.Remove(Get(id));
            SaveChanges();
        }

        public T Get(Guid id)
        {
            return table.Find(id);
        }

        public void SaveChanges()
        {
          
            database.SaveChanges();
           
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }
    }
}
