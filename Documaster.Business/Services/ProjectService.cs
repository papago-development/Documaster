using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IGenericRepository<Project> proiectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = proiectRepository;
            _unitOfWork = unitOfWork;
        }

        public Project CreateProject(Project project)
        {
            var newProject = _projectRepository.Create(project);
            _unitOfWork.SaveChanges();
            return newProject;
        }

        public Project GetProjectById(int id)
        {
            return _projectRepository.Get(id);
        }
    }
}
