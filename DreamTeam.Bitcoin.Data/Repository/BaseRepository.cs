using BitcoinAppMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeam.Bitcoin.Data.Repository
{
    /// <summary>
    /// Base class for "repository" data-pattern
    /// </summary>
    /// <typeparam name="T">type of entity</typeparam>
    /// <typeparam name="DcT">data context for store data and all operations</typeparam>
    public class BaseRepository<T, DcT> 
        where DcT : DbContext where T:class, IEntity, new()
    {
        protected readonly DcT DataContext;

        public BaseRepository(DcT dataContext)
        {
            DataContext = dataContext;
        }


        public IEnumerable<T> LastChanged(int count, Func<T, bool> filter)
        {
            GC.Collect();

            return Set().Where(filter)
                .OrderByDescending(c => c.UTCChanged).Take(count);
        }

        #region CRUD

        public bool Exists(Guid id)
        {
            return Set().FirstOrDefault(e => e.ID == id) != null;
        }

        public virtual Guid Add(T entity)
        {
            var addedEntry = Set().Add(entity);
            this.DataContext.SaveChanges();
            return addedEntry.Entity.ID;
        }

        public virtual void Update(T entity)
        {
           // entity.Validate();
            Set().Attach(entity);
            Set().Update(entity);
        }


        public virtual Task<T> GetAsync(Guid id)
        {
            return Set().SingleOrDefaultAsync(e => e.ID == id);
        }


        public virtual T Get(Guid id)
        {
            return Set().SingleOrDefault(e => e.ID == id);
        }

        public List<T> List()
        {
            return Set().ToList();
        }

        /// <summary>
        ///     Объект таблицы для кастомных запросов
        /// </summary>
        /// <returns></returns>
        protected DbSet<T> Set()
        {
            DbSet<T> dbSet = DataContext.Set<T>();
            return dbSet;
        }


        public void Remove(Guid id)
        {
            var res = Set().SingleOrDefault(s => s.ID == id);
            if (res == null) throw new DataException();
            Set().Remove(res);
        }

        #endregion
    }
}
