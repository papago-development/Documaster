using System.Collections.Generic;
using System.Linq;
using Documaster.Model.Entities;
using System;

namespace Documaster.Business.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IGenericRepository<Project> projectRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Project> GetAllProjects()
        {
            return _projectRepository.GetAll();
        }

        public Project CreateProject(Project project)
        {
            project.Created = new DateTime(project.CreatedYear, project.CreatedMonth, project.CreatedDay);

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

        public bool UpdateProject(Project project)
        {
            if(project.ExpireDay != 0 && project.ExpireYear != 0 && project.ExpireMonth != 0)
            {
                project.Expire = new DateTime(project.ExpireYear, project.ExpireMonth, project.ExpireDay);
                _projectRepository.Update(project, new List<string> { "Expire" });
            }

            var updatedProject = _projectRepository.Update(project, new List<string> { "Name", "Number", "ProjectStatusId" });
            _unitOfWork.SaveChanges();
            return updatedProject;
        }

        public bool UpdateProjectNotes(Project project)
        {
            var updatedProjectNotes = _projectRepository.Update(project, new List<string> { "Notes" });
            _unitOfWork.SaveChanges();
            return updatedProjectNotes;
        }

        public bool DoesNameNumberCombinationExist(Project project)
        {
            var projects = _projectRepository.Get(x => x.Name == project.Name && x.Number == project.Number && x.Id != project.Id);
            return projects.Any();
        }
    }
}
