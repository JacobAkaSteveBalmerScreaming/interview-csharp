using ApiCommunication;
using ConfigClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/v1/people")]
    public class PeopleController : ApiBase
    {
        private readonly FakeDatabase _db;
        public PeopleController(IApiCall apiCall, IConfiguration config, IConfigProvider configProvider) : base(apiCall, config, configProvider)
        {
            _db = new FakeDatabase();
        }

        [HttpPost]
        public object AddPerson([FromBody] object personData)
        {
            return personData;
        }

    }
}
