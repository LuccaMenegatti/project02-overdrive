using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectOverdrive.API.Data;
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
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

        public async Task<PeopleResponse> SearchPeopleByName(string name)
        {
            People people = await _dbContext.People
                            .Where(p => p.Name == name)
                            .Include(c => c.Company)
                                .ThenInclude(a => a.Address)
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

            if(people.Status == Enum.Status.Active && company.Status == Enum.Status.Active)
            {
                people.Company = company;
                _dbContext.People.Update(people);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<PeopleResponse>(people);
            }
            else
            {
                throw new Exception("Para cadastrar, a pessoa e a empresa devem estar ativas!");
            }
          
        }

        public async Task<PeopleResponse> RemovePeopleInCompany(int idPeople)
        {
            People people = await _dbContext.People
                .Where(p => p.Id == idPeople)
                .FirstOrDefaultAsync();

            people.IdCompany = null;
            people.Company = null;

            _dbContext.People.Update(people);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PeopleResponse>(people);
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
                throw new Exception("O Cpf ou Numero de Contato, já existem no banco de dados");
            }
           
        }

        public async Task<PeopleResponse> UpdatePeople(PeopleUpdateRequest vo)
        {
            People people = _mapper.Map<People>(vo);
            People dbpeople = await _dbContext.People
               .AsNoTracking()
               .Where(c => c.Id == people.Id)
               .FirstOrDefaultAsync();

            people.Cpf = dbpeople.Cpf;

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

        public async Task<PeopleResponse> ActivePeople(int id)
        {
            People people = await _dbContext.People.Where(c => c.Id == id)
                  .FirstOrDefaultAsync() ?? new People();

            if (people == null) throw new Exception("Essa pessoa não existe no banco de dados");

            if (people.Status == Enum.Status.Pending) throw new Exception("Para ativar, todos os dados devem ser preenchidos");

            if (people.UserName is null || people.NumberContact is null ||
                people.UserName.Trim() == "" || people.NumberContact.Trim() == "")
            {
                throw new Exception("Para ativar, todos os dados devem ser preenchidos");
            }
            else
            {
                if (people.Status == Enum.Status.Inactive)
                {
                    people.Status = Enum.Status.Active;
                    _dbContext.People.Update(people);
                    await _dbContext.SaveChangesAsync();
                    return _mapper.Map<PeopleResponse>(people);
                }
                else
                {
                    throw new Exception("Essa pessoa já esta ativa.");
                }
            }
        }

        public async Task<PeopleResponse> InactivePeople(int id)
        {
            People people = await _dbContext.People.Where(p => p.Id == id)
                .FirstOrDefaultAsync() ?? new People();
            if (people == null) throw new Exception("Essa pessoa não existe no banco de dados");
            if (people.IdCompany == null)
            {
                people.Status = Enum.Status.Inactive;
                _dbContext.People.Update(people);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<PeopleResponse>(people);
            }
            else
            {
                throw new Exception("Impossivel inativar, essa pessoa pertence a uma empresa.");
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
                        throw new Exception("Impossivel excluir, essa pessoa pertence a uma empresa.");
                    }
                }   
    }
}
