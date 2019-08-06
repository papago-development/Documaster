using Documaster.Business.Models;
using Documaster.Model.Entities;
using System.Collections.Generic;
using System.Web;

namespace Documaster.Business.Services
{
    public interface IOutputDocumentService
    {
        OutputDocument GetOutputDocuments(int projectId, int requirementId, int customizeTabId);
        OutputDocument CreateOutputDocument(HttpPostedFileBase fileUpload, int projectId, int? requirementId, int customizeTabId);
        OutputDocument GetOutputDocumentById(int documentId);
        IEnumerable<OutputDocument> GetOutputDocumentByProjectId(int projectId);
        List<FileToUpdate> GetOutputDocumentByProjectIdAndTabId(int projectId, int customizeTabId);
        bool DeleteOutputDocument(int id);
             
    }
}
