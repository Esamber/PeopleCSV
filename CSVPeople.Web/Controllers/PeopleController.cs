using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSVPeople.Data;
using Microsoft.Extensions.Configuration;
using CSVPeople.Web.ViewModels;

namespace CSVPeople.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly string _connectionString;
        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("upload")]
        public void Upload(FileUploadViewModel viewModel)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.AddPeopleBase64(viewModel.Base64File);
        }

        [HttpGet]
        [Route("GetPeople")]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }

        [HttpPost]
        [Route("DeletePeople")]
        public void DeletePeople()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeletePeople();
        }

    }
}
