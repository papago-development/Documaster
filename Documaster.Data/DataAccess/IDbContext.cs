using System;
using System.Collections.Generic;
using System.Linq;
using Documaster.Model.BaseEntities;

namespace Documaster.Data.DataAccess
{
    public interface IDbContext : IDisposable
    {

        IQueryable<TEntity> Get<TEntity>() where TEntity : BaseEntity;

        TEntity Create<TEntity>(TEntity entity) where TEntity : BaseEntity;

        void Update<TEntity>(TEntity originalEntity, TEntity updatedEntity, IEnumerable<string> propertiesToUpdate) where TEntity : BaseEntity;

        TEntity Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;

        int SaveChanges();
    }
}