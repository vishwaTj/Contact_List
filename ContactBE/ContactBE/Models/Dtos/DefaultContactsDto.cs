using ContactBE.Buisness;

namespace ContactBE.Models.Dtos
{
    public class DefaultContactsDto
    {
        List<ContactData> defaultContacts = new List<ContactData>()
            {
              new ContactData
              {
                    Id = Guid.NewGuid(),
                    Name = "Police",
                    Number = "100"
               },
              new ContactData
              {
                   Id = Guid.NewGuid(),
                   Name = "Ambulance",
                   Number = "108"
              },
              new ContactData
              {
                   Id = Guid.NewGuid(),
                   Name = "FireStation",
                   Number = "102"
               },
              new ContactData
              {
                   Id = Guid.NewGuid(),
                   Name = "Mechanic",
                   Number = "9852364170"
              },
              new ContactData
              {
                   Id = Guid.NewGuid(),
                   Name = "Electrician",
                   Number = "7123946580"
               },
              new ContactData
              {
                   Id = Guid.NewGuid(),
                   Name = "Rakesh",
                   Number = "7123946560"
              },
              new ContactData
              {
                   Id = Guid.NewGuid(),
                   Name = "Ramesh",
                   Number = "7123046560"
               },
              new ContactData
              {
                   Id = Guid.NewGuid(),
                   Name = "Suresh",
                   Number = "7424946560"
              },
              new ContactData
              {
                   Id = Guid.NewGuid(),
                   Name = "Lokesh",
                   Number = "7193946560"
              },
              new ContactData
              {
                   Id = Guid.NewGuid(),
                   Name = "Mukesh",
                   Number = "8193946560"
              }

            };
        public List<ContactData> GetDefaultContacts()
        {
            return defaultContacts;
        }
    }
}
