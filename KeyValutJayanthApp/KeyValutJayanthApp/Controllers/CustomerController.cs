using KeyValutJayanthApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeyValutJayanthApp.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext db;

        
        public CustomerController(DataContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(db.customers.ToList());
        }

        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            db.customers.Add(customer);
            db.SaveChanges();
            return Ok("Customer Added...man!");
        }
    }
}
