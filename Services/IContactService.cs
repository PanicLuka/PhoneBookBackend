using PhoneBookBackend.Entities;
using PhoneBookBackend.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBookBackend.Services
{
    public interface IContactService
    {
        Task<Contact> CreateContactAsync(Contact contact);

        //Task<PagedList<Contact>> GetAllContactsAsync(ContactParameters contactParameters);
        ICollection<Contact> GetAllContactsAsync();
        Task<Contact> GetContactByIdAsync(int contactId);

        Task<Contact> GetCurrentContact();
        Task<Contact> UpdateContactAsync(int contactId, Contact contact);

        Task DeleteContactAsync(int contactId);

    }
}
