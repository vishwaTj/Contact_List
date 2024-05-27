using ContactBE.Models;
using ContactBE.Models.Dtos;

namespace ContactBE.Buisness
{
    public interface IContactBL
    {
        public List<ContactData> GetContactDatas();

        public ContactData GetContactData(Guid id);
        public Guid AddNewContact(CreateContactDto contactDto);

        public void DeleteContact(Guid contactId);

        public void UpdateContact(ContactData contact);

    }
}
