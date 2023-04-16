using GMPP.MainApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GMPP.MainApi.Controllers
{
    [Route("api/responsd")]
    [ApiController]
    public class RespondController : ControllerBase
    {
        public RespondController()
        {
            
        }

        [HttpPost]
        public ResponseDto ResponsdInProject()
        {


            return new ResponseDto();
        }
    }
}
