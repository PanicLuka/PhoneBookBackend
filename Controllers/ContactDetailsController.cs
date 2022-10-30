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
    [Route("api/contactDetails")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ContactDetailsController : ControllerBase
    {
        private readonly IContactDetailsService _contactDetailsService;

        public ContactDetailsController(IContactDetailsService contactDetailsService)
        {
            _contactDetailsService = contactDetailsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<ContactDetails>>> GetAllContactDetailsAsync()
        {
            var contactDetails = await _contactDetailsService.GetAllContactDetailsAsync();

            return Ok(contactDetails);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{contactDetailId}")]
        public async Task<ActionResult<ContactDetails>> GetContactDetailByIdAsync(int contactDetailId)
        {
            var contactDetail = await _contactDetailsService.GetContactDetailByIdAsync(contactDetailId);

            return Ok(contactDetail);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("get/{contactId}")]
        public async Task<ActionResult<ContactDetails>> GetContactDetailByContactIdAsync(int contactId)
        {
            var contactDetails = await _contactDetailsService.GetContactDetailsByContactId(contactId);

            return Ok(contactDetails);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateContactDetailAsync([FromBody] ContactDetails contactDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _contactDetailsService.CreateContactDetailAsync(contactDetail);
                    return Ok();
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

        [HttpPut("{contactDetailId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContactDetails>> UpdateContactDetailAsync(int contactDetailId, [FromBody] ContactDetails contactDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newContactDetails = await _contactDetailsService.UpdateContactDetailAsync(contactDetailId, contactDetail);
                    return Ok(newContactDetails);
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
        [HttpDelete("{contactDetailId}")]
        public async Task<IActionResult> DeleteContactDetailAsync(int contactDetailId)
        {
            try
            {
                await _contactDetailsService.DeleteContactDetailAsync(contactDetailId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }
    }
}
