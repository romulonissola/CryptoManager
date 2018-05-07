using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace CryptoManager.WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper _mapper;
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected Guid GetUserId()
        {
            return Guid.Parse(User.FindFirstValue("Id"));
        }
    }
}