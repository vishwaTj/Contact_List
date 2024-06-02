using ContactBE.DataAccess;
using ContactBE.Models;
using ContactBE.Models.Dtos;
using ContactListTests.UnitTests.MockFolder;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System;

namespace ContactListTests.UnitTests.DataAccess
{
    public class ContactDLTest
    {
        private ContactDL contactDL;
        private MockContact mockContact;
        private DbContextOptions<ContactDatabase> _options;

        public ContactDLTest()
        {
            _options = new DbContextOptionsBuilder<ContactDatabase>()
                .UseInMemoryDatabase(databaseName: "TestContactDatabase")
                .Options;

            using (var context = new ContactDatabase(_options))
            {
                context.Database.EnsureCreated();
            }

            var contextInstance = new ContactDatabase(_options);
            contactDL = new ContactDL(contextInstance);
            mockContact = new MockContact();
        }

        [Fact]
        public void GetContactDatasTest()
        {
            // Arrange: Add a contact to the in-memory database
            using (var context = new ContactDatabase(_options))
            {
                context.Contacts.Add(new ContactData
                {
                    Id = Guid.NewGuid(),
                    Name = "John Doe",
                    Number = "1234567890"
                });
                context.SaveChanges();
            }

            // Act: Get contacts list
            var result = contactDL.GetContacts();

            // Assert: Check if the result is not null and contains the added contact
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void GetContactDataTest()
        {
            // Arrange: Create and add a contact
            var contactDto = mockContact.CreateContactDto();
            var resultId = contactDL.CreateContact(contactDto);

            // Act: Retrieve the contact
            var contact = contactDL.GetContactData(resultId);

            // Assert: Check if the data matches
            Assert.Equal(contact.Name, contactDto.Name);
            Assert.Equal(contact.Number, contactDto.Number);
        }

        [Fact]
        public void CreateContactDataTest()
        {
            // Arrange: Create contact DTO
            var contactDto = mockContact.CreateContactDto();

            // Act: Add new contact
            var result = contactDL.CreateContact(contactDto);

            // Assert: Check the return type and value
            Assert.NotNull(result);
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public void DeleteContactDataTest()
        {
            // Arrange: Create and add a contact
            var contactDto = mockContact.CreateContactDto();
            var resultId = contactDL.CreateContact(contactDto);

            // Act: Delete the contact
            contactDL.DeleteContact(resultId);

            // Assert: Check if the contact exists
            var contact = contactDL.GetContactData(resultId);
            Assert.Null(contact);
        }

        [Fact]
        public void UpdateContactData()
        {
            // Arrange: Create and add a contact
            var contactDto = mockContact.CreateContactDto();
            var resultId = contactDL.CreateContact(contactDto);
            var newContactData = new ContactData { Id = resultId, Name = "Updated", Number = "00000" };

            // Act: Update contact
            contactDL.UpdateContact(newContactData);

            // Assert: Retrieve and check the updated contact data
            var contact = contactDL.GetContactData(resultId);
            Assert.Equal(contact.Name, newContactData.Name);
            Assert.Equal(contact.Number, newContactData.Number);
        }

        [Fact]
        public void UpdateContactDataNotFound()
        {
            // Arrange: Create and add a contact
            var contactDto = mockContact.CreateContactDto();
            var resultId = contactDL.CreateContact(contactDto);
            var newContactData = new ContactData { Id = Guid.NewGuid(), Name = "Updated", Number = "00000" };

            // Act: Update contact with a new ID
            contactDL.UpdateContact(newContactData);

            // Assert: Check if the contact data is null
            var contact = contactDL.GetContactData(newContactData.Id);
            Assert.Null(contact);
        }

    }
}
