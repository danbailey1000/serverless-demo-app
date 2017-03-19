using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactRepo _mockContactRepo = new MockContactRepo();

        // GET api/contacts
        [HttpGet]
        public IEnumerable<ContactData> Get()
        {
            return _mockContactRepo.Get();
        }

        // GET api/contacts/5
        [HttpGet("{id}")]
        public ContactData Get(int id)
        {
            return _mockContactRepo.Get(id);
        }

        // POST api/contacts
        [HttpPost]
        public void Post([FromBody] ContactData contactData)
        {
            _mockContactRepo.Add(contactData);
        }

        // PUT api/contacts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ContactData contactData)
        {
            _mockContactRepo.Update(id, contactData);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _mockContactRepo.Delete(id);
        }
    }
}
