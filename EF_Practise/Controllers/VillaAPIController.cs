using EF_Practise.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_Practise.Controllers
{
    [Route("api/villaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public List<Villa> GetVillas()
        {
            return new List<Villa>()
            {
                new Villa { Id = 1, Name = "A" },
                new Villa { Id = 2, Name = "B" },
                new Villa { Id = 3, Name = "C" },
            };
        }
    }
}