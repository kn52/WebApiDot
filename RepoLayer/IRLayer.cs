namespace RepoLayer
{
    using EmpLayer;
    using System.Collections.Generic;

    public interface IRLayer
    {
        IEnumerable<Emp> GetAllEmployees();

        string AddEmployee(Emp emp);
        string GenerateJSONWebToken();
        bool ValidateJSONWebToken(string token);
    }
}
