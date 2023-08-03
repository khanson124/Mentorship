using Mentorship.Data.Enum;
using Mentorship.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.IO;

namespace Mentorship.Data.Services
{
    public class UserServices : IUserService
    {
        private readonly MentorshipDbContext _db;

        public UserServices(MentorshipDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(User user)
        {

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
            {
                throw new ArgumentException("User with given id does not exist.");
            }
            user.Status = UserStatus.Deleted; 
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw;
            }
        }





        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        /*        public async Task<User> GetUserByIdAsync(int id)
                {
                    var results = await _db.Users.FirstOrDefaultAsync(n => n.UserId == id);
                    return results;
                }*/

        public User GetUserById(int id)
        {
            var result = _db.Users.FirstOrDefault(n => n.UserId == id);
            return result;

        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        /*    public async Task<User> GetUserById(int id)
   {
       var result = await _db.Users.FirstOrDefault(n => n.UserId == id);
       return result;
   }*/


        public async Task<User> UpdateAsync(int id, User newUser)
        {
            _db.Update(newUser);
            await _db.SaveChangesAsync();
            return newUser;
        }
    }
}
