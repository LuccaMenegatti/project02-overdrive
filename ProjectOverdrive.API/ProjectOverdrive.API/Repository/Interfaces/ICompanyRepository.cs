
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<CompanyResponse>> SearchCompany();
        Task<CompanyResponse> SearchCompanyByCnpj(string cnpj);
        Task<CompanyResponse> SearchCompanyByName(string name);
        Task<CompanyResponse> SearchPeopleInCompany(int id);
        Task<CompanyRequest> AddCompany(CompanyRequest vo);
        Task<CompanyUpdateRequest> UpdateCompany(CompanyUpdateRequest vo);
        Task<bool> DeleteCompany(int id);
    }
}
