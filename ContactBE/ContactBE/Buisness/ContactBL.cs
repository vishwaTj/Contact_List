using ContactBE.DataAccess;
using ContactBE.Models;
using ContactBE.Models.Dtos;

namespace ContactBE.Buisness
{
    public class ContactBL : IContactBL
    {
        private readonly IContactDL _contactDL;

        public ContactBL(IContactDL contactDL)
        {
            _contactDL = contactDL;
        }

        public List<ContactData> GetContacts() => _contactDL.GetContacts();

        public ContactData GetContactData(Guid contactId) => _contactDL.GetContactData(contactId);

        public Guid AddNewContact(CreateContactDto contactDto)
        {
            return _contactDL.AddNewContact(contactDto);
        }

        public void UpdateContact(ContactData contact)
        {
            _contactDL.UpdateContact(contact);
        }

        public void DeleteContact(Guid contactId)
        {
            _contactDL.DeleteContact(contactId);
        }
    }
}
