using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;
using webapi.Resources;
using webapi.Repositories;

namespace webapi.Services.ContactService
{
    /// <summary>
    /// Implementation of the contact service.
    /// </summary>
    public class ContactService : IContactService
    {
        private readonly IGenericRepository<Contact> _genericRepository;

        public ContactService(DataContext context)
        {
            _genericRepository = new GenericRepository<Contact>(context);

        }

        /// <summary>
        /// Retrieves all contacts.
        /// </summary>
        /// <returns>A list of all contacts.</returns>
        public async Task<List<Contact>> GetAll()
        {
            List<Contact> contacts = (List<Contact>)await _genericRepository.GetAll();

            return contacts;
        }

        /// <summary>
        /// Retrieves a contact by ID.
        /// </summary>
        /// <param name="id">The ID of the contact to retrieve.</param>
        /// <returns>The contact with the specified ID, or null if not found.</returns>
        public async Task<Contact> GetById(int id)
        {
            Contact contact = await _genericRepository.GetById(x => x.Id == id).FirstOrDefaultAsync();

            if (contact != null)
            {
                return contact;
            }
            else
            {
                throw new Exception(MessageKeys.ContactNotFound);
            }
        }

        /// <summary>
        /// Adds a contact user.
        /// </summary>
        /// <param name="newContact">The contact to add.</param>
        /// <returns>The newly added contact.</returns>
        public async Task<Contact> Add(Contact newContact)
        {
            // Add the contact to the database
            _genericRepository.Insert(newContact);

            return newContact;
        }

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        /// <param name="updatedContact">The updated contact details.</param>
        /// <returns>The updated contact object, or null if the user was not found.</returns>
        public async Task<Contact> Update(Contact updatedContact)
        {
            // Find the contact to update by ID
            Contact contact = await _genericRepository.GetById(x => x.Id == updatedContact.Id).FirstOrDefaultAsync();

            if (contact != null)
            {
                return contact;
            }
            else
            {
                throw new Exception(MessageKeys.ContactNotFound);
            }
        }

        /// <summary>
        /// Deletes a contact by ID.
        /// </summary>
        /// <param name="id">The ID of the contact to delete.</param>
        /// <returns>The boolean flag.</returns>
        public async Task<bool> Delete(int id)
        {
            // Find the contact to delete by ID
            var contact = await _genericRepository.GetById(x => x.Id == id).FirstOrDefaultAsync();

            if (contact != null)
            {
                return _genericRepository.Delete(contact);
            }
            else
            {
                throw new Exception(MessageKeys.ContactNotFound);
            }
        }
    }
}
