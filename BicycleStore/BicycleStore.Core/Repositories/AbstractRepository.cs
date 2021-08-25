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
            var entry = table.Attach(target);

            if(entry.State != EntityState.Added)
            {
                foreach (var property in entry.Properties)
                {
                    var original = property.OriginalValue;
                    var current = property.CurrentValue;

                    if (ReferenceEquals(original, current))
                    {
                        continue;
                    }

                    if (original == null)
                    {
                        property.IsModified = true;
                        continue;
                    }

                    var propertyIsModified = !original.Equals(current);
                    property.IsModified = propertyIsModified;
                }
            }
            
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
