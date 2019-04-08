using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using Documaster.Business.Models;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserProfile> _userProfileRepository;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userProfileRepository = unitOfWork.Repository<UserProfile>();
        }

        public bool ValidateUserAndPassword(LoginModel loginModel)
        {
            var userProfile = _userProfileRepository.Get(x => x.UserName == loginModel.UserName).FirstOrDefault();
            if (userProfile == null)
            {
                return false;
            }
            var isSamePassword = Crypto.VerifyHashedPassword(userProfile.Password, loginModel.Password);
            return isSamePassword;
        }

        public bool CreateUserAndPassword(LoginModel loginModel)
        {
            var userProfile = new UserProfile
            {
                UserName = loginModel.UserName,
                Password = Crypto.HashPassword(loginModel.Password)
            };
            var createdUser = _userProfileRepository.Create(userProfile);
            _unitOfWork.SaveChanges();
            return createdUser != null;
        }

        public bool UserExists()
        {
            return _userProfileRepository.GetAll().Any();
        }
    }
}
