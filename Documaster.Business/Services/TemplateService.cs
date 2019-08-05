using System;
using System.Collections.Generic;
using Documaster.Model.Entities;
using System.Linq;
namespace Documaster.Business.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IGenericRepository<Template> _templateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TemplateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _templateRepository = _unitOfWork.Repository<Template>();
        }

        public IEnumerable<Template> GetTemplates()
        {
            return _templateRepository.GetAll();
        }

        public Template CreateTemplate(Template template)
        {
            var newTemplate = _templateRepository.Create(template);
            _unitOfWork.SaveChanges();
            return newTemplate;
        }

        public bool DeleteTemplate(Template template)
        {
            var deleteTemplate = _templateRepository.Delete(template.Id);
            _unitOfWork.SaveChanges();
            return deleteTemplate;
        }

        public bool UpdateTemplate(Template template)
        {
            var updateTemplate = _templateRepository.Update(template, new List<string> { "Name", "Text" });
            _unitOfWork.SaveChanges();
            return updateTemplate;
        }

        public Template GetTemplateById(int id)
        {
            return _templateRepository.Get(id);
        }

        public bool DoesNameExist(Template template)
        {
            var templates = _templateRepository.Get(x => x.Name == template.Name && x.Id != template.Id);
            return templates.Any();
        }
    }
}