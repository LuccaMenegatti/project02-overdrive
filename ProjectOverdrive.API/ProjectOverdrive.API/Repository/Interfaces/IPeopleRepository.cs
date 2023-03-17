using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Repository.Interfaces
{
    public interface IPeopleRepository
    {
        Task<List<PeopleResponse>> SearchPeople();
        Task<PeopleResponse> SearchPeopleByName(string name);
        Task<PeopleRequest> AddPeopleInCompany(int idPeople, int idCompany);
        Task<PeopleRequest> AddPeople(PeopleRequest vo);
        Task<PeopleUpdateRequest> UpdatePeople(PeopleUpdateRequest vo);
        Task<bool> DeletePeople(int id);
    }
}
