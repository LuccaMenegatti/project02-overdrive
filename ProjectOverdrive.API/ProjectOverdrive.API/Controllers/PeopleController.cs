using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<People>> SearchPeople()
        {
            return Ok();
        }
    }
}
