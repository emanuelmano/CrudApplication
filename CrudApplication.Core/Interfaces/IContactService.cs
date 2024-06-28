using CrudApplication.Models;
using CrudApplication.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApplication.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<ContactModel>> GetAllContactsAsync(int skip, int take);
        Task<ContactModel> GetContactByIdAsync(int id);
        Task<int> CreateContactAsync(ContactModel contactModel);
        Task<int> UpdateContactAsync(ContactModel contactModel);
        Task<int> DeleteContactAsync(int id);
        Task<List<Contact>> FilterContactsAsync(int countryId, int companyId);
    }
}
