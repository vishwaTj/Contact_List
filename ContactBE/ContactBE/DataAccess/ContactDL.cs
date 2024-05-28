using ContactBE.Models;
using ContactBE.Models.Dtos;

namespace ContactBE.DataAccess
{
    public class ContactDL : IContactDL
    {
        private List<ContactData> contacts = new List<ContactData>();

        public List<ContactData> GetContacts()
        {
            return contacts;
        }

        public ContactData GetContactData(Guid contactId)
        {
            return contacts.Find(existingItem => existingItem.Id == contactId);
        }

        public Guid AddNewContact(CreateContactDto contactDto)
        {
            var contact = new ContactData {
                Id = Guid.NewGuid(),
                Name = contactDto.Name,
                Number = contactDto.Number,
            };
            contacts.Add(contact);
            return contact.Id;
        }
        public void UpdateContact(ContactData data)
        {
            var contact = contacts.FirstOrDefault(c => c.Id == data.Id);
            if (contact != null)
            {
                contact.Name = data.Name;
                contact.Number = data.Number;
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }

        }

        public void DeleteContact(Guid contactId)
        {
            contacts.RemoveAt(contacts.FindIndex(existingItem => existingItem.Id == contactId));
        }

    }
}
