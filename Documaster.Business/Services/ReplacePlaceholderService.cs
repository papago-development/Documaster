using System;
using System.Collections.Generic;
using Documaster.Model.Entities;
using Newtonsoft.Json;

namespace Documaster.Business.Services
{
    public class ReplacePlaceholderService : IReplacePlaceholderService
    {
        private readonly IUnitOfWork _unitOfWork;
 
        private readonly IGenericRepository<Template> _templateRepository;
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IGenericRepository<Customer> _customerRepository;

        public ReplacePlaceholderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = _unitOfWork.Repository<Project>();
            _customerRepository = _unitOfWork.Repository<Customer>();
            _templateRepository = unitOfWork.Repository<Template>();
        }
        
        public ProjectTemplate Replace(int projectId, int templateId, string name)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "Config/Placeholders.json";
            var configFile = System.IO.File.ReadAllText(path);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(configFile, new JsonSerializerSettings());

            var template = _templateRepository.Get(templateId);
            var projectTemplate = new ProjectTemplate
            {
                ProjectId = projectId,
                Text = template.Text,
                Name = name
            };
           
            foreach (var item in dictionary)
            {
                if (template.Text.Contains(item.Key))
                {
                    var table = item.Value.Split('.')[0];
                    var property = item.Value.Split('.')[1];

                    var valueToReplace = GetValue(table, property, projectId);
                  projectTemplate.Text = projectTemplate.Text.Replace(item.Key, valueToReplace);
                }
            }
           // projectTemplate.Text = template.Text;
            return projectTemplate;
        }

        private string GetValue(string table, string property, int projectId)
        {
            switch (table)
            {
                case "Project":
                    var project = _projectRepository.Get(projectId);
                    switch (property)
                    {
                        case "Name":
                            return project.Name;
                        case "Number":
                            return project.Number;
                        case "Address":
                            return project.Address;
                        default:
                            return String.Empty ;
                    }
                case "Customer":
                    var customer = _customerRepository.Get(projectId);
                    switch (property)
                    {
                        case "Name":
                            return customer.Name;
                        case "Address":
                            return customer.Address;
                        case "Telephone":
                            return customer.Telephone;
                        default:
                            return String.Empty;
                    }
                default:
                    return String.Empty;
            }
        }
        
    }
}
