using System;
using System.Linq;
using System.Linq.Expressions;

namespace Contracts;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool tracking);
    IQueryable<T> FindByCondation(Expression<Func<T, bool>> expression, bool tracking);
    void Create (T entity);
    void Update (T entity);
    void Delete (T entity);
}
