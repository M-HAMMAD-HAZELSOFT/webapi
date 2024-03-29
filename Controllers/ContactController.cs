﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using webapi.Models;
using webapi.Resources;
using webapi.Dtos.Contact;
using webapi.Shared.Models;
using webapi.BaseControllers;
using webapi.Services.ContactService;

namespace webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IContactService _contactService;

        public ContactController(IMapper mapper, IContactService contactService)
        {
            _mapper = mapper;
            _contactService = contactService;
        }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <param name="queryStringParams">The query string params.</param>
        /// <returns>An ActionResult.</returns>
        [HttpGet]
        public IActionResult GetAll([FromQuery] QueryStringParams queryStringParams)
        {
            PagedResult<Contact> result;

            try
            {
                result = _contactService.GetPagedResult(queryStringParams);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }


        /// <summary>
        /// Retrieves all contacts.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var contacts = (await _contactService.GetAll())
                .Select(c => _mapper.Map<ContactDto>(c)).ToList();

            return Ok(new { Items = contacts });
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
                var contact = await _contactService.GetById(id);
                return Ok(new { Items = _mapper.Map<ContactDto>(contact) });
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
        public async Task<ActionResult> AddUser(Contact newContact)
        {
            try
            {
                var contacts = await _contactService.Add(newContact);

                return Ok(new { Items = _mapper.Map<ContactDto>(contacts) });
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
        public async Task<IActionResult> UpdateUser(int id, Contact updatedContact)
        {
            try
            {
                var contact = await _contactService.Update(updatedContact);

                return Ok(new { Items = _mapper.Map<ContactDto>(contact) });
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
                if (await _contactService.Delete(id))
                {
                    return Ok(MessageKeys.DeletedSuccessfully);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
