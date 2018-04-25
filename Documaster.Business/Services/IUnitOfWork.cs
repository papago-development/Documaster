using Documaster.Model.BaseEntities;

namespace Documaster.Business.Services
{
    public interface IUnitOfWork : IService
    {
        bool SaveChanges();

        IGenericRepository<TEntity> Repository<TEntity>()
                where TEntity : BaseEntity;
    }
}