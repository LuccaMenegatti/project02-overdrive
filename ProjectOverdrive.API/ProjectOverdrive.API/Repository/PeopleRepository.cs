using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectOverdrive.API.Data;
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;
using ProjectOverdrive.API.Repository.Interfaces;
using System.Xml.Linq;

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
               //.Include(c => c.Company)
                    //.ThenInclude(a => a.Address)
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

        public async Task<PeopleRequest> AddPeopleInCompany(int idPeople, int idCompany)
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
                return _mapper.Map<PeopleRequest>(people);
            }
            else
            {
                throw new Exception("Para cadastrar, a pessoa e a empresa devem estar ativas!");
            }
          
        }

        public async Task<PeopleRequest> RemovePeopleInCompany(int idPeople)
        {
            People people = await _dbContext.People
                .Where(p => p.Id == idPeople)
                .FirstOrDefaultAsync();

            people.IdCompany = null;
            people.Company = null;

            _dbContext.People.Update(people);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PeopleRequest>(people);
        }

        public async Task<PeopleRequest> AddPeople(PeopleRequest vo)
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
                return _mapper.Map<PeopleRequest>(people);
            }
            else
            {
                throw new Exception("O Cpf ou Numero de Contato, já existem no banco de dados");
            }
           
        }

        public async Task<PeopleUpdateRequest> UpdatePeople(PeopleUpdateRequest vo)
        {
            People people = _mapper.Map<People>(vo);
            var checkCpf = await _dbContext.People
                .Where(p => p.Cpf == people.Cpf)
                .FirstOrDefaultAsync();
            var checkContact = await _dbContext.People
                .Where(p => p.NumberContact == people.NumberContact)
                .FirstOrDefaultAsync();

            if (vo.UserName is null || vo.NumberContact is null ||
                vo.UserName.Trim() == "" || vo.NumberContact.Trim() == "")
            {
                people.Status = Enum.Status.Pending;
            }
            else
            {
                people.Status = Enum.Status.Active;
            }

            if (checkCpf == null && checkContact == null)
            {
                _dbContext.People.Update(people);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<PeopleUpdateRequest>(people);
            }
            else
            {
                throw new Exception("O Cpf ou Numero de Contato, já existem no banco de dados");
            }
        }

        public async Task<bool> DeletePeople(int id)
        {
            try
            {
                People people = await _dbContext.People.Where(p => p.Id == id)
                .FirstOrDefaultAsync() ?? new People();
                if (people == null) return false;
                _dbContext.People.Remove(people);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        
    }
}
