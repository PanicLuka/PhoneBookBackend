using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBookBackend.Entities;
using PhoneBookBackend.Helpers;
using PhoneBookBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBookBackend.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<ActionResult<List<Contact>>> GetAllContactsAsync([FromQuery] ContactParameters contactParameters)
        public ActionResult<ICollection<Contact>> GetAllContactsAsync()
        {
            var contacts = _contactService.GetAllContactsAsync();

            return Ok(contacts);
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("current")]
        public async Task<ActionResult<int>> GetCurrentContact()
        {
            try
            {
                var contact = await _contactService.GetCurrentContact();

                int contactId = contact.ContactId;

                return Ok(contactId);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status204NoContent, e.Message);

            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{contactId}")]
        public async Task<ActionResult<Contact>> GetContactByIdAsync(int contactId)
        {
            var contact = await _contactService.GetContactByIdAsync(contactId);

            return Ok(contact);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Contact>> CreateContactAsync([FromBody] Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _contactService.CreateContactAsync(contact);
                    return Ok(contact);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{contactId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Contact>> UpdateContactAsync(int contactId, [FromBody] Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newContact = await _contactService.UpdateContactAsync(contactId, contact);
                    return Ok(newContact);
                }
                else
                {
                    return BadRequest();
                }
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{contactId}")]
        public async Task<IActionResult> DeleteContactAsync(int contactId)
        {
            try
            {
                await _contactService.DeleteContactAsync(contactId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }
    }
}
