using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectOverdrive.API.Data;
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;
using ProjectOverdrive.API.Repository.Interfaces;
using System.Collections.Generic;

namespace ProjectOverdrive.API.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApiDbContext _dbContext;
        private IMapper _mapper;
        public CompanyRepository(ApiDbContext apiDbContext, IMapper mapper)
        {
            _dbContext = apiDbContext;
            _mapper = mapper;
        }

        public async Task<List<CompanyResponse>> SearchCompany()
        {
            List<Company> company = await _dbContext.Company
                .Include(a => a.Address)
                .ToListAsync();

             return _mapper.Map<List<CompanyResponse>>(company); 
        }

        public async Task<CompanyResponse> SearchCompanyByCnpj(string cnpj)
        {
            Company company = await _dbContext.Company
                .Where(c => c.Cnpj == cnpj)
                .Include(a => a.Address)
                .FirstOrDefaultAsync();

            return _mapper.Map<CompanyResponse>(company);
        }

        public async Task<CompanyResponse> SearchCompanyByName(string name)
        {
            Company company = await _dbContext.Company
                .Where(c => c.CompanyName == name)
                .Include(a => a.Address)
                .FirstOrDefaultAsync();

            return _mapper.Map<CompanyResponse>(company);
        }

        public async Task<CompanyResponse> SearchPeopleInCompany(int id)
        {
            List<People> peoples = await _dbContext.People
                .Where(p => p.Company.Id == id)
                .ToListAsync();

            var company = await _dbContext.Company
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            company.Peoples = peoples;

            return _mapper.Map<CompanyResponse>(company);
        }

        public async Task<CompanyRequest> AddCompany(CompanyRequest vo)
        {
            Company company = _mapper.Map<Company>(vo);
            var checkCnpj = await _dbContext.Company
                .Where(p => p.Cnpj == company.Cnpj)
                .Where(p => p.Cnpj == company.Cnpj)
                .FirstOrDefaultAsync();


            if (vo.FantasyName is null || vo.LegalNature is null || 
                vo.FantasyName.Trim() == "" || vo.LegalNature.Trim() == "")    
            {
                company.Status = Enum.Status.Pending;
            }
            else
            {
                company.Status = Enum.Status.Active;
            }

            if (checkCnpj == null)
            {
                await _dbContext.Company.AddAsync(company);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<CompanyRequest>(company); ;
            }
            else
            {
                throw new Exception("Esse Cnpj já existe no banco de dados");
            }   
        }

        public async Task<CompanyUpdateRequest> UpdateCompany(CompanyUpdateRequest vo)
        {
            Company company = _mapper.Map<Company>(vo);
            var checkCnpj = await _dbContext.Company
                .Where(p => p.Cnpj == company.Cnpj)
                .Where(p => p.Cnpj == company.Cnpj)
                .FirstOrDefaultAsync();

            if (vo.FantasyName is null || vo.LegalNature is null ||
                vo.FantasyName.Trim() == "" || vo.LegalNature.Trim() == "")
            {
                company.Status = Enum.Status.Pending;
            }
            else
            {
                company.Status = Enum.Status.Active;
            }

            if (checkCnpj == null)
            {
                _dbContext.Company.Update(company);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<CompanyUpdateRequest>(company);
            }
            else
            {
                throw new Exception("Esse Cnpj já existe no banco de dados");
            }  
        }

        public async Task<bool> DeleteCompany(int id)
        {
            try
            {
                Company company = await _dbContext.Company.Where(c => c.Id == id)
                    .FirstOrDefaultAsync() ?? new Company();
                Address address = await _dbContext.Address
                    .Where(a => a.Id == company.IdAddress)
                    .FirstOrDefaultAsync();
                if (company == null) return false;
                _dbContext.Company.Remove(company);
                _dbContext.Address.Remove(address);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }   
    }
}
