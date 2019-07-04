using Documaster.Model.Entities;
using System.Collections.Generic;

namespace Documaster.Business.Services
{
    public interface ITemplateService
    {
        IEnumerable<Template> GetTemplates();
        Template CreateTemplate(Template template);
        Template GetTemplateById(int id);

        bool UpdateTemplate(Template template);
        bool DeleteTemplate(Template template);
        bool DoesNameExist(Template template);
    }
}