using ContactBE.Buisness;
using ContactBE.Models;
using ContactBE.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ContactBE.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ContactController : ControllerBase
    {
 

        private readonly IContactBL _contactBL;

        public ContactController(IContactBL contactBL)
        {
            _contactBL = contactBL;
        }

        [HttpGet]
        // template of swagger
        public List<ContactData> GetContacts() => _contactBL.GetContactDatas();

        [HttpGet("{id}")]
        public ContactData GetContactData(Guid id) => _contactBL.GetContactData(id);

        [HttpPost]
        public ActionResult<Guid> CreateContact(CreateContactDto contactDto) {
            return _contactBL.AddNewContact(contactDto);
        }

        [HttpPut]
        public void UpdateContact(ContactData contact)
        {
            _contactBL.UpdateContact(contact);
        }

        [HttpDelete("{id}")]
        public void DeleteContact(Guid id)
        {
            _contactBL.DeleteContact(id);
        }
    }
}
