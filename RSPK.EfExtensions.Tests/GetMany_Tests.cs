using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RSPK.EfExtensions.Tests
{
    [TestClass]
    public class GetMany_Tests
    {
        [TestMethod]
        public void GetAnyOf_Positive()
        {
            var array = EntityWithIdOfInt.Range(1, 10).GetAnyOf(1, 2, 3).ToArray();
         
            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array.SingleOrDefault(s => s.Id == 1));
            Assert.IsNotNull(array.SingleOrDefault(s => s.Id == 2));
            Assert.IsNotNull(array.SingleOrDefault(s => s.Id == 3));
        }

        [TestMethod]
        public void GetAnyOf_Partialy()
        {
            var array = EntityWithIdOfInt.Range(1, 10).GetAnyOf(1, 2, 300).ToArray();

            Assert.AreEqual(2, array.Length);
            Assert.IsNotNull(array.SingleOrDefault(s => s.Id == 1));
            Assert.IsNotNull(array.SingleOrDefault(s => s.Id == 2));
        }

        [TestMethod]
        public void GetAnyOf_Negative()
        {
            var array = EntityWithIdOfInt.Range(1, 10).GetAnyOf(100, 200, 300).ToArray();
            Assert.AreEqual(0, array.Length);
        }


        [TestMethod]
        public void GetAllOf_Positive()
        {
            var array = EntityWithIdOfInt.Range(1, 10).GetAllOf(1, 2, 3).ToArray();

            Assert.AreEqual(3, array.Length);
            Assert.IsNotNull(array.SingleOrDefault(s => s.Id == 1));
            Assert.IsNotNull(array.SingleOrDefault(s => s.Id == 2));
            Assert.IsNotNull(array.SingleOrDefault(s => s.Id == 3));
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownIdsException))]
        public void GetAllOf_Partialy()
        {
            var array = EntityWithIdOfInt.Range(1, 10).GetAllOf(1, 2, 300);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAllOf_Negative()
        {
            EntityWithIdOfInt.Range(1, 10).GetAllOf(new int[0]);
        }
    }
}
