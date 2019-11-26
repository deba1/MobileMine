using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {

        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();

        void Insert(TEntity entity);
        void Delete(TEntity entity);

        int Submit();

        void Update(TEntity entity);
    }
}