using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSPK.EfExtensions
{
    public static class QueryableExtensions
    {
        public static TEntity UpdateOrNull<TEntity, TKey>(this DbContext context, TEntity entity) where TEntity : class, IKeyable<TKey>
        {
            var origin = context.Set<TEntity>().Find(entity.Id);
            if (origin != null)
                context.Entry(origin).CurrentValues.SetValues(entity);
            return origin;
        }
        public static TEntity Update<TEntity, TKey>(this DbContext context, TEntity entity) where TEntity : class, IKeyable<TKey>
        {
            var ans = UpdateOrNull<TEntity, TKey>(context, entity);
            if (ans == null)
                throw new UnknownIdException(entity.Id);
            return ans;
        }

        public static T DeleteFromDb<T>(this DbSet<T> dbSet, int id) where T : class, IKeyable<int>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            dbSet.Remove(entity);
            return entity;
        }
        public static T DeleteFromDb<T>(this DbSet<T> dbSet, string id) where T : class, IKeyable<string>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            dbSet.Remove(entity);
            return entity;
        }
        public static T DeleteFromDb<T>(this DbSet<T> dbSet, Guid id) where T : class, IKeyable<Guid>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            dbSet.Remove(entity);
            return entity;
        }
        public static T MarkAsRemoved<T>(this DbSet<T> dbSet, int id) where T : class,  IKeyable<int>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            return dbSet.MarkAsRemoved(entity);
        }
        public static T MarkAsRemoved<T>(this DbSet<T> dbSet, string id) where T : class,  IKeyable<string>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            return dbSet.MarkAsRemoved(entity);
        }
        public static T MarkAsRemoved<T>(this DbSet<T> dbSet, Guid id) where T : class,  IKeyable<Guid>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            return dbSet.MarkAsRemoved(entity);
        }
        public static T MarkAsRemoved<T>(this DbSet<T> dbSet, T entity) where T : class,  IRemoveable
        {
            if (entity.IsRemoved)
                throw new InvalidOperationException("Entity is already deleted");
            entity.IsRemoved = true;
            entity.RemoveDate = DateTime.Now;
            return entity;
        }
        public static void MarkAsRemoved<T>(this T entity) where T : IRemoveable
        {
            if (entity.IsRemoved)
                throw new InvalidOperationException("Entity is already deleted");
            entity.IsRemoved = true;
            entity.RemoveDate = DateTime.Now;
        }
        public static void Initialize(this object entity)
        {
            var guidKey = entity as IKeyable<Guid>;
            if (guidKey != null)
                if (guidKey.Id == Guid.Empty)
                    guidKey.Id = Guid.NewGuid();
            var createable = entity as ICreateable;
            if (createable != null)
                createable.Created = DateTime.Now;

            var removeable = entity as IRemoveable;
            if (removeable != null)
                removeable.IsRemoved = false;
        }

        public static IQueryable<T> Get<T>(this IQueryable<T> query, Guid[] ids) where T : class,  IKeyable<Guid>
        {
            return query.Where(q => ids.Contains(q.Id));
        }
        public static T Get<T>(this IQueryable<T> query, Guid id) where T : class,  IKeyable<Guid>
        {
            var ans = query.GetOrNull(id);
            if (ans == null)
                throw new UnknownIdException(id);
            return ans;
        }
        public static T Get<T>(this IQueryable<T> query, int id) where T : class,  IKeyable<int>
        {
            var ans = query.GetOrNull(id);
            if (ans == null)
                throw new UnknownIdException(id);
            return ans;
        }
        public static T Get<T>(this IQueryable<T> query, long id) where T : class,  IKeyable<long>
        {
            var ans = query.GetOrNull(id);
            if (ans == null)
                throw new UnknownIdException(id);
            return ans;
        }
        public static T Get<T>(this IQueryable<T> query, string id) where T : class,  IKeyable<string>
        {
            var ans = query.GetOrNull(id);
            if (ans == null)
                throw new UnknownIdException(id);
            return ans;
        }
        public static T GetOrNull<T>(this IQueryable<T> query, long id) where T : class, IKeyable<long>
        {
            return query.FirstOrDefault(e => e.Id == id);
        }

        public static T GetOrNull<T>(this IQueryable<T> query, Guid id) where T : class, IKeyable<Guid>
        {
            return query.FirstOrDefault(e => e.Id == id);
        }
        public static T GetOrNull<T>(this IQueryable<T> query, int id) where T : class,  IKeyable<int>
        {
            return query.FirstOrDefault(e => e.Id == id);
        }
        public static T GetOrNull<T>(this IQueryable<T> query, string id) where T : class,  IKeyable<string>
        {
            return query.FirstOrDefault(e => e.Id == id);
        }

        public static IQueryable<T> ThatWasCreated<T>(this IQueryable<T> query, DateTime? includeFrom = null, DateTime? excludeTo = null)
            where T : class,  ICreateable
        {
            IQueryable<T> ans = query;
            if (includeFrom.HasValue)
                ans = ans.Where(a => a.Created >= includeFrom.Value);
            if (excludeTo.HasValue)
                ans = ans.Where(a => a.Created < excludeTo.Value);
            return ans;
        }
        public static IQueryable<T> NotRemoved<T>(this IQueryable<T> query) where T : class, IRemoveable
        {
            return query.Where(q => !q.IsRemoved);
        }
        public static ICollection<T> Sycnronize<T>(this ICollection<T> collection, IDbSet<T> context, Guid[] ids) where T : class, IEntity<Guid>
        {
            var newAgents = context.Get(ids).ToList();
            var oldAgents = collection.ToList();
            var forRemove = oldAgents.Except(newAgents);
            var forAdd = newAgents.Except(oldAgents);

            foreach (var rem in forRemove)
                collection.Remove(rem);

            foreach (var add in forAdd)
                collection.Add(add);

            return collection;
        }
    }
}
