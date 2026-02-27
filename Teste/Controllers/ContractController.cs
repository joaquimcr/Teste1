using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teste.Data;
using Teste.Model;
using Teste.ViewModel;

namespace Teste.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class ContractController : ControllerBase
    {
        [HttpGet]
        [Route(template: "contacts")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var contacts = await context.Contacts.AsNoTracking().ToListAsync();
            return Ok(contacts);

        }


        [HttpGet]
        [Route(template: "contact/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, int id)
        {
            var contact = await context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return contact == null ? NotFound():Ok(contact);

        }

        [HttpPost(template :"contacts")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context,
            [FromBody] CreateContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var contactnew = new Contact
            {
                Email = contactViewModel.Email,
                Name = contactViewModel.Name,
                Phone = contactViewModel.Phone,
            };
            try
            {
                await context.Contacts.AddAsync(contactnew);
                await context.SaveChangesAsync();
                return Created(uri: $"v1/todos/{contactnew.Id}", contactnew);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut(template: "contact/{id}")]    
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context,
             [FromBody] CreateContactViewModel contactViewModel, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            var contactUp = await context.Contacts.FirstOrDefaultAsync(p => p.Id == id);
            if (contactUp == null) return NotFound();
            try
            {
                contactUp.Email = contactViewModel.Email;
                contactUp.Name = contactViewModel.Name;
                contactUp.Phone = contactViewModel.Phone;


                context.Contacts.Update(contactUp);
                await context.SaveChangesAsync();

                return Ok(contactUp);
            
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpDelete(template: "contacts/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context,
             [FromRoute] int id)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(p=>p.Id == id);
            if (contact == null) return NotFound();
            try
            {
                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

    }





}
