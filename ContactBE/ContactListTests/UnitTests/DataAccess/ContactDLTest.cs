using ContactBE.DataAccess;
using ContactBE.Models;
using ContactBE.Models.Dtos;
using ContactListTests.UnitTests.MockFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactListTests.UnitTests.DataAccess
{
    public class ContactDLTest
    {
        private ContactDL contactDL;
        private MockContact mockContact;
        
        
        public ContactDLTest()
        {
            contactDL = new ContactDL();
            mockContact = new MockContact();
        } 
        
            
        [Fact]
        public void GetContactDatasTest()
        {
            // Get contacts list
            var result = contactDL.GetContactDatas();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetContactDataTest()
        {
            // create contact
            var contactDto = mockContact.CreateContactDto();
            var result = contactDL.AddNewContact(contactDto);

            var contact = contactDL.GetContactData(result);

            //Assert check if the data matches
            Assert.Equal(contact.Name, contactDto.Name);
            Assert.Equal(contact.Number, contactDto.Number);
        }

        [Fact]
        public void CreateContactDataTest()
        {
            // create contact
            var contactDto = mockContact.CreateContactDto();
            var result = contactDL.AddNewContact(contactDto);
            
            // Assert check the return type
            Assert.NotNull(result);
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public void DeleteContactDataTest()
        {
            // create a contact
            var contactDto = mockContact.CreateContactDto();
            var result = contactDL.AddNewContact(contactDto);

            // delete the contact
            contactDL.DeleteContact(result);

            //Assert check if contact exists
            var contact = contactDL.GetContactData(result);
            Assert.Null(contact);
        }

        [Fact]
        public void UpdateContactData()
        {
            // create a contact
            var contactDto = mockContact.CreateContactDto();
            var result = contactDL.AddNewContact(contactDto);
            var newContactData = new ContactData { Id = result, Name = "Updated", Number = "00000" };


            // update contact
            contactDL.UpdateContact(newContactData);

            // Assert get the updated contact data
            var contact = contactDL.GetContactData(result);
            Assert.Equal(contact.Name, newContactData.Name);
            Assert.Equal(contact.Number, newContactData.Number);
        }

        [Fact]
        public void UpdateContactDataNotFound()
        {
            // create a contact
            var contactDto = mockContact.CreateContactDto();
            var result = contactDL.AddNewContact(contactDto);
            var newContactData = new ContactData { Id = Guid.NewGuid(), Name = "Updated", Number = "00000" };


            // update contact
            contactDL.UpdateContact(newContactData);

            // Assert get the updated contact data
            var contact = contactDL.GetContactData(newContactData.Id);
            Assert.Null(contact);
        }
    }
}
