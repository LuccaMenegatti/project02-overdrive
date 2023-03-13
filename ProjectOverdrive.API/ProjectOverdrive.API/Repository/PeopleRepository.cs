using Microsoft.EntityFrameworkCore;
using ProjectOverdrive.API.Data;
using ProjectOverdrive.API.Models;
using ProjectOverdrive.API.Repository.Interfaces;
using System.Xml.Linq;

namespace ProjectOverdrive.API.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly ApiDbContext _dbContext;
        public PeopleRepository(ApiDbContext apiDbContext) 
        {
            _dbContext = apiDbContext;
        }

        public async Task<People> SearchPeopleById(int id)
        {
            return await _dbContext.People.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<People> SearchPeopleByName(string name)
        {
            return await _dbContext.People.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<People>> SearchPeople()
        {
            return await _dbContext.People.ToListAsync();
        }

        public async Task<People> AddPeople(People people)
        {
            await _dbContext.People.AddAsync(people);
            await _dbContext.SaveChangesAsync();

            return people;
        }

        public async Task<People> UpdatePeople(People people, int id)
        {
            People peopleById = await SearchPeopleById(id);

            if(peopleById == null)
            {
                throw new Exception($"Pessoa do ID: {id} não foi encontrada " +
                    $"no banco de dados.");
            }

            peopleById.Name = people.Name;
            peopleById.Cpf = people.Cpf;
            peopleById.NumberContact = people.NumberContact;
            peopleById.UserName = people.UserName;
            peopleById.Status = people.Status;
            peopleById.Company = people.Company;

            _dbContext.People.Update(peopleById);
            await _dbContext.SaveChangesAsync();

            return peopleById;
        }

        public async Task<bool> DeletePeople(int id)
        {
            People peopleById = await SearchPeopleById(id);

            if (peopleById == null)
            {
                throw new Exception($"Pessoa do ID: {id} não foi encontrada " +
                    $"no banco de dados.");
            }

            _dbContext.People.Remove(peopleById);
            await _dbContext.SaveChangesAsync();

            return true;

        }

        
    }
}
