using Moq;
using ContactBE.Buisness;
using ContactBE.Models;
using ContactBE.Models.Dtos;
using ContactBE.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace ContactListTests.UnitTests.Controller
{
    public class ContactControllerTest
    {
        private Mock<IContactBL> mockContactBL;
        private ContactController contactController;

        public ContactControllerTest()
        {
            mockContactBL = new Mock<IContactBL>();
            contactController = new ContactController(mockContactBL.Object);
        }

        [Fact]
        public void GetContacts_ShouldReturnListOfContacts()
        {
            // Arrange
            var mockContactDataList = new List<ContactData>
            {
                new ContactData { Id = Guid.NewGuid(), Name = "Test Name 1", Number = "0000000001" },
                new ContactData { Id = Guid.NewGuid(), Name = "Test Name 2", Number = "0000000002" }
            };

            mockContactBL.Setup(bl => bl.GetContacts()).Returns(mockContactDataList);

            // Act
            var result = contactController.GetContacts();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ContactData>>(result);
            Assert.Equal(mockContactDataList.Count, result.Count);
        }

        [Fact]
        public void GetContactData_ShouldReturnContactData()
        {
            // Arrange
            var contactId = Guid.NewGuid();
            var mockContactData = new ContactData { Id = contactId, Name = "Test Name", Number = "0000000000" };

            mockContactBL.Setup(bl => bl.GetContactData(contactId)).Returns(mockContactData);

            // Act
            var result = contactController.GetContactData(contactId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ContactData>(result);
            Assert.Equal(contactId, result.Id);
        }

        [Fact]
        public void CreateContact_ShouldReturnCreatedContactId()
        {
            // Arrange
            var contactDto = new CreateContactDto { Name = "Test Name", Number = "0000000000" };
            var newContactId = Guid.NewGuid();

            mockContactBL.Setup(bl => bl.CreateContact(contactDto)).Returns(newContactId);

            // Act
            var result = contactController.CreateContact(contactDto);

            // Assert
            Assert.NotNull(result);
            var actionResult = Assert.IsType<ActionResult<Guid>>(result);
            var createdResult = Assert.IsType<Guid>(actionResult.Value);
            Assert.Equal(newContactId, createdResult);
        }

        [Fact]
        public void UpdateContact_ShouldCallUpdateContactInBL()
        {
            // Arrange
            var contact = new ContactData { Id = Guid.NewGuid(), Name = "Updated Name", Number = "1234567890" };

            // Act
            contactController.UpdateContact(contact);

            // Assert
            mockContactBL.Verify(bl => bl.UpdateContact(contact), Times.Once);
        }

        [Fact]
        public void DeleteContact_ShouldCallDeleteContactInBL()
        {
            // Arrange
            var contactId = Guid.NewGuid();

            // Act
            contactController.DeleteContact(contactId);

            // Assert
            mockContactBL.Verify(bl => bl.DeleteContact(contactId), Times.Once);
        }
    }
}
