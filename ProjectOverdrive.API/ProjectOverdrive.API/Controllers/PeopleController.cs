using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOverdrive.API.Models;
using ProjectOverdrive.API.Repository.Interfaces;

namespace ProjectOverdrive.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;
        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        [HttpGet("SearchPeople")]
        public async Task<ActionResult<List<People>>> SearchPeople()
        {
            List<People> people = await _peopleRepository.SearchPeople();
            return Ok(people);
        }

        [HttpGet("SearchPeopleById/{id}")]
        public async Task<ActionResult<List<People>>> SearchPeopleById(int id)
        {
            People people = await _peopleRepository.SearchPeopleById(id);
            return Ok(people);
        }

        [HttpGet("SearchPeopleByName/{name}")]
        public async Task<ActionResult<List<People>>> SearchPeopleByName(string name)
        {
            People people = await _peopleRepository.SearchPeopleByName(name);
            return Ok(people);
        }

        [HttpPost]
        public async Task<ActionResult<People>> AddPeople([FromBody] People people)
        {
            People peopleAdd =  await _peopleRepository.AddPeople(people);
            return Ok(peopleAdd);
        }

        [HttpPut("UpdatePeople/{id}")]
        public async Task<ActionResult<People>> UpdatePeople([FromBody] People people,
            int id)
        {
            people.Id = id;
            People peopleUpdate = await _peopleRepository.UpdatePeople(people, id);
            return Ok(peopleUpdate);
        }

        [HttpDelete("DeletePeople/{id}")]
        public async Task<ActionResult<People>> DeletePeople(int id)
        {
           
            bool deleted = await _peopleRepository.DeletePeople(id);
            return Ok(deleted);
        }
    }
}
