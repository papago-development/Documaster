using System.Collections.Generic;
using System.Linq;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public class ProjectTemplateService : IProjectTemplateService
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

        public ProjectTemplate Get(int id)
        {
            var projectTemplate = _projectTemplateRepository.Get(id);
            return projectTemplate;
        }

            public ProjectTemplate Create(int projectId, int templateId, string name)
        {
            var projectTemplate = _replacePlaceholderService.Replace(projectId, templateId, name);
                var created = _projectTemplateRepository.Create(projectTemplate);
                _unitOfWork.SaveChanges();
                return created;
        }

        public bool Update(ProjectTemplate projectTemplate)
        {
            var updated = _projectTemplateRepository.Update(projectTemplate, new List<string> { "Text" });
            _unitOfWork.SaveChanges();

            return updated;
        }

        //Get list of projectTemplates by specified projectId
        public IEnumerable<ProjectTemplate> GetProjectTemplates(int projectId)
        {
            return _projectTemplateRepository.GetAll().Where(x => x.ProjectId == projectId).OrderByDescending(x => x.CreationDate);
        }

        public bool Delete(ProjectTemplate projectTemplate)
        {
            var deleted = _projectTemplateRepository.Delete(projectTemplate.Id);
            _unitOfWork.SaveChanges();
            return deleted;
        }

        public bool DoesNameExist(ProjectTemplate projectTemplate)
        {
            var projectTemplates = _projectTemplateRepository.Get(x => x.Name == projectTemplate.Name && x.Id != projectTemplate.Id);
            return projectTemplates.Any();
        }
    }
}
