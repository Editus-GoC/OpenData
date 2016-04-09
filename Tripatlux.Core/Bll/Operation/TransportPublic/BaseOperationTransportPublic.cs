using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tripatlux.Core.Bll.Operation
{
    public abstract class BaseOperationTransportPublic<T> where T : class
    {
        protected TRANSPORT_PUBLICContext _context;

        public BaseOperationTransportPublic(TRANSPORT_PUBLICContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            DbSet<T> r = _context.Set<T>();
            return r;
        }

        public T GetById(Guid id)
        {
            DbSet<T> r = _context.Set<T>();
            return r.Find(id);
        }

        public virtual T GetByName(String name)
        {
            throw new Exception("Not implemented");
        }

        public virtual T Add(T item)
        {
            AddModify(item, EntityState.Added);
            return item;
        }

        public virtual void RemoveById(Guid id)
        {
            DbSet<T> r = _context.Set<T>();
            T obj = r.Find(id);
            if (obj != default(T))
                r.Remove(r.Find(id));
        }

        public virtual void Modify(T item)
        {
            AddModify(item, EntityState.Modified);
        }

        private void AddModify(T item, EntityState state)
        {
            DbSet<T> r = _context.Set<T>();
            r.Attach(item);
            _context.Entry(item).State = state;
        }

        public void RemoveAll()
        {
            DbSet<T> r = _context.Set<T>();
            r.RemoveRange(r);
        }
    }
}
