using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RSPK.EfExtensions.Tests
{
    [TestClass]
    public class GetOrNullTests
    {
        [TestMethod]
        public void GetOrNullInt()
        {
            Assert.AreEqual(2, EntityWithIdOfInt.Range(1, 10).GetOrNull(2).Id);
        }
        [TestMethod]
        public void GetIntOrNullFail()
        {
            Assert.IsNull(EntityWithIdOfInt.Range(1, 10).GetOrNull(100));
        }
        [TestMethod]
        public void GetOrNullLong()
        {
            Assert.AreEqual((long)2, EntityWithIdOfLong.Range(0, 10).GetOrNull((long)2).Id);
        }
        [TestMethod]
        public void GetOrNullLongFail()
        {
            Assert.IsNull(EntityWithIdOfLong.Range(0, 10).GetOrNull((long)100));
        }

        [TestMethod]
        public void GetOrNullStr()
        {
            Assert.AreEqual("2", EntityWithIdOfString.Range(1, 10).GetOrNull("2").Id);
        }
        [TestMethod]
        public void GetOrNullStrFail()
        {
            Assert.IsNull(EntityWithIdOfString.Range(1, 10).GetOrNull("100"));
        }

        [TestMethod]
        public void GetOrNullGuid()
        {
            var query = EntityWithIdOfGuid.Range(10);
            var id = query.First().Id;
            Assert.AreEqual(id, query.GetOrNull(id).Id);
        }
        [TestMethod]
        public void GetGuidOrNullFail()
        {
            Assert.IsNull(EntityWithIdOfGuid.Range(10).GetOrNull(Guid.NewGuid()));
        }
    }
}
