using CrudApplication.Data;
using CrudApplication.Models;
using CrudApplication.Models.Entities;
using CrudApplication.Services.Interfaces;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApplication.Services
{
    public class CountryService : ICountryService
    {

        private readonly AppDbContex _context;
        private readonly ILogger<CountryService> _logger;

        public CountryService(AppDbContex context,ILogger<CountryService>logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<CountryModel>> GetAllCountriesAsync(int skip, int take)
        {
            try
            {
                return await _context.Countries.Select(x => new CountryModel
                {
                    Id = x.CountryId,
                    CountryName = x.CountryName
                }).Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all countries");
                throw;
            }
            
        }
        public async Task<CountryModel> GetCountryByIdAsync(int id)
        {
            try
            {
                if (_context.Countries.Count(x => x.CountryId == id) == 0)
                    throw new Exception($"Country with Id ={id} not exist");

                var country = await _context.Countries.Where(x => x.CountryId == id).Select(x => new CountryModel()
                {
                    Id = x.CountryId,
                    CountryName = x.CountryName
                }).FirstAsync();

                return await Task.FromResult(country);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching country with Id={id}", id);
                throw;
            }
            
        }

        public async Task<int> CreateCountryAsync(CountryModel countryModel)
        {
            try
            {
                var country = new Country()
                {
                    CountryName = countryModel.CountryName
                };

                _context.Countries.Add(country);
                await _context.SaveChangesAsync();
                return country.CountryId;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while creating new country");
                throw;
            }
         
        }

        public async Task<int> UpdateCountryAsync(CountryModel countryModel)
        {
            try
            {
                var country = _context.Countries.FirstOrDefault(x => x.CountryId == countryModel.Id);
                if (country == null)
                    throw new Exception($"Country with Id ={countryModel.Id} not exist");

                country.CountryName = countryModel.CountryName;

                _context.Entry(country).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return country.CountryId;
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex, "Error occurred while updating country with Id={id}", countryModel.Id);
                throw;
            }
         
        }
        public async Task<int> DeleteCountryAsync(int id)
        {
            try
            {
                var country = _context.Countries.FirstOrDefault(x => x.CountryId == id);
                if (country == null)
                    throw new Exception($"Country with Id ={id} not exist");

                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                return id;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while deleting country with Id={id}", id);
                throw;
            }
           
        }
    }
}
