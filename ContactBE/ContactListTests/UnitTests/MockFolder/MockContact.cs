using ContactBE.Models;
using ContactBE.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactListTests.UnitTests.MockFolder
{
    public class MockContact
    {
        public List<ContactData> contacts = new List<ContactData>
        {
            new ContactData
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                Number = "0000000000",

            },
            new ContactData
            {
                Id = Guid.NewGuid(),
                Name = "Test2",
                Number = "0000000001",

            }
        };

        public CreateContactDto CreateContactDto() => new CreateContactDto
        {
            Name = "Test",
            Number = "0000000000"
        };
    }
}
