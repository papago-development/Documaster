using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public interface IProjectTemplateService
    {
        bool CreateOrUpdate(ProjectTemplate projectTemplate);

        ProjectTemplate Create(ProjectTemplate projectTemplate);

        ProjectTemplate GetTemplate(int templateId, int projectId);
    }
}
