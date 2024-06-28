using CrudApplication.Data;
using CrudApplication.Models;
using CrudApplication.Models.Entities;
using CrudApplication.Services;
using CrudApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContex _context;
        private readonly ICompanyService _companyService;
        public CompaniesController(AppDbContex context, ICompanyService companyService)
        {
            _companyService = companyService;
            _context = context;
        }


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Company>>> GetAllCompanies()
        //{

        //    var allCompanies = await _context.Companies.ToListAsync();

        //    return allCompanies;

        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetAllCompanies(int skip, int take)
        {
            var companies = await _companyService.GetAllCompaniesAsync(skip, take);
            return Ok(companies);
        }

        //[HttpGet("{id}")]

        //public async Task<ActionResult<Company>> GetCompany(int id)
        //{
        //    var company = await _context.Companies.FindAsync(id);

        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    return company;
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyModel>> GetCompany(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        //[HttpPost]

        //public async Task<ActionResult<int>> CreateCompany(Company company)
        //{
        //    _context.Companies.Add(company);
        //    await _context.SaveChangesAsync();

        //    return company.Id;
        //}
        [HttpPost]
        public async Task<ActionResult<int>> CreateCompany(CompanyModel company)
        {
            var companyId = await _companyService.CreateCompanyAsync(company);
            return Ok(companyId);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCompany(int id, Company company)
        //{
        //    if (id != company.Id)
        //        return BadRequest();

        //    _context.Entry(company).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return Ok(id);
        //    }
        //    catch (DbUpdateConcurrencyException) when (CompanyExists(id) == null)
        //    {
        //        return NotFound();
        //    }
        //}
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateCompany(CompanyModel company)
        {
            await _companyService.UpdateCompanyAsync(company);
            return Ok(company.Id);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCompany(int id)
        //{
        //    var company = await _context.Companies.FindAsync(id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Companies.Remove(company);
        //    await _context.SaveChangesAsync();

        //    return Ok(id);
        //}
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCompany(int id)
        {
            await _companyService.DeleteCompanyAsync(id);
            return Ok(id);
        }


    }
}
