using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Documaster.Model.BaseEntities;

namespace Documaster.Business.Services
{
    public interface IGenericRepository<TEntity> : IService
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity Get( int id );
        IEnumerable<TEntity> Get( Expression<Func<TEntity, bool>> expression );
        TEntity Create( TEntity entity );
        bool Update( TEntity entity, IEnumerable<string> propertiesToUpdate );
        bool Delete( int id );
    }
}