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
    public class ContactService : IContactService
    {
        private readonly DataContext _context;
        private readonly ContactValidator _validator;

        public ContactService(DataContext context, ContactValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            
                
            _validator.ValidateAndThrow(contact);

            await _context.AddAsync(contact);

            await SaveChangesAsync();

            return contact;


        }

        public async Task DeleteContactAsync(int contactId)
        {
            var contact = await GetContactByIdHelperAsync(contactId);

            if (contact == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(contact);
            await SaveChangesAsync();
        }

        //public async Task<PagedList<Contact>> GetAllContactsAsync(ContactParameters contactParameters)
        public ICollection<Contact> GetAllContactsAsync()
        {
            var list = _context.Contacts.Include(x => x.Details).ToList();
            return list;
            //var contacts = await _context.Contacts.ToListAsync();
            //if (contacts == null || contacts.Count == 0)
            //{
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //}

            //IQueryable<Contact> queryable = contacts.AsQueryable();


            //return PagedList<Contact>.ToPagedList(queryable, contactParameters.PageNumber, contactParameters.PageSize);

        }

        public async Task<Contact> GetCurrentContact()
        {
            var contact = await _context.Contacts.FromSqlRaw(" SELECT TOP 1 * FROM Contacts ORDER BY ContactId DESC").FirstOrDefaultAsync();
            return (Contact)contact;
        }

        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(e => e.ContactId == contactId);
            if (contact == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);


            }

            return contact;
        }

        public async Task<Contact> UpdateContactAsync(int contactId, Contact contact)
        {
            var oldContact = await GetContactByIdHelperAsync(contactId);
            if (oldContact == null)
            {
                await CreateContactAsync(contact);
                return contact;
            }
            else
            {

                oldContact.FirstName = contact.FirstName;
                oldContact.LastName = contact.LastName;
                



                await SaveChangesAsync();

                if (oldContact == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return oldContact;

            }
        }

       

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<Contact> GetContactByIdHelperAsync(int contactId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(e => e.ContactId == contactId);

            if (contact == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return contact;
        }

      
    }
}
