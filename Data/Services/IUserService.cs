using Mentorship.Models;

namespace Mentorship.Data.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Mentor GetUserById(int id);
        void Add(User user);

        Mentor Update(int id, User newUser);



        void Delete(int id);
    }
}
