using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RSPK.EfExtensions.Tests
{
    [TestClass]
    public class Get_Tests
    {
        [TestMethod]
        public void GetInt()
        {
            Assert.AreEqual(2, EntityWithIdOfInt.Range(1, 10).Get(2).Id);
        }
        [TestMethod]
        [ExpectedException(typeof(UnknownIdException))]
        public void GetIntFail()
        {
            EntityWithIdOfInt.Range(1, 10).Get(100);
        }
        [TestMethod]
        public void GetLong()
        {
            Assert.AreEqual((long)2, EntityWithIdOfLong.Range(0, 10).Get((long)2).Id);
        }
        [TestMethod]
        [ExpectedException(typeof(UnknownIdException))]
        public void GetLongFail()
        {
            EntityWithIdOfLong.Range(0, 10).Get((long)100);
        }

        [TestMethod]
        public void GetStr()
        {
            Assert.AreEqual("2", EntityWithIdOfString.Range(1, 10).Get("2").Id);
        }
        [TestMethod]
        [ExpectedException(typeof(UnknownIdException))]
        public void GetStrFail()
        {
            EntityWithIdOfString.Range(1, 10).Get("100");
        }

        [TestMethod]
        public void GetGuid()
        {
            var query = EntityWithIdOfGuid.Range(10);
            var id = query.First().Id;
            Assert.AreEqual(id, query.Get(id).Id);
        }
        [TestMethod]
        [ExpectedException(typeof(UnknownIdException))]
        public void GetGuidFail()
        {
            EntityWithIdOfGuid.Range(10).Get(Guid.NewGuid());
        }
    }
}
