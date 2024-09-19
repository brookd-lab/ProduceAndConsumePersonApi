using PersonApi.Models;

namespace PersonApi.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllPeople();
        Task<Person> GetPerson(int id);
        Task CreatePerson(Person person);
        Task UpdatePerson(Person person);
        Task DeletePerson(Person person);
    }
}
