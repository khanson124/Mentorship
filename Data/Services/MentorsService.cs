using Mentorship.Models;
using Microsoft.EntityFrameworkCore;

namespace Mentorship.Data.Services
{
    public class MentorsService : IMentorService
    {
        private readonly MentorShipDbContext _db;

        public MentorsService(MentorShipDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Mentor>> GetAllMentors()
        {
            var result = await _db.Mentors.Include(m => m.User).ToListAsync();
            return result;
        }


        public void Add(Mentor mentor)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Mentor GetMentorById(int id)
        {
            throw new NotImplementedException();
        }

        public Mentor Update(int id, Mentor newMentor)
        {
            throw new NotImplementedException();
        }
    }
}
