using Microsoft.AspNetCore.Mvc;
using ProjectOverdrive.API.Models;
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
        public async Task<ActionResult<List<Company>>> SearchCompany()
        {
            List<Company> company = await _companyRepository.SearchCompany();
            return Ok(company);
        }

        [HttpGet("SearchCompanyById/{id}")]
        public async Task<ActionResult<List<Company>>> SearchCompanyById(int id)
        {
            Company company = await _companyRepository.SearchCompanyById(id);
            return Ok(company);
        }

        [HttpGet("SearchCompanyByName/{name}")]
        public async Task<ActionResult<List<Company>>> SearchCompanyByName(string name)
        {
            Company company = await _companyRepository.SearchCompanyByName(name);
            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult<Company>> AddCompany([FromBody] Company company)
        {
            Company companyAdd = await _companyRepository.AddCompany(company);
            return Ok(companyAdd);
        }

        [HttpPut("UpdateCompany/{id}")]
        public async Task<ActionResult<Company>> UpdateCompany([FromBody] Company company,
            int id)
        {
            company.Id = id;
            Company companyUpdate = await _companyRepository.UpdateCompany(company, id);
            return Ok(companyUpdate);
        }

        [HttpDelete("DeleteCompany/{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {

            bool deleted = await _companyRepository.DeleteCompany(id);
            return Ok(deleted);
        }
    }
}
