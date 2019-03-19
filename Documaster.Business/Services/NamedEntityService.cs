using Documaster.Model.BaseEntities;
using System.Linq;

namespace Documaster.Business.Services
{
    public class NamedEntityService<TEntity> : INamedEntityService<TEntity>
        where TEntity : NamedEntity
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public NamedEntityService(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public bool DoesNameExist(TEntity entity)
        {
            var entities = _genericRepository.Get(x => x.Name == entity.Name && x.Id != entity.Id);
            return entities.Any();
        }
    }
}
