using ContactBE.Models;
using ContactBE.Models.Dtos;

namespace ContactBE.DataAccess
{
    public class ContactDL : IContactDL
    {
        private readonly ContactDatabase _context;

        public ContactDL(ContactDatabase context)
        {
            _context = context;
        }


        public List<ContactData> GetContacts()
        {
            return _context.Contacts.ToList();
        }

        public List<ContactData> GetDefaultContacts()
        {
            var defaultContacts = new DefaultContactsDto();
            var defaultContactsData = defaultContacts.GetDefaultContacts();

            foreach (var contact in defaultContactsData)
            {
                if (!_context.Contacts.Any(c => c.Name == contact.Name && c.Number == contact.Number))
                {
                    _context.Contacts.Add(contact);
                }
            }

            _context.SaveChanges();
            return _context.Contacts.ToList();
        }

        public ContactData GetContactData(Guid contactId)
        {
             return _context.Contacts.Find(contactId);
        }


        public Guid CreateContact(CreateContactDto contactDto)
        {
            var contact = new ContactData {
                Id = Guid.NewGuid(),
                Name = contactDto.Name,
                Number = contactDto.Number,
            };
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return contact.Id;
        }
        public void UpdateContact(ContactData data)
        {
            var existingContact = _context.Contacts.Find(data.Id);
            if (existingContact != null)
            {
                _context.Entry(existingContact).CurrentValues.SetValues(data);
                _context.SaveChanges();
            }

        }

        public void DeleteContact(Guid contactId)
        {
            var contact = _context.Contacts.Find(contactId);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
        }

    }
}
