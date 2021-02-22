using Invoice.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Invoice.Core.Interfaces.Base
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        IQueryable<T> All();
        IQueryable<T> All(params Expression<Func<T, Object>>[] includeProperties);
        T Find(long id);
        T Find(Int64 id, params Expression<Func<T, object>>[] includeProperties);
        void Insert(T entity);
        void Update(T entity, int id);
        void Delete(T entity);
    }
}
