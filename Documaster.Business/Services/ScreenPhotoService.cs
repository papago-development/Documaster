using System;
using System.Web;
using Documaster.Model.Entities;
using Documaster.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using Documaster.Business.Models;

namespace Documaster.Business.Services
{
    public class ScreenPhotoService : IScreenPhotoService
    {
        private readonly IGenericRepository<ScreenPhoto> _screenPhotoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScreenPhotoService(IGenericRepository<ScreenPhoto> screenPhotoRepository,
                           IUnitOfWork unitOfWork)
        {
            _screenPhotoRepository = screenPhotoRepository;
            _unitOfWork = unitOfWork;
        }

        public ScreenPhoto CreateScreenPhoto(HttpPostedFileBase fileUpload, string documentType)
        {
            if(fileUpload == null || !Enum.TryParse<DocumentType>(documentType, true, out var parsedDocumentType))
            {
                return null;
            }

            var length = fileUpload.ContentLength;
            var tempImage = new byte[length];
            fileUpload.InputStream.Read(tempImage, 0, length);

            var image = new ScreenPhoto
            {
                Name = fileUpload.FileName,
                DocumentData = tempImage,
                ContentType = fileUpload.ContentType,
                DocumentType = parsedDocumentType.ToString()
            };

            var imageCreated = _screenPhotoRepository.Create(image);
            _unitOfWork.SaveChanges();
            return imageCreated;
        }

        public bool DeleteScreenPhoto(int id)
        {
            var deleteScreenPhoto = _screenPhotoRepository.Delete(id);
            _unitOfWork.SaveChanges();
            return deleteScreenPhoto;
        }

        public List<FileToUpdate> GetScreenPhotoByDocType(string documentType)
        {
            return _screenPhotoRepository
                       .Get(x => x.DocumentType == documentType)
                       .Select(item => new FileToUpdate
                       {
                           Id = item?.Id ?? 0,
                           FileName = item?.Name
                       })
                       .ToList();
        }

        public ScreenPhoto GetScreenPhotoById(int id)
        {
            return _screenPhotoRepository.Get(id);
        }

        public IEnumerable<ScreenPhoto> GetScreenPhotos()
        {
            return _screenPhotoRepository.GetAll();
        }

        public bool UpdateScreenPhoto(ScreenPhoto screenPhoto)
        {
            var updateScreenPhoto = _screenPhotoRepository
              .Update(screenPhoto, new List<string> { "IsSelected" });
            _unitOfWork.SaveChanges();

            return updateScreenPhoto;
        }
    }
}
