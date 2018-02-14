using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Documaster.Model.BaseEntities;

namespace Documaster.Business.Services
{
    public interface IGenericEntityService<TEntity> : IService
        where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get( int id );
        IEnumerable<TEntity> Get( Expression<Func<TEntity, bool>> expression );
        TEntity Create( TEntity entity );
        bool Update( TEntity entity, IEnumerable<string> propertiesToUpdate );
        bool Delete( int id );
    }
}