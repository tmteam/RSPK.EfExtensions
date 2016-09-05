using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSPK.EfExtensions.Tests
{
    public class EntityWithIdOfInt : IEntity<int>
    {
        
        public int Id{get;set;}

        public bool IsRemoved{get;set;}

        public DateTime? RemoveDate{get;set;}

        public DateTime Created{get;set;}
        public static IQueryable<EntityWithIdOfInt> Range(int start, int count)
        {
            return Enumerable.Range(start, count)
                .Select(i => new EntityWithIdOfInt
                {
                    Created = DateTime.Now,
                    Id = i,
                    IsRemoved = false,
                    RemoveDate = null
                })
                .AsQueryable();
        }
    }
    public class EntityWithIdOfLong : IEntity<long>
    {

        public long Id { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime? RemoveDate { get; set; }

        public DateTime Created { get; set; }

        public static IQueryable<EntityWithIdOfLong> Range(int start, int count)
        {
            return Enumerable.Range(start, count)
                .Select(i => new EntityWithIdOfLong
                {
                    Created = DateTime.Now,
                    Id = (long)i,
                    IsRemoved = false,
                    RemoveDate = null
                })
                .AsQueryable();
        }
    }
    public class EntityWithIdOfString : IEntity<string>
    {
        public string Id { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime? RemoveDate { get; set; }

        public DateTime Created { get; set; }

        public static IQueryable<EntityWithIdOfString> Range(int start, int count)
        {
            return Enumerable.Range(start, count)
                .Select(i => new EntityWithIdOfString
                {
                    Created = DateTime.Now,
                    Id = i.ToString(),
                    IsRemoved = false,
                    RemoveDate = null
                })
                .AsQueryable();
        }
    }
    public class EntityWithIdOfGuid : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime? RemoveDate { get; set; }

        public DateTime Created { get; set; }

        public static IQueryable<EntityWithIdOfGuid> Range(int count)
        {
            return Enumerable
                .Range(0, count)
                .Select(i => new EntityWithIdOfGuid     
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    IsRemoved = false,
                    RemoveDate = null
                })
                .ToArray()
                .AsQueryable();
        }
    }
}
