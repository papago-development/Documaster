using System.Collections.Generic;
using System.Web;
using Documaster.Model.Entities;
using Documaster.Business.Models;
namespace Documaster.Business.Services
{
    public interface IScreenPhotoService
    {
        ScreenPhoto CreateScreenPhoto(HttpPostedFileBase fileUpload, string documentType);
        ScreenPhoto GetScreenPhotoById(int id);
        IEnumerable<ScreenPhoto> GetScreenPhotos();
        List<FileToUpdate> GetScreenPhotoByDocType(string documentType);
        bool DeleteScreenPhoto(int id);
        bool UpdateScreenPhoto(ScreenPhoto screenPhoto);
    }
}
