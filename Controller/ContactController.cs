using contactapi.Models;
using ContactsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _contactService.GetByIdAsync(id);
            if (contact == null)
                return NotFound(new { Message = $"Contact with ID {id} not found." });

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check for unique email
            var existing = (await _contactService.GetAllAsync()).FirstOrDefault(c => c.Email == contact.Email);
            if (existing != null)
                return BadRequest(new { Message = "A contact with this email already exists." });

            var createdContact = await _contactService.CreateAsync(contact);
            return CreatedAtAction(nameof(GetById), new { id = createdContact.ID }, createdContact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _contactService.UpdateAsync(id, contact);
            if (!success)
                return NotFound(new { Message = $"Contact with ID {id} not found." });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _contactService.DeleteAsync(id);
            if (!success)
                return NotFound(new { Message = $"Contact with ID {id} not found." });

            return NoContent();
        }
    }
}
