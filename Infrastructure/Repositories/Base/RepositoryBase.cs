using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using YourProjectName.Infrastructure.DataContext;

namespace YourProjectName.Infrastructure.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected YourProjectNameDbContext RepositoryContext;

        public RepositoryBase(YourProjectNameDbContext repositoryContext)
            => RepositoryContext = repositoryContext;

        public IQueryable<TEntity> FindAll(bool trackChanges) =>
            !trackChanges ?
                RepositoryContext.Set<TEntity>()
                    .AsNoTracking() :
                RepositoryContext.Set<TEntity>();

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression,
            bool trackChanges) =>
            !trackChanges ?
                RepositoryContext.Set<TEntity>()
                    .Where(expression)
                    .AsNoTracking() :
                RepositoryContext.Set<TEntity>()
                    .Where(expression);

        public void Create(TEntity entity) => RepositoryContext.Set<TEntity>().Add(entity);

        public void Update(TEntity entity) => RepositoryContext.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => RepositoryContext.Set<TEntity>().Remove(entity);

       

    }

}
