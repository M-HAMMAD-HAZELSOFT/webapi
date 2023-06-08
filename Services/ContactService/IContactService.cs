using webapi.Dtos.Contact;
using webapi.Models;

namespace webapi.Services.ContactService
{
    /// <summary>
    /// Interface for managing contact data.
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Retrieves all contacts.
        /// </summary>
        /// <returns>A list of all contacts.</returns>
        Task<List<Contact>> GetAll();

        /// <summary>
        /// Retrieves a contact by their ID.
        /// </summary>
        /// <param name="id">The ID of the contact to retrieve.</param>
        /// <returns>The contact with the specified ID.</returns>
        Task<Contact> GetById(int id);

        /// <summary>
        /// Adds a new contact.
        /// </summary>
        /// <param name="newContact">The contact to add.</param>
        /// <returns>A list of all contacts including the newly added contact.</returns>
        Task<List<Contact>> Add(Contact newContact);

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        /// <param name="id">The ID of the contact to update.</param>
        /// <param name="updatedContact">The updated contact object.</param>
        /// <returns>The updated contact object.</returns>
        Task<Contact> Update(int id, Contact updatedContact);

        /// <summary>
        /// Deletes a contact by their ID.
        /// </summary>
        /// <param name="id">The ID of the contact to delete.</param>
        /// <returns>The deleted contact object.</returns>
        Task<List<Contact>> Delete(int id);
    }
}
