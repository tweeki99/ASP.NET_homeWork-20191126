using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20191126.AuthJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _20191126.AuthJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegController : ControllerBase
    {
        private readonly DataAccess.AppContext context;

        public RegController(DataAccess.AppContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var existingUser = await context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);

            if (existingUser != null)
                return BadRequest();

            context.Users.Add(user);

            await context.SaveChangesAsync();

            return Ok(new { message = "Пользователь успешно зареган" });


            /*
             * 1. Принимаем в параметрах объект с данными пользователя (DTO)        +
             * 2. Обращаемся к сервису, который проводит аутентификацию             +
             * 3. Получаем от сервиса токен                                         +
             *      а) Если токен пуст - кидаем 401 ошибку                          +
             *      б) Если всё нормально - возвращаем клиенту токен в объекте      +
             */
        }
    }
}