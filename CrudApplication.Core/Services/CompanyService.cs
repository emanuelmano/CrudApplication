using CrudApplication.Data;
using CrudApplication.Models;
using CrudApplication.Models.Entities;
using CrudApplication.Services.Interfaces;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApplication.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContex _context;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(AppDbContex context,ILogger<CompanyService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<CompanyModel>> GetAllCompaniesAsync(int skip, int take)
        {
            try
            {
                return await _context.Companies.Select(x => new CompanyModel
                {
                    Id = x.CompanyId,
                    CompanyName = x.CompanyName
                }).Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while fetching all companies");
                throw;
            }
          
        }

        public async Task<CompanyModel> GetCompanyByIdAsync(int id)
        {
            try
            {
                if (_context.Companies.Count(x => x.CompanyId == id) == 0)
                    throw new Exception($"Company with Id ={id} not exist");

                var company = await _context.Companies.Where(x => x.CompanyId == id).Select(x => new CompanyModel()
                {
                    Id = x.CompanyId,
                    CompanyName = x.CompanyName
                }).FirstAsync();

                return await Task.FromResult(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching company with Id={id}", id);
                throw;
            }
        
        }

        public async Task<int> CreateCompanyAsync(CompanyModel companyModel)
        {
            try
            {
                var company = new Company()
                {
                    CompanyName = companyModel.CompanyName
                };

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
                return company.CompanyId;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while creating new company");
                throw;
            }
          
        }

        public async Task<int> UpdateCompanyAsync(CompanyModel companyModel)
        {
            try
            {
                var company = _context.Companies.FirstOrDefault(x => x.CompanyId == companyModel.Id);
                if (company == null)
                    throw new Exception($"Company with Id ={companyModel.Id} not exist");

                company.CompanyName = companyModel.CompanyName;

                _context.Entry(company).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return company.CompanyId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating company with Id={id}", companyModel.Id);
                throw;
            }
         
        }

        public async Task<int> DeleteCompanyAsync(int id)
        {
            try
            {
                var company = _context.Companies.FirstOrDefault(x => x.CompanyId == id);
                if (company == null)
                    throw new Exception($"Company with Id ={id} not exist");

                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting company with Id={id}", id);
                throw;
            }
         
        }
    }
}
