using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BackEnd.BAL.Interfaces;
using BackEnd.DAL.Context;

namespace BackEnd.BAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal BakEndContext Context;

        internal DbSet<T> DbSet;

        public GenericRepository(BakEndContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int page = 0, string noTrack = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (noTrack != "")
            {
                query = query.AsNoTracking();
            }

            query = includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                // Paging
                return page > 0 ? query.ToList().Skip(page - 1).Take(1) : query.ToList();
            }
        }

        public virtual T GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public virtual T GetEntity(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            return query.SingleOrDefault();

        }

        public virtual async Task<T> GetAsyncByID(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual T Last()
        {
            return DbSet.LastOrDefault();
        }
        public int Count()
        {
            return DbSet.Count();
        }

        public virtual void Update(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public void RemovRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public void RemovRange(IEnumerable<T> entities, string noTrack = "")
        {
            Context.Set<T>().RemoveRange(entities);
        }



        public virtual IEnumerable<T> GetMobilApp(Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string includeProperties = "", int page = 0, string NoTrack = "")
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (NoTrack != "")
            {
                query = query.AsNoTracking();
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                // Paging
                if (page > 0)
                {
                    page *= 5;
                    return query.ToList().Skip(page - 5).Take(5);
                }
                else
                    return query.ToList();
            }
        }

    }
}
