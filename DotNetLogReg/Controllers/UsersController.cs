using DotNetLogReg.Filters;
using DotNetLogReg.Models;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetLogReg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SimpleLogActionFilter]
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

        //Here we upadate the existing data

        [HttpPut("{id}")]
        public IActionResult UpadateUser(int id, [FromBody] UserDTO userDTO)
        {
            //chheck if the user exist
            var existinguser = _dbContext.Users.Find(id);
            if (existinguser == null)
            {
                return NotFound("User Not Found");
            }

            //Update the properties
            existinguser.FirstName = userDTO.FirstName;
            existinguser.lastName = userDTO.lastName;
            existinguser.Email = userDTO.Email;
            existinguser.Password = userDTO.Password;
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Handle Concurrency isues if the record was updated by another processs
                if (!_dbContext.Users.Any(e => e.UserId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent(); //4 No content 

        }

        //Here we delete the data or records 
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _dbContext.Users.Find(id);
            if(user== null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return NoContent();
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
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null)
                return Ok(user);

            else
                return NoContent();
        }

        //The exception Filter

        [HttpPost]
        [Route("Divide")]
        [CalculatorExceptionFilter]
        public IActionResult Divide(int x, int y)
        {
            return Ok(x / y);
        }

        [HttpGet]
        [SimpleResultFilter]
        [Route("ResultFilter")]
        public IActionResult Get()
        {
            var data = new { FirstName = "sangram", LastName = "katare" };
            return Ok(data);
        }



        [HttpGet]
        [Route("Status Code")]
        public IActionResult Gets(string firstName, int age)
        {
            var data = new { FirstName = firstName, Age = age };
            return Ok(data);
        }

    }
}
