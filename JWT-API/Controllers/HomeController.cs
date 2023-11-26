using JWT_API.Model;
using JWT_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public HomeController(ICustomerService _customerService)
        {
            this._customerService = _customerService;
        }
        [HttpGet("get")]
        public IActionResult GetData()
        {
            var result = _customerService.GetData();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("post")]
        public IActionResult PostData(customermodel customermodel)
        {
            var result = _customerService.PostData(customermodel);
            if (result > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("createuser")]
        public IActionResult CreateUser(UserModel userModel)
        {
            var result = _customerService.CreateUser(userModel);   
            if(result>0)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("getuser")]
        public IActionResult GetUsers()
        {
            var result = _customerService.GetUser();
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
