using eCommerce.Models;
using eCommerce.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace eCommerce.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private MobileMineContext mmContext = new MobileMineContext();
        public Repository(MobileMineContext mmContext)
        {
            this.mmContext = mmContext;
        }

        public TEntity GetById(int id)
        {
            return mmContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return mmContext.Set<TEntity>().ToList();
        }

        public void Insert(TEntity entity)
        {
            mmContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            mmContext.Set<TEntity>().Remove(entity);
        }
        public int Submit()
        {
            return mmContext.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            mmContext.Entry(entity).State = EntityState.Modified;
        }
    }
}