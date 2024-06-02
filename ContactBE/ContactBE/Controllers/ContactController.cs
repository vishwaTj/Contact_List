using ContactBE.Buisness;
using ContactBE.Models;
using ContactBE.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContactBE.Controllers
{
    [ApiController]
    [Route("v1/contacts")]
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
        [SwaggerOperation(Summary = "Get Appointments")]
        public List<ContactData> GetContacts() => _contactBL.GetContacts();

        ///<summary>
        ///Fetch Default Contancts
        ///</summary>
        ///<returns> appends a list of default contacts to existing ones </returns>
        [HttpGet("default")]
        [SwaggerOperation(Summary = "Get Appointments")]
        public List<ContactData> GetDefaultContacts() => _contactBL.GetDefaultContacts();

        /// <summary>
        /// Returns a single Contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns Contact data for the given Id</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get an Appointment")]
        public ContactData GetContactData(Guid id) => _contactBL.GetContactData(id);

        /// <summary>
        /// Create a Contact
        /// </summary>
        /// <param name="contactDto"></param>
        /// <returns>Returns Guid of the successfully created contact</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Create an Appointment")]
        public ActionResult<Guid> CreateContact(CreateContactDto contactDto) {
            return _contactBL.CreateContact(contactDto);
        }

        /// <summary>
        /// Modify a Contact
        /// </summary>
        /// <param name="contact"></param>
        [HttpPut]
        [SwaggerOperation(Summary = "Update Appointment")]
        public void UpdateContact(ContactData contact)
        {
            _contactBL.UpdateContact(contact);
        }

        /// <summary>
        /// Delete a Contact
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an Appointment")]
        public void DeleteContact(Guid id)
        {
            _contactBL.DeleteContact(id);
        }
    }
}
