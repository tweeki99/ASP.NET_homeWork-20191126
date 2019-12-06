using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20191126.AuthJWT.DTOs;
using _20191126.AuthJWT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _20191126.AuthJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthDTO authDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var token = await authService.Authenticate(authDTO.Login, authDTO.Password);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            return Ok(new { token });


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