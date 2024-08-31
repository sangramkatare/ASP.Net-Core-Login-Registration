using DotNetLogReg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetLogReg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MydbContext _dbContext;
        public UsersController(MydbContext mydbContext)
        {
            _dbContext = mydbContext;

        }
        //Here we add data in the database from endpoints
        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == userDTO.Email);
            if (user == null)
            {
                _dbContext.Users.Add(new User
                {
                    FirstName = userDTO.FirstName,
                    lastName = userDTO.lastName,
                    Email = userDTO.Email,
                    Password = userDTO.Password
                });
                _dbContext.SaveChanges();
                return Ok("User Registration Successfully");
            }
            else
            {
                return BadRequest("User allredy Exist, with Same EMail address. ");

            }
        }

        //Here we create Login for all who are allready exist
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

     
        //here get all user 
        [HttpGet]
        [Route("Get all User")]
        public IActionResult GetUsers()
        {
            return Ok(_dbContext.Users.ToList());
        } 

       
        //Here get user by specific condition
        [HttpGet]
        [Route("Get Specific User")]
        public IActionResult GetUser(int id) 
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId==id);
            if (user != null)
                return Ok(user);

            else
                return NoContent();
        }

    }
}
