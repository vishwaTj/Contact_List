using ContactBE.Models;
using ContactBE.Models.Dtos;

namespace ContactBE.Buisness
{
    public interface IContactBL
    {
        // Function returns a list of Contacts
        public List<ContactData> GetContacts();

        // Function returns a set of Default contacts
        public List<ContactData> GetDefaultContacts();

        //Function returns a specific contact for that id
        public ContactData GetContactData(Guid id);

        //Function creates a new contact and returns its Guid
        public Guid CreateContact(CreateContactDto contactDto);

        //Function Deletes a contact
        public void UpdateContact(ContactData contact);

        // Function deletes a contact
        public void DeleteContact(Guid contactId);


    }
}
