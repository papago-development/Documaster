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

        public IQueryable<Project> GetAllProjects()
        {
            return _projectRepository.GetAll();
        }

        public Project CreateProject(Project project)
        {
            var newProject = _projectRepository.Create(project);
            _unitOfWork.SaveChanges();
            return newProject;
        }

        public bool DeleteProject(Project project)
        {
            var deleteProject = _projectRepository.Delete(project.Id);
            _unitOfWork.SaveChanges();
            return deleteProject;
        }

        public Project GetProjectById(int id)
        {
            return _projectRepository.Get(id);
        }

        public Project GetProjectByProjectId(int id)
        {
            return _projectRepository.Get(x => x.Id == id).FirstOrDefault();
        }
        
        public Project GetProjectByNoteId(int id)
        {
          return  _projectRepository.Get(x => x.Id == id).FirstOrDefault();
        }

        public bool UpdateProject(Project project)
        {
            var updatedProject = _projectRepository.Update(project, new List<string> { "Name", "Expire", "Number", "ProjectStatusId" });
            _unitOfWork.SaveChanges();
            return updatedProject;
        }

    }
}
