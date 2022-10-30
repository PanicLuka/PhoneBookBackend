using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PhoneBookBackend.Entities;
using PhoneBookBackend.Helpers;
using PhoneBookBackend.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhoneBookBackend.Services
{
    public class ContactDetailsService : IContactDetailsService
    {
        private readonly DataContext _context;
        private readonly ContactDetailValidator _validator;
        public ContactDetailsService(DataContext context, ContactDetailValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task CreateContactDetailAsync(ContactDetails contactDetail)
        {
            _validator.ValidateAndThrow(contactDetail);

            await _context.AddAsync(contactDetail);

            await SaveChangesAsync();

        }

        public async Task DeleteContactDetailAsync(int contactDetailId)
        {
            var contactDetail = await GetContactDetailByIdHelperAsync(contactDetailId);

            if (contactDetail == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(contactDetail);
            await SaveChangesAsync();
        }

        public async Task<List<ContactDetails>> GetAllContactDetailsAsync()
        {
            var contactDetails = await _context.Details.ToListAsync();
            if (contactDetails == null || contactDetails.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return contactDetails;
        }



        public async Task<ContactDetails> GetContactDetailByIdAsync(int contactDetailId)
        {
            var contactDetail = await _context.Details.FirstOrDefaultAsync(e => e.ContactDetailsId == contactDetailId);
            if (contactDetail == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);


            }

            return contactDetail;
        }

        public async Task<ContactDetails> UpdateContactDetailAsync(int contactDetailId, ContactDetails contactDetail)
        {
            var oldContactDetail = await GetContactDetailByIdHelperAsync(contactDetailId);
            if (oldContactDetail == null)
            {
                await CreateContactDetailAsync(contactDetail);
                return contactDetail;
            }
            else
            {
                oldContactDetail.Name = contactDetail.Name;
                oldContactDetail.Value = contactDetail.Value;
                oldContactDetail.ContactId = contactDetail.ContactId;




                await SaveChangesAsync();

                if (oldContactDetail == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return oldContactDetail;

            }
        }



        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<ContactDetails> GetContactDetailByIdHelperAsync(int contactDetailId)
        {
            var contactDetail = await _context.Details.FirstOrDefaultAsync(e => e.ContactDetailsId == contactDetailId);

            if (contactDetail == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return contactDetail;
        }

        public async Task<List<ContactDetails>> GetContactDetailsByContactId(int contactId)
        {
            var contactDetails = await _context.Details.ToListAsync();

            var contactDetailsById = new List<ContactDetails>();

            foreach(var detail in contactDetails)
            {
                if(detail.ContactId == contactId)
                {
                    contactDetailsById.Add(detail);
                }
            }

            return contactDetailsById;
        }
    }
}
