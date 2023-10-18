using System;
using System.Linq;
using System.Linq.Expressions;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly RepositoryContext repositoryContext;

    public RepositoryBase(RepositoryContext repositoryContext)
    {
        this.repositoryContext = repositoryContext ??
            throw new ArgumentNullException(nameof(repositoryContext));
    }

    public virtual void Create(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual IQueryable<T> FindAll(bool tracking) =>
        !tracking ?
        repositoryContext.Set<T>().AsNoTracking() :
        repositoryContext.Set<T>();
   
    public virtual IQueryable<T> FindByCondation(
        Expression<Func<T, bool>> expression,
        bool tracking) =>
            !tracking ? repositoryContext.Set<T>()
                .Where(expression).AsNoTracking() :
            repositoryContext.Set<T>()
                .Where(expression);

}

