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

        public TemplateService(IGenericRepository<Template> templateRepository,
                               IUnitOfWork unitOfWork)
        {
            _templateRepository = templateRepository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Template> GetTemplates()
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
            var updateTemplate = _templateRepository.Update(template, new List<string> { "Name" });
            _unitOfWork.SaveChanges();
            return updateTemplate;
        }

        public Template GetTemplateById(int id)
        {
            return _templateRepository.Get(id);
        }
    }
}
