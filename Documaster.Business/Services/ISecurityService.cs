using Documaster.Business.Models;

namespace Documaster.Business.Services
{
    public interface ISecurityService : IService
    {
        bool ValidateUserAndPassword(LoginModel loginModel);
        bool CreateUserAndPassword(LoginModel loginModel);
        bool UserExists();
    }
}
