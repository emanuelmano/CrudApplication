using CrudApplication.Data;
using CrudApplication.Models;
using CrudApplication.Models.Entities;
using CrudApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly AppDbContex _context;
        private readonly ICountryService _countryService;
        private readonly ILogger<CountryController> _logger;
        public CountryController(AppDbContex context, ICountryService countryService, ILogger<CountryController> logger)
        {
            _countryService = countryService;
            _context = context;
            _logger = logger;
        }


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Country>>> GetAllCountry()
        //{

        //    var allCountries = await _context.Countries.ToListAsync();

        //    _logger.Log( LogLevel.Information,"Hello Word");
        //    return allCountries;

        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetAllCountries(int skip, int take)
        {
            var countries = await _countryService.GetAllCountriesAsync(skip,take);
            return Ok(countries);
        }

        //[HttpGet("{id}")]

        //public async Task<ActionResult<Country>> GetCountry(int id)
        //{
        //    var country = await _context.Countries.FindAsync(id);

        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    return country;
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryModel>> GetCountry(int id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        //[HttpPost]

        //public async Task<ActionResult<int>> CreateCountry(Country country)
        //{
        //    _context.Countries.Add(country);
        //    await _context.SaveChangesAsync();

        //    return Ok(country.CountryId);
        //}
        [HttpPost]
        public async Task<ActionResult<int>> CreateCountry(CountryModel country)
        {
            var countryId = await _countryService.CreateCountryAsync(country);
            return Ok(countryId);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateCountry(int id, Country country)
        //{
        //    if (id != country.CountryId)
        //        return BadRequest();

        //    _context.Entry(country).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return Ok(id);
        //    }
        //    catch (DbUpdateConcurrencyException ex) when (CountryExist(id) == null)
        //    {
        //        _logger.LogError(ex.Message);
        //        return NotFound();
        //    }
        //}
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateCountry(CountryModel country)
        {
            await _countryService.UpdateCountryAsync(country);
            return Ok(country.Id);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCountry(int id)
        //{
        //    var country = await _context.Countries.FindAsync(id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Countries.Remove(country);
        //    await _context.SaveChangesAsync();

        //    return Ok(id);
        //}
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCountry(int id)
        {
            await _countryService.DeleteCountryAsync(id);
            return Ok(id);
        }

    }
}
