using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using University.DataAccess;
using University.Helpers;
using University.Models.DataModels;

namespace University.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UniversityDBContext _context;
        private readonly IStringLocalizer<AccountController> _stringLocalizer;

        public AccountController(JwtSettings jwtSettings, UniversityDBContext context, IStringLocalizer<AccountController> stringLocalize)
        {
            _jwtSettings = jwtSettings;
            _context = context;
            _stringLocalizer = stringLocalize;
        }


        [HttpPost]
        public async Task<IActionResult> GetToken(UserLogins userLogin)
        {
            try
            {
                var welcomeMessage = string.Format(_stringLocalizer.GetString("Welcome"), userLogin.UserName);
                var postName = _stringLocalizer.GetString("Welcome").Value ?? string.Empty;
                var todayIs = _stringLocalizer["Welcome"];

                var Token = new UserTokens();

                var user = await _context.Users.FirstOrDefaultAsync(user => user.Name == userLogin.UserName);

                if (user != null && user.Password == userLogin.Password)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        UserType = user.UserType.ToString(),
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(new
                    {
                    Welcome = welcomeMessage,
                    Token = Token
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> GetUserList()
        {
            return Ok(await _context.Users.ToListAsync());
        }

    }
}
