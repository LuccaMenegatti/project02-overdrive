
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<SearchCompanyResponse>> SearchCompany();
        Task<SearchCompanyResponse> SearchCompanyByCnpj(string cnpj);
        Task<List<SearchCompanyResponse>> SearchCompanyByName(string name);
        Task<CompanyOffAddressResponse> SearchPeopleInCompany(int id);
        Task<SearchCompanyResponse> AddCompany(CompanyRequest vo);
        Task<SearchCompanyResponse> UpdateCompany(CompanyUpdateRequest vo);
        Task<CompanyOffAddressAndPeopleResponse> ActiveCompany(int id);
        Task<CompanyOffAddressAndPeopleResponse> InactiveCompany(int id);
        Task<bool> DeleteCompany(int id);
    }
}
