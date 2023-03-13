using Microsoft.EntityFrameworkCore;
using ProjectOverdrive.API.Data;
using ProjectOverdrive.API.Models;
using ProjectOverdrive.API.Repository.Interfaces;

namespace ProjectOverdrive.API.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApiDbContext _dbContext;
        public CompanyRepository(ApiDbContext apiDbContext)
        {
            _dbContext = apiDbContext;
        }

        public async Task<List<Company>> SearchCompany()
        {
            return await _dbContext.Company.ToListAsync();
        }

        public async Task<Company> SearchCompanyById(int id)
        {
            return await _dbContext.Company.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Company> SearchCompanyByName(string name)
        {
            return await _dbContext.Company.FirstOrDefaultAsync(p => p.CompanyName == name);
        }

        public async Task<Company> AddCompany(Company company)
        {
            await _dbContext.Company.AddAsync(company);
            await _dbContext.SaveChangesAsync();

            return company;
        }

        public async Task<Company> UpdateCompany(Company company, int id)
        {
            Company companyById = await SearchCompanyById(id);

            if (companyById == null)
            {
                throw new Exception($"Empresa do ID: {id} não foi encontrada " +
                    $"no banco de dados.");
            }

            companyById.Cnpj = company.Cnpj;
            companyById.Status = company.Status;
            companyById.StartDate = company.StartDate;
            companyById.CompanyName = company.CompanyName;
            companyById.FantasyName = company.FantasyName;
            companyById.Cnae = company.Cnae;
            companyById.LegalNature = company.LegalNature;
            companyById.Address = company.Address;
            companyById.Finance = company.Finance;

            _dbContext.Company.Update(companyById);
            await _dbContext.SaveChangesAsync();

            return companyById;
        }

        public async Task<bool> DeleteCompany(int id)
        {
            Company companyById = await SearchCompanyById(id);

            if (companyById == null)
            {
                throw new Exception($"Empresa do ID: {id} não foi encontrada " +
                    $"no banco de dados.");
            }

            _dbContext.Company.Remove(companyById);
            await _dbContext.SaveChangesAsync();

            return true;

        }
    }
}
