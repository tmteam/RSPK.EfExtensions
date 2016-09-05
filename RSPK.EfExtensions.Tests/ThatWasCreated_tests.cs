using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSPK.EfExtensions;

namespace RSPK.EfExtensions.Tests
{
    [TestClass]
    class ThatWasCreated_Tests
    {
        [TestMethod]
        public void Null_Null(){
            var array = CreateTestArray();
            
            var areEqual = array
                .AsQueryable()
                .ThatWasCreated(includeFrom:null , excludeTo:null)
                .SequenceEqual(array);
            
            Assert.IsTrue(areEqual);
        }
        [TestMethod]
        public void From_Null()
        {
            var array = CreateTestArray();

            var from = new DateTime(1990,01,02);
            var result = array.AsQueryable()
                .ThatWasCreated(includeFrom:from, excludeTo: null).ToArray();
            Assert.IsNull(result.AsQueryable().GetOrNull(1));
            result.AsQueryable().GetAllOf(2,3,4,5,6,7,8,9,10);
        }

        [TestMethod]
        public void From_To()
        {
            var array = CreateTestArray();

            var from = new DateTime(1990, 01, 02);
            var to = new DateTime(1990, 01, 08);
           
            var result = array.AsQueryable()
                .ThatWasCreated(includeFrom: from, excludeTo: to).ToArray();
           
            Assert.IsFalse(result.AsQueryable().GetAnyOf(1,8,9).Any());

            result.AsQueryable().GetAllOf(2, 3, 4, 5, 6, 7);
        }
        [TestMethod]
        public void Null_To()
        {
            var array = CreateTestArray();

            var to = new DateTime(1990, 01, 08);
           
            var result = array.AsQueryable()
                .ThatWasCreated(includeFrom: null, excludeTo: to).ToArray();
            Assert.IsFalse(result.AsQueryable().GetAnyOf(8, 9).Any());

            result.AsQueryable().GetAllOf(1,2, 3, 4, 5, 6, 7, 8, 9, 10);
        }
        EntityWithIdOfInt[] CreateTestArray()
        {
            var array = EntityWithIdOfInt.Range(1, 10).ToArray();
            for (int i = 0; i < 10; i++)
            {
                array[i].Created = new DateTime(1990, 01, i);
            }
            return array;
        }
    }
}
