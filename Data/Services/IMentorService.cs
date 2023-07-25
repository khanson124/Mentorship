using Mentorship.Models;

namespace Mentorship.Data.Services
{
    public interface IMentorService
    {
        Task<IEnumerable<Mentor>> GetAllMentors();
        Mentor GetMentorById(int id);   
        void Add(Mentor mentor);

        Mentor Update(int id, Mentor newMentor);



        void Delete(int id);    
    }
}
