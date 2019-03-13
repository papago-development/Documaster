using Documaster.Business.Models;
using Documaster.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Documaster.Business.Services
{
    public interface IOutputDocumentService
    {
        OutputDocument GetOutputDocuments(int projectId, int requirementId);
        OutputDocument CreateOutputDocument(HttpPostedFileBase fileUpload, int projectId, int? requirementId, string documentType);
        OutputDocument GetOutputDocumentById(int documentId);
        IEnumerable<OutputDocument> GetOutputDocumentByProjectId(int projectId);
        List<FileToUpdate> GetOutputDocumentByIdAndDocType(int projectId, string documentType);
        bool DeleteOutputDocument(int id);
             
    }
}
