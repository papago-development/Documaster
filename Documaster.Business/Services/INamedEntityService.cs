using System;
using Documaster.Model.BaseEntities;
namespace Documaster.Business.Services
{
    public interface INamedEntityService<TEntity>
        where TEntity: NamedEntity
    {
        bool DoesNameExist(string name);
    }
}
