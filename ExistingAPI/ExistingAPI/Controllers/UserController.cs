using ExistingAPI.Context;
using ExistingAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExistingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CustomContext context;
        public UserController(CustomContext context)
        {
            this.context = context;
        }
        [HttpPost]
        [Route("add")]
        public IActionResult Register(UserEntity user)
        {
             context.Users.Add(user);
            var userd = context.SaveChanges();
            if (userd != null)
            {
                return Ok(userd);
            }
            else
            {
                return BadRequest("failed");
            }
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            List<UserEntity> users = context.Users.ToList();
            if (users != null && users.Any())
            {
                return Ok(users);
            }
            return BadRequest("No data available");
        }

        [HttpPut]
        [Route("edit/{userId}")]
        public IActionResult Edit(int userId, UserEntity updatedUser)
        {
            var existingUser = context.Users.FirstOrDefault(u => u.UserID == userId);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            // Update user properties
            existingUser.UserName = updatedUser.UserName;
            existingUser.UserEmail = updatedUser.UserEmail;
            existingUser.UserPhone = updatedUser.UserPhone;

            context.SaveChanges();

            // Return the updated user entity
            return Ok(existingUser);
        }

        [HttpDelete]
        [Route("delete/{userId}")]
        public IActionResult Delete(int userId)
        {
            var existingUser = context.Users.FirstOrDefault(u => u.UserID == userId);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            context.Users.Remove(existingUser);
            context.SaveChanges();

            return Ok("User deleted successfully");
        }

    }
}
