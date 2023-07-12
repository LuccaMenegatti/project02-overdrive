using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectOverdrive.API.Data;
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;
using ProjectOverdrive.API.Repository.Interfaces;
using System.Collections.Generic;
using System.Net;

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

        public async Task<List<SearchCompanyResponse>> SearchCompany()
        {
            List<Company> company = await _dbContext.Company
                .Include(a => a.Address)
                .Include(p => p.Peoples)
                .ToListAsync();

             return _mapper.Map<List<SearchCompanyResponse>>(company); 
        }

        public async Task<SearchCompanyResponse> SearchCompanyByCnpj(string cnpj)
        {
            Company company = await _dbContext.Company
                .Where(c => c.Cnpj == cnpj)
                .Include(a => a.Address)
                .FirstOrDefaultAsync();

            return _mapper.Map<SearchCompanyResponse>(company);
        }

        public async Task<List<SearchCompanyResponse>> SearchCompanyByName(string name)
        {
            List<Company> company = await _dbContext.Company
                .Where(c => c.CompanyName.Contains(name))
                .Include(a => a.Address)
                .ToListAsync();

            return _mapper.Map<List<SearchCompanyResponse>>(company);
        }

        public async Task<CompanyOffAddressResponse> SearchPeopleInCompany(int id)
        {
            List<People> peoples = await _dbContext.People
                .Where(p => p.Company.Id == id)
                .ToListAsync();

            var company = await _dbContext.Company
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            company.Peoples = peoples;

            return _mapper.Map<CompanyOffAddressResponse>(company);
        }

        public async Task<SearchCompanyResponse> AddCompany(CompanyRequest vo)
        {
            Company company = _mapper.Map<Company>(vo);
            var checkCnpj = await _dbContext.Company
                .Where(p => p.Cnpj == company.Cnpj)
                .FirstOrDefaultAsync();


            if (vo.FantasyName is null ||
                vo.LegalNature is null ||
                vo.FantasyName.Trim() == "" ||
                vo.LegalNature.Trim() == "" ||
                vo.Address is null ||
                vo.Address.Cep is null ||
                vo.Address.City is null ||
                vo.Address.Contact is null ||
                vo.Address.District is null ||
                vo.Address.Number == 0 ||
                vo.Address.Street is null)
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
                return _mapper.Map<SearchCompanyResponse>(company); 
            }
            else
            {
                company = null;
                return _mapper.Map<SearchCompanyResponse>(company);
            }   
        }

        public async Task<SearchCompanyResponse> UpdateCompany(CompanyUpdateRequest vo)
        {
            Company company = _mapper.Map<Company>(vo);
            Company dbCompany = await _dbContext.Company
                .AsNoTracking()
                .Where(c => c.Id == company.Id)
                .FirstOrDefaultAsync();

            var checkCompany = await _dbContext.Company
                .Where(p => p.Id == company.Id)
                .FirstOrDefaultAsync();

            if (checkCompany == null)
            {
                throw new BadHttpRequestException("Empresa invalida!");
            }

            company.Cnpj = dbCompany.Cnpj;
            company.StartDate = dbCompany.StartDate;

            var status =
                company.Cnpj != null &&
                company.StartDate != null &&
                company.CompanyName != null &&
                company.FantasyName != null &&
                company.Cnae != null &&
                company.LegalNature != null &&
                company.Finance != 0 &&
                company.Address != null &&
                company.Address.Cep != null &&
                company.Address.City != null &&
                company.Address.Contact != null &&
                company.Address.District != null &&
                company.Address.Number != 0 &&
                company.Address.Street != null;

            if (status) company.Status = Enum.Status.Active;
            else company.Status = Enum.Status.Pending;

                _dbContext.Company.Update(company);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<SearchCompanyResponse>(company);
        }

        public async Task<string> ChangeCompanyStatus(int id)
        {
            Company company = await _dbContext.Company
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            List<People> people = await _dbContext.People
                .Where(p => p.IdCompany == id)
                .ToListAsync();

            if (company.Status != Enum.Status.Pending)
            {
                var status = company.Status == Enum.Status.Active;
                if (status)
                {
                    company.Status = Enum.Status.Inactive;
                    foreach (var person in people)
                    {
                        if (person != null)
                        {
                            person.IdCompany = null;
                            person.Company = null;
                        }
                    }
                }
                else
                {
                    company.Status = Enum.Status.Active;
                }
                _dbContext.Company.Update(company);
                _dbContext.SaveChanges();
                return _mapper.Map<string>(company.Status);
            }
            else
            {
                return _mapper.Map<string>(company.Status);
            }
        }

        public async Task<bool> DeleteCompany(int id)
        {  
                Company company = await _dbContext.Company.Where(c => c.Id == id)
                   .FirstOrDefaultAsync() ?? new Company();
                Address address = await _dbContext.Address
                    .Where(a => a.Id == company.IdAddress)
                    .FirstOrDefaultAsync();
                if (company == null) return false;

                var checkPeople = await _dbContext.People
                    .Where(p => p.IdCompany == id)
                    .FirstOrDefaultAsync();

                if (checkPeople == null)
                {
                    _dbContext.Company.Remove(company);
                    _dbContext.Address.Remove(address);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new BadHttpRequestException("Impossivel excluir, existem pessoas nessa empresa.");
                }
        }   
    }
}
