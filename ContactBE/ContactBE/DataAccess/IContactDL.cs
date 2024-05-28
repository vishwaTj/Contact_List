﻿using ContactBE.Models;
using ContactBE.Models.Dtos;

namespace ContactBE.DataAccess
{
    public interface IContactDL
    {
        //Function returns a list of contacts
        public List<ContactData> GetContacts();

        //Function returns a specific contact
        public ContactData GetContactData(Guid id);

        //Function Adds a new contact
        public Guid AddNewContact(CreateContactDto contactDto);

        //Function Updates an existing contact
        public void UpdateContact(ContactData contact);

        //Function Deletes an existing contact
        public void DeleteContact(Guid contactId);

    }
}
