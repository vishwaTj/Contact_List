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

        /// <summary>
        /// Get the List of Contacts 
        /// </summary>
        /// <returns>List of Contacts</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ContactData>), StatusCodes.Status200OK)]
        public List<ContactData> GetContacts() => _contactBL.GetContactDatas();

        /// <summary>
        /// Returns a single Contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Contact data for the given Id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactData), StatusCodes.Status200OK)]
        public ContactData GetContactData(Guid id) => _contactBL.GetContactData(id);

        /// <summary>
        /// Create a Contact
        /// </summary>
        /// <param name="contactDto"></param>
        /// <returns>Returns Guid of the successfully created contact</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public ActionResult<Guid> CreateContact(CreateContactDto contactDto) {
            return _contactBL.AddNewContact(contactDto);
        }

        /// <summary>
        /// Modify a Contact
        /// </summary>
        /// <param name="contact"></param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public void UpdateContact(ContactData contact)
        {
            _contactBL.UpdateContact(contact);
        }

        /// <summary>
        /// Delete a Contact
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public void DeleteContact(Guid id)
        {
            _contactBL.DeleteContact(id);
        }
    }
}
