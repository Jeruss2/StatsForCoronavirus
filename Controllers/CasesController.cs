using CoronavirusModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StatsForCoronavirus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : Controller
    {
        [HttpGet]

        public IEnumerable<Case> Get()
        {
            SqlConnection connection =
                new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Josh;Integrated Security=SSPI;");
            connection.Open();

            //Connect();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT TotalCases, TotalDeaths, TotalRecoveries, DateAdded FROM Cases";

            cmd.Connection = connection;

            SqlDataReader reader = cmd.ExecuteReader();

            List<Case> cases = new List<Case>();

            while (reader.Read())
            {
                Case cCase = new Case();
                cCase.TotalCases = reader.GetString(0);
                cCase.TotalDeaths = reader.GetString(1);
                cCase.TotalRecoveries = reader.GetString(2);
                cCase.DateAdded = reader.GetDateTime(3);
                // might use int.Parse instead?
                cCase.DateAdded.ToString("MM/dd/yyyy");
                cases.Add(cCase);

            }

            return cases;
        }

        // GET: api/Listings/5
        [HttpGet("{TotalCases}", Name = "Get")]
        public List<Case> Get(int TotalCases)
        {
            try
            {
                List<Case> cases = new List<Case>();

                using (SqlConnection connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Josh;Integrated Security=SSPI;"))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT TotalCases, TotalDeaths, TotalRecoveries FROM Cases WHERE TotalCases = @TotalCases";
                        cmd.Parameters.AddWithValue("@TotalCases", TotalCases);
                        cmd.Connection = connection;
                        connection.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Case cCase = new Case();
                            cCase.TotalCases = reader.GetString(0);
                            cCase.TotalDeaths = reader.GetString(1);
                            cCase.TotalRecoveries = reader.GetString(2);
                            cCase.DateAdded = reader.GetDateTime(3);
                            // might use int.Parse instead?

                            cases.Add(cCase);
                        }

                        return cases;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // POST: api/Listings
        [HttpPost]
        public void AddListing([FromBody] Case cases)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Josh;Integrated Security=SSPI;");
            connection.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "insert into Cases\r\nvalues (@TotalCases, @TotalDeaths, @TotalRecoveries, @DateAdded)";

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@TotalCases";
            param.Value = cases.TotalCases;

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@TotalDeaths";
            param1.Value = cases.TotalDeaths;

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@TotalRecoveries";
            param2.Value = cases.TotalRecoveries;

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName = "@DateAdded";
            param3.Value = cases.DateAdded;



            cmd.Parameters.Add(param);
            cmd.Parameters.Add(param1);
            cmd.Parameters.Add(param2);
            cmd.Parameters.Add(param3);

            cmd.Connection = connection;

            cmd.ExecuteNonQuery();
        }

        // PUT: api/Listings/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}