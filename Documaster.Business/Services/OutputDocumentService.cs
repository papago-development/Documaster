using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Documaster.Business.Models;
using Documaster.Model.Entities;
using Documaster.Model.Enums;

namespace Documaster.Business.Services
{
    public class OutputDocumentService : IOutputDocumentService
    {
        private readonly IGenericRepository<OutputDocument> _outputDocumentRepository;
        private readonly IGenericRepository<CustomizeTab> _customizeTabRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OutputDocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _outputDocumentRepository = _unitOfWork.Repository<OutputDocument>();
            _customizeTabRepository = _unitOfWork.Repository<CustomizeTab>();

        }

        public OutputDocument CreateOutputDocument(HttpPostedFileBase fileUpload, int projectId, int? requirementId, int customizeTabId)
        {
            if (fileUpload == null)
            {
                return null;
            }

            var documentType = _customizeTabRepository.Get(customizeTabId).Type;
            //!Enum.TryParse<DocumentType>(documentType, true, out var parsedDocumentType)

            var length = fileUpload.ContentLength;
            var tempImage = new byte[length];
            fileUpload.InputStream.Read(tempImage, 0, length);

            var output = new OutputDocument
            {
                Name = fileUpload.FileName,
                DocumentData = tempImage,
                ContentType = fileUpload.ContentType,
                DocumentType = documentType,
                ProjectId = projectId,
                RequirementId = requirementId,
                CustomizeTabId = customizeTabId
            };

            var documentCreated = _outputDocumentRepository.Create(output);

            try {

                _unitOfWork.SaveChanges();
            }
            catch(Exception e)
            {
                Console.Write(e);
            }
            return documentCreated;
        }

        public OutputDocument GetOutputDocuments(int projectId, int requirementId, int customizeTabId)
        {
            return _outputDocumentRepository
                    .Get(x => x.ProjectId == projectId &&
                         x.RequirementId == requirementId && x.CustomizeTabId == customizeTabId)
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

        public List<FileToUpdate> GetOutputDocumentByIdAndDocType(int projectId, int customizeTabId, string documentType)
        {
            return _outputDocumentRepository
                            .Get(x => x.ProjectId == projectId && x.DocumentType == documentType && x.CustomizeTabId == customizeTabId)
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
