using System.Collections.Generic;
using Documaster.Model.BaseEntities;

namespace Documaster.Business.Services
{
    public interface IGenericEntityService<TEntity> : IService
        where TEntity : BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get( int id );
        TEntity Create( TEntity entity );
        bool Update( TEntity entity, IEnumerable<string> propertiesToUpdate );
        bool Delete( int id );
    }
}