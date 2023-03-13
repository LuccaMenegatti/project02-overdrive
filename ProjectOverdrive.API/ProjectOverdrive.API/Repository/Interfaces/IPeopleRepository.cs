using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Repository.Interfaces
{
    public interface IPeopleRepository
    {
        Task<List<People>> SearchPeople();
        Task<People> SearchPeopleById(int id);
        Task<People> SearchPeopleByName(string name);
        Task<People> AddPeople(People people);
        Task<People> UpdatePeople(People people, int id);
        Task<bool> DeletePeople(int id);


    }
}
