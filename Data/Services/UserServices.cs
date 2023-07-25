using Mentorship.Models;
using System.IO;

namespace Mentorship.Data.Services
{
    public class UserServices : IUserService
    {
        private readonly MentorShipDbContext _db;

        public UserServices(MentorShipDbContext db)
        {
            _db = db;
        }
        public void Add(User user)
        {
            
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Mentor GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Mentor Update(int id, User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
