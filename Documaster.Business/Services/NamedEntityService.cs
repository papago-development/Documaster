using System;
using Documaster.Model.BaseEntities;
using Documaster.Data.DataAccess;
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

        public bool DoesNameExist(string name)
        {
            var entities = _genericRepository.Get(x => x.Name == name);
            return entities.Any();
        }
    }
}
