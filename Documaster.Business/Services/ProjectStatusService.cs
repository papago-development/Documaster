using System.Collections.Generic;
using System.Linq;
using Documaster.Model.Entities;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Data.Entity.Infrastructure.Interception;

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
             try
            {
                var deleteProjectStatus = _projectStatusRepository.Delete(projectStatus.Id);

                _unitOfWork.SaveChanges();

                return deleteProjectStatus;
            }
            catch(DbUpdateException)
            {
                throw new Exception("In uz");    
            }
        }

        public bool EditProjectStatus(ProjectStatus projectStatus)
        {
            var editProjectStatus = _projectStatusRepository.Update(projectStatus, new List<string> { "Name", "Color" });
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
