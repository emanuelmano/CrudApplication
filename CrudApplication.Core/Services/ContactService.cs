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
    public class ContactService : IContactService
    {
        private readonly AppDbContex _context;
        private readonly ILogger<ContactService> _logger;

        public ContactService(AppDbContex context,ILogger<ContactService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<ContactModel>> GetAllContactsAsync(int skip, int take)
        {
            try
            {
                return await _context.Contacts.Select(x => new ContactModel
                {
                    Id = x.ContactId,
                    ContactName = x.ContactName
                }).Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all contacts");
                throw;
            }
          
        }

        public async Task<ContactModel> GetContactByIdAsync(int id)
        {
            try
            {
                if (_context.Contacts.Count(x => x.ContactId == id) == 0)
                    throw new Exception($"Contact with Id ={id} not exist");

                var contact = await _context.Contacts.Where(x => x.ContactId == id).Select(x => new ContactModel()
                {
                    Id = x.ContactId,
                    ContactName = x.ContactName
                }).FirstAsync();

                return await Task.FromResult(contact);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while fetching contact with Id={id}", id);
                throw;
            }
         
        }
        public async Task<int> CreateContactAsync(ContactModel contactModel)
        {
            try
            {
                var contact = new Contact()
                {
                    ContactName = contactModel.ContactName
                };

                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return contact.ContactId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating new contact");
                throw;
            }
         
        }

        public async Task<int> UpdateContactAsync(ContactModel contactModel)
        {
            try
            {
                var contact = _context.Contacts.FirstOrDefault(x => x.ContactId == contactModel.Id);
                if (contact == null)
                    throw new Exception($"Contact with Id ={contactModel.Id} not exist");

                contact.ContactName = contactModel.ContactName;

                _context.Entry(contact).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return contact.ContactId;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while updating contact with Id={id}", contactModel.Id);
                throw;
            }
         
        }
        public async Task<int> DeleteContactAsync(int id)
        {
            try
            {
                var contact = _context.Contacts.FirstOrDefault(x => x.ContactId == id);
                if (contact == null)
                    throw new Exception($"Contact with Id ={id} not exist");

                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting country with Id={id}", id);
                throw;
            }
         
        }
        public async Task<List<Contact>> FilterContactsAsync(int countryId, int companyId)
        {
            try
            {
                return await _context.Contacts
                .Include(x => x.Company)
                .Include(x => x.Country)
                .Where(c => c.CountryId == countryId || c.CompanyId == companyId).ToListAsync();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error occurred while filtering contacts with CountryId={countryId} and CompanyId={companyId}", countryId, companyId);
                throw;
            } 
        }

    }
}
