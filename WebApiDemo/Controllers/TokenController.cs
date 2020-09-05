namespace WebApiDemo
{
    using DataLayer;
    using EmpLayer;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        public TokenController(IDLayer ops)
        {
            this.Service = ops;
        }

        public IDLayer Service { get; set; }
        // GET: api/<controller>
        [HttpGet]
        public string GetToken()
        {
            return Service.GenerateJSONWebToken();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Emp emp)
        {
            
        }

        // PUT api/<controller>/5
        [HttpPut]
        public bool ValidateToken([FromBody]string token)
        {
            return Service.ValidateJSONWebToken(token);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }        
    }
}