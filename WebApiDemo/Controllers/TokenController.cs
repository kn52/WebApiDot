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
            return GenerateJSONWebToken();
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
            return ValidateJSONWebToken(token);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public static string GenerateJSONWebToken(IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static bool ValidateJSONWebToken(string token, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(configuration);
            try
            {
                SecurityToken validatedToken = tokenHandler.ReadToken(token);
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (Exception ex)
            {

                return false;
            }
            return true;

        }

        private static TokenValidationParameters GetValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
            };
        }
    }
}