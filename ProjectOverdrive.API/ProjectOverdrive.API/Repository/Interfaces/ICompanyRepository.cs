
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<SearchCompanyResponse>> SearchCompany();
        Task<SearchCompanyResponse> SearchCompanyByCnpj(string cnpj);
        Task<SearchCompanyResponse> SearchCompanyByName(string name);
        Task<CompanyResponse> SearchPeopleInCompany(int id);
        Task<CompanyRequest> AddCompany(CompanyRequest vo);
        Task<CompanyUpdateRequest> UpdateCompany(CompanyUpdateRequest vo);
        Task<CompanyUpdateRequest> InactiveCompany(int id);
        Task<bool> DeleteCompany(int id);
    }
}
