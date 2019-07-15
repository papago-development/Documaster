using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public interface IReplacePlaceholderService
    {
        ProjectTemplate Replace(int templateId, int projectId);
    }
}
