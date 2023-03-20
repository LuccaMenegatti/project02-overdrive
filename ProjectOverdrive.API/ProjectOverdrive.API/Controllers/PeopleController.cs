using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;
using ProjectOverdrive.API.Repository;
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
            _peopleRepository = peopleRepository ?? throw new ArgumentException(nameof(Repository)); 
        }

        [HttpGet("SearchPeople")]
        public async Task<ActionResult<IEnumerable<People>>> SearchPeople()
        {
            var peoples = await _peopleRepository.SearchPeople();
            return Ok(peoples);
        }


        [HttpGet("SearchPeopleByName/{name}")]
        public async Task<ActionResult<List<PeopleResponse>>> SearchPeopleByName(string name)
        {
            var people = await _peopleRepository.SearchPeopleByName(name);
            if (people == null) return NotFound();
            return Ok(people);
        }

        [HttpPut("AddPeopleInCompany")]
        public async Task<ActionResult<PeopleRequest>> AddPeopleInCompany(int idPeople, int idCompany)
        {
            if (idPeople == null && idCompany == null) return BadRequest();
            var people = await _peopleRepository.AddPeopleInCompany(idPeople, idCompany);
            return Ok(people);
        }

        [HttpPut("RemovePeopleInCompany")]
        public async Task<ActionResult<PeopleRequest>> RemovePeopleInCompany(int idPeople)
        {
            if (idPeople == null) return BadRequest();
            var people = await _peopleRepository.RemovePeopleInCompany(idPeople);
            return Ok(people);
        }

        [HttpPost]
        public async Task<ActionResult<PeopleRequest>> AddPeople([FromBody] PeopleRequest vo)
        {
            if (vo == null) return BadRequest(); 
            var peopleAdd =  await _peopleRepository.AddPeople(vo);
            return Ok(peopleAdd);
        }

        [HttpPut("UpdatePeople")]
        public async Task<ActionResult<PeopleUpdateRequest>> UpdatePeople([FromBody] PeopleUpdateRequest vo)
        {
            if (vo == null) return BadRequest();
            var peopleUpdate = await _peopleRepository.UpdatePeople(vo);
            return Ok(peopleUpdate);
        }

        [HttpPut("InactivePeople")]
        public async Task<ActionResult<PeopleUpdateRequest>> InactivePeople(int id)
        {
            if (id == null) return BadRequest();
            var peopleUpdate = await _peopleRepository.InactivePeople(id);
            return Ok(peopleUpdate);
        }

        [HttpDelete("DeletePeople/{id}")]
        public async Task<ActionResult<People>> DeletePeople(int id)
        {
            var deleted = await _peopleRepository.DeletePeople(id);
            if(!deleted) return BadRequest();
            return Ok(deleted);
        }
    }
}
