using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Documaster.Data.DataAccess;
using Documaster.Model.BaseEntities;

namespace Documaster.Business.Services
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
            where TEntity : BaseEntity
    {
        private readonly IDbContext _dbContext;

        public GenericRepository( IDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Get<TEntity>().OrderBy( x => x.LastUpdate ).ToList();
        }

        public TEntity Get( int id )
        {
            return _dbContext.Get<TEntity>().SingleOrDefault( x => x.Id == id );
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> expression)
        {
            return _dbContext.Get<TEntity>().Where( expression ).ToList();
        }

        public TEntity Create( TEntity entity )
        {
            if ( entity == null )
            {
                throw new ArgumentNullException( nameof(entity) );
            }

            entity.CreationDate = DateTime.Now;
            entity.LastUpdate = DateTime.Now;
            var savedEntity = _dbContext.Create( entity );
            return savedEntity;
        }

        // Disconnected update
        public bool Update( TEntity entity, IEnumerable<string> propertiesToUpdate )
        {
            if ( entity == null )
            {
                throw new ArgumentNullException( nameof(entity) );
            }

            var existingEntity = Get( entity.Id );
            if ( existingEntity == null )
            {
                return false;
            }

            entity.LastUpdate = DateTime.Now;
            var completeListOfProperties = propertiesToUpdate.ToList();
            completeListOfProperties.AddRange( new List<string> {"LastUpdate"} );

            _dbContext.Update( existingEntity, entity, completeListOfProperties );
            return true;
        }

        public bool Delete( int id )
        {
            var entityToDelete = _dbContext.Get<TEntity>().SingleOrDefault( x => x.Id == id );
            if ( entityToDelete == null )
            {
                return false;
            }

            return _dbContext.Delete( entityToDelete ) != null;
        }
    }
}