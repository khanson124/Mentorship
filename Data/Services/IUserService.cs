using Mentorship.Models;
using System.Threading.Tasks;

namespace Mentorship.Data.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
       // Task <User> GetUserById(int id);
        public User GetUserById(int id);
        Task AddAsync(User user);

        Task<User> UpdateAsync(int id, User newUser);



        void Delete(int id);
    }
}
