using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Week14Practice2_Jwt.Jwt;
using Week14Practice2_Jwt.Models;
using Week14Practice2_Jwt.Services;

namespace Week14Practice2_Jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDataProtector _dataProtector;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IDataProtector dataProtector, IConfiguration configuration)
        {
            _userService = _userService;
            _dataProtector = dataProtector.CreateProtector("security");
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            // Şifreyi IDataProtector ile şifreleyelim (encrypt)
            string encryptedPassword = _dataProtector.Protect(request.Password);

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                Password = encryptedPassword,  // Şifrelenmiş parola
            };

            var result = await _userService.AddUser(addUserDto);

            if (result.IsSucced)
                return Ok(result);
            else
                return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var loginUserDto = new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _userService.LoginUser(loginUserDto);
            //resultun IsSucces i false ise giriş yapılamaz
            if (!result.IsSucceed)
                return BadRequest(result.Message);

            //result false gelmediyse kullanıcın adını emailini ve tipini çekerim
            var user = result.Data;

            //property injection ile çekildi
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwt(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"])

            });

            return Ok(new LoginResponse
            {
                Message = "Giriş Başarıyla tamamlandı",
                Token = token
            });
        }

        //oturumu açık olmayanıın buna istek atamamasını sağlıyorum
        [HttpGet]
        [Authorize(Roles = "Admin")]     //admin olan istek atabilsin
        public IActionResult Test()
        {
            return Ok("Test end- point cevabı");
        }
        //401 hatası :oturum açık değil veya yetkin yok
        //403 hatası :oturum açık ama yetkin yok

    }


}

