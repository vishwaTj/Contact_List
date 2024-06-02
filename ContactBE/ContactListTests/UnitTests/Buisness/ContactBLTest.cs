using Moq;
using ContactBE.Buisness;
using ContactBE.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactListTests.UnitTests.MockFolder;
using ContactBE.Models;

namespace ContactListTests.UnitTests.Buisness
{
    public class ContactBLTest
    {
        private Mock<IContactDL> mockContactDL;
        private ContactBL contactBL;  
        private MockContact mock;

        public ContactBLTest()
        {
            mockContactDL = new Mock<IContactDL>();
            contactBL = new ContactBL(mockContactDL.Object);
            mock = new MockContact();
        }

        [Fact]
        public void TestGetContactDatasTestBL()
        {
            //Arrange
             mockContactDL.Setup(x => x.GetContacts()).Returns(mock.contacts);

            //Act
            var result = contactBL.GetContacts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test1",result[0].Name);
        }

        [Fact]
        public void TestGetContactDataBL()
        {
            //Arrange
            var Contact = new ContactData { Id = Guid.NewGuid(), Name = "Sampler", Number = "0147258369" };
            mockContactDL.Setup(x => x.GetContactData(Contact.Id)).Returns(Contact);

            //Act
            var result = contactBL.GetContactData(Contact.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(Contact, result);
        }

        [Fact]
        public void TestCreateContactBL()
        {
            //Arrange
            var id = Guid.NewGuid();
            var contactDto = mock.CreateContactDto();
            mockContactDL.Setup(x => x.CreateContact(contactDto)).Returns(id);

            //Act
            var result = contactBL.CreateContact(contactDto);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(id, result);
        }

        [Fact]
        public void TestUpdateContactBL()
        {
            // Arrange
            var contact = new ContactData
            {
                Id = Guid.NewGuid(),
                Name = "Updated Name",
                Number = "1234567890"
            };

            // Act
            contactBL.UpdateContact(contact);

            // Assert
            mockContactDL.Verify(dl => dl.UpdateContact(It.Is<ContactData>(c =>
                c.Id == contact.Id &&
                c.Name == contact.Name &&
                c.Number == contact.Number
            )), Times.Once);
        }

        [Fact]
        public void TestDeleteContactBL()
        {
            //Arrange
            mockContactDL.Setup(x => x.GetContactData(It.IsAny<Guid>())).Returns(mock.contacts.FirstOrDefault());
            mockContactDL.Setup(x => x.DeleteContact(It.IsAny<Guid>()));

            // Act
            var id = Guid.NewGuid();
            contactBL.DeleteContact(id);

            //Assert
            mockContactDL.Verify(x => x.DeleteContact(id), Times.Once);
        }
    }
}
