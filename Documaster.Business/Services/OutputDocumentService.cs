using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Documaster.Business.Models;
using Documaster.Model.Entities;
using Documaster.Model.Enums;

namespace Documaster.Business.Services
{
    public class OutputDocumentService : IOutputDocumentService
    {
        private readonly IGenericRepository<OutputDocument> _outputDocumentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OutputDocumentService(IGenericRepository<OutputDocument> outputDocumentRepository,
                                     IUnitOfWork unitOfWork)
        {
            _outputDocumentRepository = outputDocumentRepository;
            _unitOfWork = unitOfWork;
        }

        public OutputDocument CreateOutputDocument(HttpPostedFileBase fileUpload, int projectId, int? requirementId, string documentType)
        {
            if(fileUpload == null || !Enum.TryParse<DocumentType>(documentType, true, out var parsedDocumentType))
            {
                return null;
            }

            var length = fileUpload.ContentLength;
            var tempImage = new byte[length];
            fileUpload.InputStream.Read(tempImage, 0, length);

            var output = new OutputDocument
            {
                Name = fileUpload.FileName,
                DocumentData = tempImage,
                ContentType = fileUpload.ContentType,
                DocumentType = parsedDocumentType.ToString(),
                ProjectId = projectId,
                RequirementId = requirementId,
            };

            var documentCreated = _outputDocumentRepository.Create(output);
            _unitOfWork.SaveChanges();
            return documentCreated;
        }
        
        public OutputDocument GetOutputDocuments(int projectId, int requirementId)
        {
            return _outputDocumentRepository
                    .Get(x => x.ProjectId == projectId &&
                         x.RequirementId == requirementId)
                    .FirstOrDefault();
        }

        public IEnumerable<OutputDocument> GetOutputDocumentByProjectId(int projectId)
        {
            return _outputDocumentRepository.GetAll().Where(x => x.ProjectId == projectId).ToList();
        }

        public bool DeleteOutputDocument(int id)
        {
            var deleteDocument = _outputDocumentRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return deleteDocument;
        }

        public OutputDocument GetOutputDocumentById(int documentId)
        {
           return _outputDocumentRepository.Get(documentId);
        }

        public List<FileToUpdate> GetOutputDocumentByIdAndDocType(int projectId, string documentType)
        {
            return _outputDocumentRepository
                            .Get(x => x.ProjectId == projectId && x.DocumentType == documentType)
                            .Select(item => new FileToUpdate
                            {
                                Id = item?.Id ?? 0,
                                FileName = item?.Name,
                                ProjectId = projectId
                            })
                            .ToList();
        }
    }
}
