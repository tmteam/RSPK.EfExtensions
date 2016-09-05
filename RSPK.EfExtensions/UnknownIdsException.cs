using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSPK.EfExtensions
{
    public class UnknownIdsException : Exception
    {
        public UnknownIdsException() { }
        public UnknownIdsException(string message) : base(message) { }
        public UnknownIdsException(object[] ids)
        {
            this.Ids = ids;
        }
        public UnknownIdsException(string message, object[] ids)
            : base(message)
        {
            this.Ids = ids;
        }
        public object[] Ids { get; private set; }
    }
}
