using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSVPeople.Data;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CSVPeople.Web.Controllers
{
    public class PeopleFileController : Controller
    {
        private readonly string _connectionString;
        public PeopleFileController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Download(int quantity)
        {
            var repo = new PeopleRepository(_connectionString);
            string people = repo.GeneratePeopleListCsv(quantity);
            var bytes = Encoding.UTF8.GetBytes(people);
            return File(bytes, "text/csv", "people.csv");
        }
    }
}
