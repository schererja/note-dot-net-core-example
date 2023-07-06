using Microsoft.AspNetCore.Mvc;

namespace Eden.API.Controllers
{
    public class UserController : BaseApiController
    {
        public UserController() { }

        [HttpGet]
        [Route("/")]
        public IActionResult UserManagement()
        {
            return Ok();
        }
    }
}
