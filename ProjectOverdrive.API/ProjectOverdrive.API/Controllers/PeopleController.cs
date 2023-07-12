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

        [HttpGet("SearchPeopleByCpf/{cpf}")]
        public async Task<ActionResult<List<PeopleResponse>>> SearchPeopleByCpf(string cpf)
        {
            var people = await _peopleRepository.SearchPeopleByCpf(cpf);
            if (people == null) return NotFound();
            return Ok(people);
        }

        [HttpPut("AddPeopleInCompany/{idPeople}/{idCompany}")]
        public async Task<ActionResult<PeopleResponse>> AddPeopleInCompany(int idPeople, int idCompany)
        {
            if (idPeople == 0 && idCompany == 0) return BadRequest();
            var people = await _peopleRepository.AddPeopleInCompany(idPeople, idCompany);
            return Ok(people);
        }

        [HttpPut("RemovePeopleInCompany/{idPeople}")]
        public async Task<ActionResult<PeopleResponse>> RemovePeopleInCompany(int idPeople)
        {
            if (idPeople == 0) return BadRequest();
            var people = await _peopleRepository.RemovePeopleInCompany(idPeople);
            return Ok(people);
        }

        [HttpPost]
        public async Task<ActionResult<PeopleResponse>> AddPeople([FromBody] PeopleRequest vo)
        {
            if (vo == null) return BadRequest(); 
            var peopleAdd =  await _peopleRepository.AddPeople(vo);
            if (peopleAdd != null) return Ok(peopleAdd);
            else return BadRequest("Esse Cpf ou Telefone já foi cadastrado");
        }

        [HttpPut("UpdatePeople")]
        public async Task<ActionResult<PeopleResponse>> UpdatePeople([FromBody] PeopleUpdateRequest vo)
        {
            if (vo == null) return BadRequest();
            if (vo.Id == null) return BadRequest("Esse Id não existe.");
            var peopleUpdate = await _peopleRepository.UpdatePeople(vo);
            return Ok(peopleUpdate);
        }

        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult<string>> ChangeStatus(int id)
        {
            if (id == null) return BadRequest();
            var status = await _peopleRepository.ChangePeopleStatus(id);
            return Ok(status);
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
