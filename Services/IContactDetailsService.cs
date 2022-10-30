using PhoneBookBackend.Entities;
using PhoneBookBackend.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBookBackend.Services
{
    public interface IContactDetailsService
    {
        Task CreateContactDetailAsync(ContactDetails contactDetail);

        Task<List<ContactDetails>> GetAllContactDetailsAsync();

        Task<List<ContactDetails>> GetContactDetailsByContactId(int contactId);

        Task<ContactDetails> GetContactDetailByIdAsync(int contactDetailId);

        Task<ContactDetails> UpdateContactDetailAsync(int contactDetailId, ContactDetails contactDetails);

        Task DeleteContactDetailAsync(int contactDetailsId);
    }
}
