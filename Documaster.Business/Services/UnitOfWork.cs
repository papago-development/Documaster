using System.Collections.Generic;
using Documaster.Business.Wrappers;
using Documaster.Data.DataAccess;
using Documaster.Model.BaseEntities;

namespace Documaster.Business.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;
        private readonly IDependencyContainerWrapper _dependencyContainerWrapper;
        private Dictionary<string, object> _repositories;

        public UnitOfWork( IDbContext dbContext, IDependencyContainerWrapper dependencyContainerWrapper )
        {
            _dbContext = dbContext;
            _dependencyContainerWrapper = dependencyContainerWrapper;
        }

        public bool SaveChanges()
        {
            var stateEntries = _dbContext.SaveChanges();
            return stateEntries != 0;
        }

        public IGenericRepository<TEntity> Repository<TEntity>()
                where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var entityType = typeof(TEntity);
            if (!_repositories.ContainsKey(entityType.Name))
            {
                var openServiceType = typeof(IGenericRepository<>);
                var closedServiceType = openServiceType.MakeGenericType(entityType);
                var repositoryInstance = _dependencyContainerWrapper.Resolve(closedServiceType);
                _repositories.Add(entityType.Name, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[entityType.Name];
        }
    }
}