using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.BaseControllers;
using webapi.Dtos.Contact;
using webapi.Services.ContactService;

namespace webapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// Retrieves all contacts.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(new { Items = await _contactService.GetAll() });
        }

        /// <summary>
        /// Retrieves a contact by ID.
        /// </summary>
        /// <param name="id">The ID of the contact to retrieve.</param>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                return Ok(new { Items = await _contactService.GetById(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new contact.
        /// </summary>
        /// <param name="newContact">The contact to create.</param>
        [HttpPost("Add")]
        public async Task<ActionResult> AddUser(ContactDto newContact)
        {
            try
            {
                return Ok(new { Items = await _contactService.Add(newContact) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        /// <param name="id">The ID of the contact to update.</param>
        /// <param name="updatedContact">The updated contact details.</param>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, ContactDto updatedContact)
        {
            try
            {
                return Ok(new { Items = await _contactService.Update(id, updatedContact) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(new { Items = await _contactService.Delete(id) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
