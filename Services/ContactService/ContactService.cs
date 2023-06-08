using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Services.ContactService
{
    /// <summary>
    /// Implementation of the contact service.
    /// </summary>
    public class ContactService : IContactService
    {
        // The DataContext instance for accessing the data
        private readonly DataContext _context;

        public ContactService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all contacts.
        /// </summary>
        /// <returns>A list of all contacts.</returns>
        public async Task<List<Contact>> GetAll()
        {
            List<Contact> contacts = await _context.Contact.ToListAsync();

            return contacts;
        }

        /// <summary>
        /// Retrieves a contact by ID.
        /// </summary>
        /// <param name="id">The ID of the contact to retrieve.</param>
        /// <returns>The contact with the specified ID, or null if not found.</returns>
        public async Task<Contact> GetById(int id)
        {
            Contact contact = await _context.Contact.FirstOrDefaultAsync(c => c.Id == id);

            if (contact != null)
            {
                return contact;
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
        public async Task<List<Contact>> Add(Contact newContact)
        {
            // Add the contact to the database
            await _context.Contact.AddAsync(newContact);
            await _context.SaveChangesAsync();

            return _context.Contact.ToList();
        }

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        /// <param name="id">The ID of the contact to update.</param>
        /// <param name="updatedUser">The updated contact details.</param>
        /// <returns>The updated contact object, or null if the user was not found.</returns>
        public async Task<Contact> Update(int id, Contact updatedUser)
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

                return contact;
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
        public async Task<List<Contact>> Delete(int id)
        {
            // Find the contact to delete by ID
            Contact contact = await _context.Contact.FirstAsync(u => u.Id == id);

            if (contact != null)
            {
                // Remove the contact from the database
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();

                return _context.Contact.ToList();
            }
            else
            {
                throw new Exception("Contact not found.");
            }
        }
    }
}
