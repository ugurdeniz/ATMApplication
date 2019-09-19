using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAL
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        List<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetQuery();

        int HardDelete(TEntity entity);
        int Update(TEntity entity);
        int Add(TEntity entity);
    }
}
