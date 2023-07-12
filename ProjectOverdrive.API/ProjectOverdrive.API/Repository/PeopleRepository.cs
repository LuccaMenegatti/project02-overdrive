using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectOverdrive.API.Data;
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Enum;
using ProjectOverdrive.API.Models;
using ProjectOverdrive.API.Repository.Interfaces;
using System.Net;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProjectOverdrive.API.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly ApiDbContext _dbContext;
        private IMapper _mapper;
        public PeopleRepository(ApiDbContext apiDbContext, IMapper mapper) 
        {
            _dbContext = apiDbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PeopleResponse>> SearchPeople()
        {
            List<People> people = await _dbContext.People
               .Include(c => c.Company)
                    .ThenInclude(a => a.Address)
               .ToListAsync();

            return _mapper.Map<List<PeopleResponse>>(people);
        }

        public async Task<List<PeopleResponse>> SearchPeopleByName(string name)
        {
            List<People> people = await _dbContext.People
                            .Where(p => p.Name.Contains(name))
                            .Include(c => c.Company)
                                .ThenInclude(a => a.Address)
                            .ToListAsync();

            return _mapper.Map<List<PeopleResponse>>(people);
        }

        public async Task<PeopleResponse> SearchPeopleByCpf(string cpf)
        {
            People people = await _dbContext.People
                            .Where(p => p.Cpf == cpf)
                            .Include(c => c.Company)
                            .FirstOrDefaultAsync();

            return _mapper.Map<PeopleResponse>(people);
        }

        public async Task<PeopleResponse> AddPeopleInCompany(int idPeople, int idCompany)
        {
            People people = await _dbContext.People
                .Where(p => p.Id == idPeople)
                .FirstOrDefaultAsync();
            Company company = await _dbContext.Company
                .Where(c => c.Id == idCompany)
                .FirstOrDefaultAsync();

            var checkPessoa = await _dbContext.People
                .Where(p => p.Id == idPeople)
                .FirstOrDefaultAsync();

            var checkCompany = await _dbContext.Company
                .Where(c => c.Id == idCompany)
                .FirstOrDefaultAsync();

            if (checkPessoa == null || checkCompany == null)
            {
                throw new BadHttpRequestException("Empresa ou Pessoa invalida!");
            }

            if (people.Status == Enum.Status.Active && company.Status == Enum.Status.Active)
            {
                people.Company = company;
                _dbContext.People.Update(people);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<PeopleResponse>(people);
            }
            else
            {
                throw new BadHttpRequestException("Para cadastrar, a pessoa e a empresa devem estar ativas!");
            }
          
        }

        public async Task<PeopleResponse> RemovePeopleInCompany(int idPeople)
        {
            People people = await _dbContext.People
                .Where(p => p.Id == idPeople)
                .FirstOrDefaultAsync();

            var checkPessoa = await _dbContext.People
                .Where(p => p.Id == idPeople)
                .FirstOrDefaultAsync();

            if (checkPessoa == null)
            {
                throw new BadHttpRequestException("Pessoa invalida!");
            } else
            {
                people.IdCompany = null;
                people.Company = null;

                _dbContext.People.Update(people);
                await _dbContext.SaveChangesAsync();

                return _mapper.Map<PeopleResponse>(people);
            }
        }

        public async Task<PeopleResponse> AddPeople(PeopleRequest vo)
        {
            People people = _mapper.Map<People>(vo);
            var checkCpf = await _dbContext.People
                .Where(p => p.Cpf == people.Cpf)
                .FirstOrDefaultAsync();
            var checkContact = await _dbContext.People
                .Where(p => p.NumberContact == people.NumberContact)
                .FirstOrDefaultAsync();

            if (vo.UserName is null ||  vo.NumberContact is null ||
                vo.UserName.Trim() ==  ""  || vo.NumberContact.Trim() == "")
            {
                people.Status = Enum.Status.Pending;
            }
            else
            {
                people.Status = Enum.Status.Active;
            }

            if(checkCpf == null && checkContact == null)
            {
                await _dbContext.People.AddAsync(people);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<PeopleResponse>(people);
            }
            else
            {
                people = null;
                return _mapper.Map<PeopleResponse>(people);
            }
           
        }

        public async Task<PeopleResponse> UpdatePeople(PeopleUpdateRequest vo)
        {
            People people = _mapper.Map<People>(vo);
            People dbpeople = await _dbContext.People
               .AsNoTracking()
               .Where(c => c.Id == people.Id)
               .FirstOrDefaultAsync();

            var checkPeople = await _dbContext.People
             .Where(p => p.Id == people.Id)
             .FirstOrDefaultAsync();

            if (checkPeople == null)
            {
                throw new BadHttpRequestException("Pessoa invalida!");
            }

            people.Cpf = dbpeople.Cpf;
            people.IdCompany = dbpeople.IdCompany;
            people.Company = dbpeople.Company;

            var status =
                people.Cpf != null &&
                people.Name != null &&
                people.NumberContact != null &&
                people.UserName != null;

            if (status) people.Status = Enum.Status.Active;
            else people.Status = Enum.Status.Pending;

            _dbContext.People.Update(people);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<PeopleResponse>(people);
        }

        public async Task<string> ChangePeopleStatus(int id)
        {
            People people = await _dbContext.People
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (people.Status != Status.Pending)
            {
                var status = people.Status == Status.Active;
                if (status)
                {
                    people.Status = Status.Inactive;
                    people.IdCompany = null;
                    people.Company = null;
                }
                else
                {
                    people.Status = Status.Active;
                }
                _dbContext.People.Update(people);
                _dbContext.SaveChanges();
                return _mapper.Map<string>(people.Status);
            }
            else
            {
                return _mapper.Map<string>(people.Status);
            }
        }

        public async Task<bool> DeletePeople(int id)
        {
            
                People people = await _dbContext.People.Where(p => p.Id == id)
                .FirstOrDefaultAsync() ?? new People();
                if (people == null) return false;

                if (people.IdCompany == null)
                    {
                        _dbContext.People.Remove(people);
                        await _dbContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        throw new BadHttpRequestException("Impossivel excluir, essa pessoa pertence a uma empresa.");
                    }
                }   
    }
}
