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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet("SearchCompany")]
        public async Task<ActionResult<List<SearchCompanyResponse>>> SearchCompany()
        {
            var company = await _companyRepository.SearchCompany();
            return Ok(company);
        }


        [HttpGet("SearchCompanyByCnpj/{cnpj}")]
        public async Task<ActionResult<List<SearchCompanyResponse>>> SearchCompanyByCnpj(string cnpj)
        {
            var company = await _companyRepository.SearchCompanyByCnpj(cnpj);
            if (company == null) return NotFound();
            return Ok(company);
        }

        [HttpGet("SearchCompanyByName/{name}")]
        public async Task<ActionResult<List<SearchCompanyResponse>>> SearchCompanyByName(string name)
        {
            var company = await _companyRepository.SearchCompanyByName(name);
            if (company == null) return NotFound();
            return Ok(company);
        }

        [HttpGet("SearchPeopleInCompany{id}")]
        public async Task<ActionResult<CompanyOffAddressResponse>> SearchPeopleInCompany(int id)
        {
            var peoples = await _companyRepository.SearchPeopleInCompany(id);
            return Ok(peoples);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyRequest>> AddCompany([FromBody] CompanyRequest vo)
        {
            if (vo == null) return BadRequest();
            var companyAdd = await _companyRepository.AddCompany(vo);
            return Ok(companyAdd);
        }

        [HttpPut("UpdateCompany")]
        public async Task<ActionResult<CompanyUpdateRequest>> UpdateCompany([FromBody] CompanyUpdateRequest vo)
        {
            if (vo == null) return BadRequest();
            var companyUpdate = await _companyRepository.UpdateCompany(vo);
            return Ok(companyUpdate);
        }

        [HttpPut("ActiveCompany")]
        public async Task<ActionResult<CompanyOffAddressAndPeopleResponse>> ActiveCompany(int id)
        {
            if (id == null) return BadRequest();
            var companyUpdate = await _companyRepository.ActiveCompany(id);
            return Ok(companyUpdate);
        }

        [HttpPut("InactiveCompany")]
        public async Task<ActionResult<CompanyOffAddressAndPeopleResponse>> InactiveCompany(int id)
        {
            if (id == null) return BadRequest();
            var companyUpdate = await _companyRepository.InactiveCompany(id);
            return Ok(companyUpdate);
        }

        [HttpDelete("DeleteCompany")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            var deleted = await _companyRepository.DeleteCompany(id);
            if (!deleted) return BadRequest();
            return Ok(deleted);
        }
    }
}
