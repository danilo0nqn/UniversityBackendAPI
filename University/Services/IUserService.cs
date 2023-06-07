using University.Models.DataModels;

namespace University.Services
{
    public interface IUserService
    {

        Task<User> GetUsersByEmail(string email);
        Task<bool> UserExistWithThisEmail(string email);

    }
}
