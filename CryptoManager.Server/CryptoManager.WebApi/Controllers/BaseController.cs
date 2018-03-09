using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CryptoManager.WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper _mapper;
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}