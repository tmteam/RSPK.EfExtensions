using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSPK.EfExtensions
{
    public interface IEntity<TEntity> : IKeyable<TEntity>, IRemoveable, ICreateable
    {
    }
}
