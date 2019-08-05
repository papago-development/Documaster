using System.Collections.Generic;
using System.Linq;
using Documaster.Model.Entities;
using System.Data.Entity.Infrastructure;

namespace Documaster.Business.Services
{
    public class ProjectStatusService : IProjectStatusService
    {
        private readonly IGenericRepository<ProjectStatus> _projectStatusRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectStatusRepository = _unitOfWork.Repository<ProjectStatus>();
        }

        public ProjectStatus CreateProjectStatus(ProjectStatus projectStatus)
        {
            var createProjectStatus = _projectStatusRepository.Create(projectStatus);
            _unitOfWork.SaveChanges();
            return createProjectStatus;
        }

        public bool DeleteProjectStatus(ProjectStatus projectStatus)
        {
            try
            {
                var deleteProjectStatus = _projectStatusRepository.Delete(projectStatus.Id);
                _unitOfWork.SaveChanges();
                return deleteProjectStatus;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public bool EditProjectStatus(ProjectStatus projectStatus)
        {
            var editProjectStatus = _projectStatusRepository.Update(projectStatus, new List<string> { "Name", "Color" });
            _unitOfWork.SaveChanges();
            return editProjectStatus;
        }

        public IEnumerable<ProjectStatus> GetAll()
        {
            return _projectStatusRepository.GetAll().ToList();
        }

        public ProjectStatus GetProjectStatusById(int id)
        {
            return _projectStatusRepository.Get(id);
        }
    }
}
