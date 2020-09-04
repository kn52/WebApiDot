namespace RepoLayer
{
    using EmpLayer;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class RLayer : IRLayer
    {
        private readonly string db;

        private readonly IConfiguration Configuration;
        public RLayer(IConfiguration configuration)
        {
            this.Configuration = configuration;
            db = this.Configuration.GetSection("ConnectionString").GetSection("DBConnection").Value;
        }

        public IEnumerable<Emp> GetAllEmployees()
        {
            List<Emp> lstemployee = new List<Emp>();
            using (SqlConnection con = new SqlConnection(db))
            {
                using (SqlCommand cmd = new SqlCommand("spGetAllEmployees", con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    try
                    {
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Emp employee = new Emp                               {
                                    ID = Convert.ToInt32(rdr["Id"]),
                                    FirstName = rdr["FName"].ToString(),
                                    LastName = rdr["LName"].ToString(),
                                    Email = rdr["Email"].ToString(),
                                    Password = rdr["Password"].ToString(),
                                    PhoneNumber = rdr["PhoneNumber"].ToString()
                                };
                                lstemployee.Add(employee);
                            }
                            return lstemployee;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                        return lstemployee;
                    }
                }
            }
            return lstemployee;
        }

        public string AddEmployee(Emp emp)
        {
            using (SqlConnection conn = new SqlConnection(db))
            {
                var keyNew = SaltGenerator.GeneratePassword(10);
                emp.Password = SaltGenerator.EncodePassword(emp.Password, keyNew);
                using (SqlCommand cmd = new SqlCommand("spAddEmployee", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@FName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LName", emp.LastName);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Password", emp.Password);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        string id = cmd.Parameters["@id"].Value.ToString();
                        if (id != null)
                        {
                            return id.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return null;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public string GenerateJSONWebToken()
        {
            return TokenGenerator.GenerateJSONWebToken(Configuration);
        }

        public bool ValidateJSONWebToken(string token)
        {
            return TokenGenerator.ValidateJSONWebToken(token,Configuration);
        }
    }
}
