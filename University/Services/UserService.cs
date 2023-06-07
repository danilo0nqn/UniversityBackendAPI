using Microsoft.EntityFrameworkCore;
using University.DataAccess;
using University.Models.DataModels;

namespace University.Services
{
    public class UserService : IUserService
    {
        private readonly UniversityDBContext _context;

        public UserService(UniversityDBContext context)
        {
            _context = context;
        }

        public async Task<User> GetUsersByEmail(string email)
        {
            var userWithDeterminedEmail = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            if (userWithDeterminedEmail == null)
            {
                throw new Exception("Cant find user with " + email + " email");
            }

            return userWithDeterminedEmail;
        }

        public async Task<bool> UserExistWithThisEmail(string email)
        {
            var userExist = await _context.Users.AnyAsync(e => e.Email == email);

            return userExist;
        }
    }
}
