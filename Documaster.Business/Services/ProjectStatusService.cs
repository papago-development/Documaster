using System.Collections.Generic;
using System.Linq;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public class ProjectStatusService : IProjectStatusService
    {
        private readonly IGenericRepository<ProjectStatus> _projectStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectStatusService(IGenericRepository<ProjectStatus> projectStatusRepository,
                                   IUnitOfWork unitOfWork)
        {
            _projectStatusRepository = projectStatusRepository;
            _unitOfWork = unitOfWork;
        }

        public ProjectStatus CreateProjectStatus(ProjectStatus projectStatus)
        {
            var createProjectStatus = _projectStatusRepository.Create(projectStatus);
            _unitOfWork.SaveChanges();
            return createProjectStatus;
        }

        public bool DeleteProjectStatus(ProjectStatus projectStatus)
        {
            var deleteProjectStatus = _projectStatusRepository.Delete(projectStatus.Id);
            _unitOfWork.SaveChanges();
            return deleteProjectStatus;
        }

        public bool EditProjectStatus(ProjectStatus projectStatus)
        {
            var editProjectStatus = _projectStatusRepository.Update(projectStatus, new List<string> { "Name" });
            _unitOfWork.SaveChanges();
            return editProjectStatus;
        }

        public IQueryable<ProjectStatus> GetAll()
        {
            return _projectStatusRepository.GetAll();
        }

        public ProjectStatus GetProjectStatusById(int id)
        {
            return _projectStatusRepository.Get(id);
        }
    }
}
