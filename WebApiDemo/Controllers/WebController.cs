using System.Collections.Generic;
using DataLayer;
using EmpLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebController : ControllerBase
    {
        public WebController(IDLayer ops) 
        {
            this.Service = ops;
        }

        public IDLayer Service { get; set; }

        // GET api/values
        [HttpGet]
        public IEnumerable<Emp> Get()
        {
            return this.Service.GetAllEmployees();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
