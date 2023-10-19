using contactapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace contactapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DataContext db;

        public ContactController(DataContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Contacts.ToList());
        }

        [HttpPost]
        public IActionResult Post(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
            return Ok(new { status = 200, message = "Contact Added Successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var c = db.Contacts.Where(x => x.Id == id).FirstOrDefault();
            if (c != null)
            {
                db.Contacts.Remove(c);
                db.SaveChanges();
                return Ok(new { status = 200, message = "Contact Deleted Successfully" });
            }
            else
            {
                return NotFound(new { status = 404, message = "Contact Not Found" });
            }
        }
    }
}
