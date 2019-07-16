using System.Collections.Generic;
using System.Linq;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public class ProjectTemplateService: IProjectTemplateService
    {
        private readonly IGenericRepository<ProjectTemplate> _projectTemplateRepository;
        private readonly IGenericRepository<Template> _templateRepository;
        private readonly IReplacePlaceholderService _replacePlaceholderService;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectTemplateService(IUnitOfWork unitOfWork,
                                      IReplacePlaceholderService replacePlaceholderService)
        {
            _unitOfWork = unitOfWork;
            _templateRepository = _unitOfWork.Repository<Template>();
            _projectTemplateRepository = _unitOfWork.Repository<ProjectTemplate>();
            _replacePlaceholderService = replacePlaceholderService;
        }

        public ProjectTemplate Create(ProjectTemplate projectTemplate)
        {
            var newProjectTemplate = _projectTemplateRepository.Create(projectTemplate);
            _unitOfWork.SaveChanges();
            return newProjectTemplate;
        }

        public ProjectTemplate GetTemplate(int templateId, int projectId)
        {
            var projectTemplate = Get(templateId, projectId);
            if (projectTemplate == null || projectTemplate !=null)
            {
                var replacedContent = _replacePlaceholderService.Replace(templateId, projectId);
                
                return replacedContent;
            }
            return projectTemplate;

        }

        public bool CreateOrUpdate(ProjectTemplate projectTemplate)
        {
            var success = true;
            if(projectTemplate.Id> 0)
            {
                success = _projectTemplateRepository.Update(projectTemplate, new List<string> { "Text" });
            }
            else
            {
                success = _projectTemplateRepository.Create(projectTemplate)!= null;
            }

            _unitOfWork.SaveChanges();
            return success;
        }


        private ProjectTemplate Get(int templateId, int projectId)
        {
            var projectTemplate = _projectTemplateRepository.Get(x => x.TemplateId == templateId && x.ProjectId == projectId).SingleOrDefault();
            return projectTemplate;
        }
    }
}
