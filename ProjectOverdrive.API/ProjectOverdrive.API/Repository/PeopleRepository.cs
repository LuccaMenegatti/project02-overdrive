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
        public PeopleRepository(ApiDbContext apiDbContext, ApiDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<People> SearchById(int id)
        {
            return await _dbContext.People.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<People> SearchByName(string name)
        {
            return await _dbContext.People.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<People>> SearchPeople()
        {
            return await _dbContext.People.ToListAsync();
        }

        public async Task<People> AddPeople(People people)
        {
            _dbContext.People.Add(people);
            _dbContext.SaveChanges();

            return people;
        }

        public Task<People> UpdatePeople(People people, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePeople(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}
