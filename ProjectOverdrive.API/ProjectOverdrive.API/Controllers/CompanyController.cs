﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("SearchPeopleInCompany/{id}")]
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
            if (companyAdd != null) return Ok(companyAdd);
            else return BadRequest("Esse CNPJ já foi cadastrado");
        }

        [HttpPut("UpdateCompany")]
        public async Task<ActionResult<CompanyUpdateRequest>> UpdateCompany([FromBody] CompanyUpdateRequest vo)
        {
            if (vo == null) return BadRequest();
            if (vo.Id == null) return BadRequest("Id não pode ser nulo");
            var companyUpdate = await _companyRepository.UpdateCompany(vo);
            return Ok(companyUpdate);
        }

        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult<string>> ChangeStatus(int id)
        {
            if (id == null) return BadRequest();
            var status = await _companyRepository.ChangeCompanyStatus(id);
            return Ok(status);
        }

        [HttpDelete("DeleteCompany/{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            var deleted = await _companyRepository.DeleteCompany(id);
            if (!deleted) return BadRequest();
            return Ok(deleted);
        }
    }
}
