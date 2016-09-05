using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace RSPK.EfExtensions
{
    public static class EntityExtensions
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

        public static TEntity DeleteFromDb<TEntity>(this DbSet<TEntity> dbSet, long id) where TEntity : class, IKeyable<long>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            dbSet.Remove(entity);
            return entity;
        }
        public static TEntity DeleteFromDb<TEntity>(this DbSet<TEntity> dbSet, int id) where TEntity : class, IKeyable<int>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            dbSet.Remove(entity);
            return entity;
        }
        public static TEntity DeleteFromDb<TEntity>(this DbSet<TEntity> dbSet, string id) where TEntity : class, IKeyable<string>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            dbSet.Remove(entity);
            return entity;
        }
        public static TEntity DeleteFromDb<TEntity>(this DbSet<TEntity> dbSet, Guid id) where TEntity : class, IKeyable<Guid>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            dbSet.Remove(entity);
            return entity;
        }

        public static TEntity MarkAsRemoved<TEntity>(this DbSet<TEntity> dbSet, long id) where TEntity : class,  IKeyable<long>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            return dbSet.MarkAsRemoved(entity);
        }
        public static TEntity MarkAsRemoved<TEntity>(this DbSet<TEntity> dbSet, int id) where TEntity : class,  IKeyable<int>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            return dbSet.MarkAsRemoved(entity);
        }
        public static TEntity MarkAsRemoved<TEntity>(this DbSet<TEntity> dbSet, string id) where TEntity : class,  IKeyable<string>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            return dbSet.MarkAsRemoved(entity);
        }
        public static TEntity MarkAsRemoved<TEntity>(this DbSet<TEntity> dbSet, Guid id) where TEntity : class,  IKeyable<Guid>, IRemoveable
        {
            var entity = dbSet.NotRemoved().Get(id);
            return dbSet.MarkAsRemoved(entity);
        }
        public static TEntity MarkAsRemoved<TEntity>(this DbSet<TEntity> dbSet, TEntity entity) where TEntity : class,  IRemoveable
        {
            if (entity.IsRemoved)
                throw new InvalidOperationException("Entity is already deleted");
            entity.IsRemoved = true;
            entity.RemoveDate = DateTime.Now;
            return entity;
        }
        public static void MarkAsRemoved<TEntity>(this TEntity entity) where TEntity : IRemoveable
        {
            if (entity.IsRemoved)
                throw new InvalidOperationException("Entity is already deleted");
            entity.IsRemoved = true;
            entity.RemoveDate = DateTime.Now;
        }

        /// <summary>
        /// Returns query for entities, with id equal to one of the listed. 
        /// Returns empty query if no entities will be found.
        /// </summary>
        public static IQueryable<TEntity> GetAnyOf<TEntity, TKey>(this IQueryable<TEntity> query, params TKey[] ids) where TEntity : class,  IKeyable<TKey>
        {
            return query.Where(q => ids.Contains(q.Id));
        }
        public static TEntity[] GetAllOf<TEntity, TKey>(this IQueryable<TEntity> query, params TKey[] ids) where TEntity : class,  IKeyable<TKey>
        {
            if (!ids.Any())
                throw new ArgumentException("parameter ids does not contain any elements");
            var ans = query.Where(q => ids.Contains(q.Id)).ToArray();
            if (ans.Length == ids.Length)
                return ans;
            throw new UnknownIdsException(ids.Except(ans.Select(a => a.Id)).Cast<object>().ToArray());
        }
        public static TEntity Get<TEntity>(this IQueryable<TEntity> query, Guid id) where TEntity : class,  IKeyable<Guid>
        {
            var ans = query.GetOrNull(id);
            if (ans == null)
                throw new UnknownIdException(id);
            return ans;
        }
        public static TEntity Get<TEntity>(this IQueryable<TEntity> query, int id) where TEntity : class,  IKeyable<int>
        {
            var ans = query.GetOrNull(id);
            if (ans == null)
                throw new UnknownIdException(id);
            return ans;
        }
        public static TEntity Get<TEntity>(this IQueryable<TEntity> query, long id) where TEntity : class,  IKeyable<long>
        {
            var ans = query.GetOrNull(id);
            if (ans == null)
                throw new UnknownIdException(id);
            return ans;
        }
        public static TEntity Get<TEntity>(this IQueryable<TEntity> query, string id) where TEntity : class,  IKeyable<string>
        {
            var ans = query.GetOrNull(id);
            if (ans == null)
                throw new UnknownIdException(id);
            return ans;
        }

        public static TEntity GetOrNull<TEntity>(this IQueryable<TEntity> query, long id) where TEntity : class, IKeyable<long>
        {
            return query.FirstOrDefault(e => e.Id == id);
        }
        public static TEntity GetOrNull<TEntity>(this IQueryable<TEntity> query, Guid id) where TEntity : class, IKeyable<Guid>
        {
            return query.FirstOrDefault(e => e.Id == id);
        }
        public static TEntity GetOrNull<TEntity>(this IQueryable<TEntity> query, int id) where TEntity : class,  IKeyable<int>
        {
            return query.FirstOrDefault(e => e.Id == id);
        }
        public static TEntity GetOrNull<TEntity>(this IQueryable<TEntity> query, string id) where TEntity : class,  IKeyable<string>
        {
            return query.FirstOrDefault(e => e.Id == id);
        }

        public static IQueryable<TEntity> ThatWasCreated<TEntity>(this IQueryable<TEntity> query, DateTime? includeFrom = null, DateTime? excludeTo = null)
            where TEntity : class,  ICreateable
        {
            IQueryable<TEntity> ans = query;
            if (includeFrom.HasValue)
                ans = ans.Where(a => a.Created >= includeFrom.Value);
            if (excludeTo.HasValue)
                ans = ans.Where(a => a.Created < excludeTo.Value);
            return ans;
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

        public static IQueryable<TEntity> NotRemoved<TEntity>(this IQueryable<TEntity> query) where TEntity : class, IRemoveable
        {
            return query.Where(q => !q.IsRemoved);
        }
        public static ICollection<TEntity> Sycnronize<TEntity>(this ICollection<TEntity> collection, IDbSet<TEntity> context, Guid[] ids) where TEntity : class, IEntity<Guid>
        {
            var newAgents = context.GetAnyOf(ids).ToList();
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
