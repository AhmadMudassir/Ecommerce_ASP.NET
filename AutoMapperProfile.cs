
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using E_Commerce.Dtos.User;
using E_Commerce.Model;

namespace E_Commerce
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<User, RegisterDto>();
        }
    }
}
