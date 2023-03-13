using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<List<Company>> SearchCompany();
        Task<Company> SearchCompanyById(int id);
        Task<Company> SearchCompanyByName(string name);
        Task<Company> AddCompany(Company company);
        Task<Company> UpdateCompany(Company company, int id);
        Task<bool> DeleteCompany(int id);
    }
}
