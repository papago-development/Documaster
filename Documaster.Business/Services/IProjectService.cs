using Documaster.Model.Entities;
using System.Linq;

namespace Documaster.Business.Services
{
    public interface IProjectService
    {
        IQueryable<Project> GetAll();
        Project Create(Project project);
        Project Get(int id);
        bool Delete(Project project);
        bool Update(Project project);
        bool UpdateNotes(Project project);
        bool DoesNameNumberCombinationExist(Project project);
    }
}
