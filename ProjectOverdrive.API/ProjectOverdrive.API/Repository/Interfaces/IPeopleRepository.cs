using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Repository.Interfaces
{
    public interface IPeopleRepository
    {
        Task<IEnumerable<PeopleResponse>> SearchPeople();
        Task<PeopleResponse> SearchPeopleByName(string name);
        Task<PeopleResponse> AddPeopleInCompany(int idPeople, int idCompany);
        Task<PeopleResponse> RemovePeopleInCompany(int idPeople);
        Task<PeopleResponse> AddPeople(PeopleRequest vo);
        Task<PeopleResponse> UpdatePeople(PeopleUpdateRequest vo);
        Task<PeopleResponse> ActivePeople(int id);
        Task<PeopleResponse> InactivePeople(int id);
        Task<bool> DeletePeople(int id);
    }
}
