using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Dtos.Email;
using E_Commerce.Dtos.User;
using E_Commerce.Model;
using E_Commerce.Services.Abstract;

namespace E_Commerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public HomeController(IAuthService authService, IEmailService emailService)
        {

            _authService = authService;
            _emailService = emailService;
        }

        #region protectedRoute
        [HttpGet]
        [Authorize(Policy = "Uchiha")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _authService.ProtectedRoute());
        }
        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<ActionResult<GenericResponse<LoginResponseDto>>> LoginUser(LoginDto loginDto)
        {
            return Ok(await _authService.LoginUser(loginDto));
        }
        #endregion

        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult<GenericResponse<RegisterResponseDto>>> RegisterUser(RegisterDto registerDto)
        {
            return Ok(await _authService.RegisterUser(registerDto));
        }
        #endregion

        #region ForgotPassword
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<GenericResponse<string>>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            return Ok(await _authService.ForgotPassword(forgotPasswordDto));
        }
        #endregion

        #region Reset
        [HttpPost("Reset")]
        public async Task<ActionResult<GenericResponse<string>>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            return Ok(await _authService.ResetPassword(resetPasswordDto));
        }
        #endregion

        #region sendMail
        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                return Ok(await _emailService.SendEmailAsync(request));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}

