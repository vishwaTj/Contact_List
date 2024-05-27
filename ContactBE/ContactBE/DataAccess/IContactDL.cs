using ContactBE.Models;
using ContactBE.Models.Dtos;

namespace ContactBE.DataAccess
{
    public interface IContactDL
    {
        public List<ContactData> GetContactDatas();
        public ContactData GetContactData(Guid id);
        public Guid AddNewContact(CreateContactDto contactDto);

        public void DeleteContact(Guid contactId);

        public void UpdateContact(ContactData contact);
    }
}
