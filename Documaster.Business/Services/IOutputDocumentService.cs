using Documaster.Business.Models;
using Documaster.Model.Entities;
using System.Collections.Generic;
using System.Web;

namespace Documaster.Business.Services
{
    public interface IOutputDocumentService
    {
        OutputDocument GetOutputDocuments(int projectId, int requirementId);
        //OutputDocument CreateOutputDocument(HttpPostedFileBase fileUpload, int projectId, int? requirementId, string documentType);
        OutputDocument CreateOutputDocument(HttpPostedFileBase fileUpload, int projectId, int? requirementId, int customizeTabId, string documentType);
        OutputDocument GetOutputDocumentById(int documentId);
        IEnumerable<OutputDocument> GetOutputDocumentByProjectId(int projectId);
        //List<FileToUpdate> GetOutputDocumentByIdAndDocType(int projectId, string documentType);
        List<FileToUpdate> GetOutputDocumentByIdAndDocType(int projectId, int customizeTabId, string documentType);
        bool DeleteOutputDocument(int id);
             
    }
}
