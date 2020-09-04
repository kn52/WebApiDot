namespace DataLayer
{
    using EmpLayer;
    using RepoLayer;
    using System.Collections.Generic;
    public class DLayer : IDLayer
    {
        public DLayer(IRLayer ops)
        {
            this.Repository = ops;
        }

        public IRLayer Repository { get; set; }

        public IEnumerable<Emp> GetAllEmployees()
        {
            return this.Repository.GetAllEmployees();
        }

        public string AddEmployee(Emp emp)
        {
            return "";        
        }

        public string GenerateJSONWebToken()
        {
            return Repository.GenerateJSONWebToken();
        }

        public bool ValidateJSONWebToken(string token)
        {
            return Repository.ValidateJSONWebToken(token);
        }
    }
}
