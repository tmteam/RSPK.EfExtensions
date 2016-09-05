using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSPK.EfExtensions
{
    public class UnknownIdException : Exception
    {
        public UnknownIdException() { }
        public UnknownIdException(string message) : base(message) { }
        public UnknownIdException(object id)
        {
            this.Id = id;
        }
        public UnknownIdException(string message, object id)
            : base(message)
        {
            this.Id = id;
        }
        public object Id { get; private set; }
    }
}
