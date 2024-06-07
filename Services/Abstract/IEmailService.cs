using AutoMapper.Internal;
using E_Commerce.Dtos.Email;
using E_Commerce.Model;

namespace E_Commerce.Services.Abstract
{
    public interface IEmailService
    {
        Task<GenericResponse<string>> SendEmailAsync(MailRequest mailRequest);
    }
}
