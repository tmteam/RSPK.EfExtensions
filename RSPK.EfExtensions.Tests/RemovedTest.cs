using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSPK.EfExtensions.Tests
{
    [TestClass]
    public class RemovedTest
    {
        [TestMethod]
        public void MarkAsRemoved()
        {
            var entity = new EntityWithIdOfInt();
            entity.Initialize();
            entity.MarkAsRemoved();
            Assert.IsTrue(entity.IsRemoved);
            Assert.IsNotNull(entity.RemoveDate);
        }
        [TestMethod]
        public void NotRemovedPositive()
        {
            var collection = EntityWithIdOfInt.Range(1, 10).ToArray();

            collection.AsQueryable().Get(1).MarkAsRemoved();
            collection.AsQueryable().Get(2).MarkAsRemoved();
            collection.AsQueryable().Get(3).MarkAsRemoved();
            collection.AsQueryable().Get(4).MarkAsRemoved();
            collection.AsQueryable().Get(5).MarkAsRemoved();

            Assert.IsNotNull(collection.AsQueryable().NotRemoved().GetOrNull(6));
        }
        [TestMethod]
        public void NotRemovedNegative()
        {
            var collection = EntityWithIdOfInt.Range(1, 10).ToArray();

            collection.AsQueryable().Get(1).MarkAsRemoved();

            Assert.IsNull(collection.AsQueryable().NotRemoved().GetOrNull(1));
        }
    }
}
