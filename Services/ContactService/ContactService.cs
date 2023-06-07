using Microsoft.EntityFrameworkCore;
using AutoMapper;
using webapi.Data;
using webapi.Models;
using webapi.Dtos.Contact;

namespace webapi.Services.ContactService
{
    /// <summary>
    /// Implementation of the contact service.
    /// </summary>
    public class ContactService : IContactService
    {
        // The IMapper instance for mapping between different types
        private readonly IMapper _mapper;
        // The DataContext instance for accessing the data
        private readonly DataContext _context;

        public ContactService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Retrieves all contacts.
        /// </summary>
        /// <returns>A list of all contacts.</returns>
        public async Task<List<ContactDto>> GetAll()
        {
            List<Contact> user = await _context.Contact.ToListAsync();

            return (user.Select(c => _mapper.Map<ContactDto>(c))).ToList();
        }

        /// <summary>
        /// Retrieves a contact by ID.
        /// </summary>
        /// <param name="id">The ID of the contact to retrieve.</param>
        /// <returns>The contact with the specified ID, or null if not found.</returns>
        public async Task<ContactDto> GetById(int id)
        {
            Contact contact = await _context.Contact.FirstOrDefaultAsync(c => c.Id == id);

            ContactDto result = _mapper.Map<ContactDto>(contact);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("Contact not found.");
            }
        }

        /// <summary>
        /// Adds a contact user.
        /// </summary>
        /// <param name="newContact">The contact to add.</param>
        /// <returns>A list of all contacts including the newly added contact.</returns>
        public async Task<List<ContactDto>> Add(ContactDto newContact)
        {
            Contact contact = _mapper.Map<Contact>(newContact);

            // Add the contact to the database
            await _context.Contact.AddAsync(contact);
            await _context.SaveChangesAsync();

            return (_context.Contact.Select(u => _mapper.Map<ContactDto>(u))).ToList();
        }

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        /// <param name="id">The ID of the contact to update.</param>
        /// <param name="updatedUser">The updated contact details.</param>
        /// <returns>The updated contact object, or null if the user was not found.</returns>
        public async Task<ContactDto> Update(int id, ContactDto updatedUser)
        {
            // Find the contact to update by ID
            Contact contact = await _context.Contact.FirstOrDefaultAsync(u => u.Id == id);

            if (contact != null)
            {
                // Update the contact 
                contact.FirstName = updatedUser.FirstName;
                contact.LastName = updatedUser.LastName;
                contact.PhoneNumber = updatedUser.PhoneNumber;
                contact.Email = updatedUser.Email;
                contact.Address = updatedUser.Address;

                _context.Contact.Update(contact);
                await _context.SaveChangesAsync();

                return _mapper.Map<ContactDto>(contact);
            }
            else
            {
                throw new Exception("Contact not found.");
            }
        }

        /// <summary>
        /// Deletes a contact by ID.
        /// </summary>
        /// <param name="id">The ID of the contact to delete.</param>
        /// <returns>The deleted contact object, or null if the contact was not found.</returns>
        public async Task<List<ContactDto>> Delete(int id)
        {
            // Find the contact to delete by ID
            Contact contact = await _context.Contact.FirstAsync(u => u.Id == id);

            if (contact != null)
            {
                // Remove the contact from the database
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();

                return (_context.Contact.Select(c => _mapper.Map<ContactDto>(c))).ToList();
            }
            else
            {
                throw new Exception("Contact not found.");
            }
        }
    }
}
