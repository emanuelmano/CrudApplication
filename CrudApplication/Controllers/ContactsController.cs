using CrudApplication.Data;
using CrudApplication.Models;
using CrudApplication.Models.Entities;
using CrudApplication.Services;
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
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly AppDbContex _context;
        private readonly ILogger<ContactsController> _logger;   
        public ContactsController(AppDbContex context, IContactService contactService, ILogger<ContactsController> logger)
        {
            _context = context;
            _contactService = contactService;
            _logger = logger;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        //{

        //    var allContacts = await _context.Contacts.ToListAsync();

        //    _logger.Log(LogLevel.Information, "Hello Word");
        //    return allContacts;

        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactModel>>> GetAllContacts(int skip, int take)
        {
            var contacts = await _contactService.GetAllContactsAsync(skip, take);
            return Ok(contacts);
        }

        //[HttpGet("{id}")]

        //public async Task<ActionResult<Contact >> GetContact(int id)
        //{
        //    var contact = await _context.Contacts.FindAsync(id);

        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    return contact;
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactModel>> GetContact(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        //[HttpPost]

        //public async Task<ActionResult<int>> CreateContacty(Contact contact)
        //{
        //    _context.Contacts.Add(contact);
        //    await _context.SaveChangesAsync();

        //    return Ok(contact.CountryId);
        //}
        [HttpPost]
        public async Task<ActionResult<int>> CreateContact(ContactModel contact)
        {
            var contactId = await _contactService.CreateContactAsync(contact);
            return Ok(contactId);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateContact(int id, Contact contact)
        //{
        //    if (id != contact.CountryId)
        //        return BadRequest();

        //    _context.Entry(contact).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return Ok(id);
        //    }
        //    catch (DbUpdateConcurrencyException ex) when (ContactExist(id) == null)
        //    {
        //        _logger.LogError(ex.Message);
        //        return NotFound();
        //    }
        //}
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateContact(ContactModel contact)
        {
            await _contactService.UpdateContactAsync(contact);
            return Ok(contact.Id);
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteContact(int id)
        //{
        //    var contact = await _context.Contacts.FindAsync(id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Contacts.Remove(contact);
        //    await _context.SaveChangesAsync();

        //    return Ok(id);
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteContact(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return Ok(id);
        }

        //[HttpGet("filter")]
        //public async Task<ActionResult<IEnumerable<Contact>>> FilterContact(int countryId, int companyId)
        //{
        //    var filteredContacts = await _context.Contacts
        //        .Where(c => c.CountryId == countryId && c.CompanyId == companyId)
        //        .ToListAsync();

        //    if (filteredContacts == null || filteredContacts.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(filteredContacts);
        //}


        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Contact>>> FilterContacts(int countryId, int companyId)
        {
            var contacts = await _contactService.FilterContactsAsync(countryId, companyId);
            return Ok(contacts);
        }

    }

}
