using Invoice.Core.Entity.Base;
using Invoice.Core.Interfaces.Base;
using Invoice.Data.AppDataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Invoice.Data.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly InvoiceDbContext _context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public BaseRepository(InvoiceDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return entities.Where(x => !x.IsDelete);
        }

        public IQueryable<T> All(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }

        public T Find(long id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public T Find(long id, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.CreateDate = DateTime.UtcNow;
            entity.UpdateDate = DateTime.UtcNow;
            entities.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity, int id)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            T exist = _context.Set<T>().Find(id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
            _context.SaveChanges();
        }
    }
}
