using Documaster.Model.Entities;
using System.Collections.Generic;

namespace Documaster.Business.Services
{
    public interface IProjectTemplateService
    {
        ProjectTemplate Create(int projectId, int templateId, string name);
        bool Update(ProjectTemplate projectTemplate);
        bool Delete(int id);
        bool DoesNameExist(ProjectTemplate projectTemplate);

        ProjectTemplate Get(int id);

        IEnumerable<ProjectTemplate> GetProjectTemplates(int projectId);

    }
}
