using Microsoft.AspNetCore.Mvc;
using E_Commerce.Dtos.Email;
using E_Commerce.Dtos.User;
using E_Commerce.Model;

namespace E_Commerce.Services.Abstract
{
    public interface IAuthService
    {
         Task<string> ProtectedRoute();
         Task<GenericResponse<RegisterResponseDto>> RegisterUser(RegisterDto registerDto);
         Task<GenericResponse<LoginResponseDto>> LoginUser(LoginDto loginDto);
         Task<GenericResponse<string>> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<GenericResponse<string>> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
