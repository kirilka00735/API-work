using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contracts.User;
using WebApplication1.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;





namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        public ExoticPlacesContext Context { get; }
        public UsersController(ExoticPlacesContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Получить информацию о всех пользователях
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = Context.Users.ToList();
            return Ok(users);
        }
        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     
        ///     {
        ///        "userName": "string",
        ///        "email": "string",     
        ///        "pass": "string",
        ///     }
        ///
        /// </remarks>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest user)
        {
            var userDto = user.Adapt<User>();
            Context.Users.Add(userDto);
            Context.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// Получить информацию о отзове по номеру ID
        /// </summary>
        // POST api/<UsersController>
        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            User? user = Context.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Not found");
            }
            return Ok(user);
        }
        /// <summary>
        /// Изменение данных пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     {
        ///        "userName": "string",
        ///        "email": "string",
        ///        "pass": "string",
        ///     }
        ///
        /// </remarks>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>

        // POST api/<UsersController>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserUpdateRequest update)
        {
            var user = Context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            user = update.Adapt(user);
            Context.SaveChanges();
            return Ok("User updated successfully");
        }
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            User? user = Context.Users.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Not found");
            }
            Context.Users.Remove(user);
            Context.SaveChanges();
            return Ok();
        }
    }
}
