using ChristianBookClub.Domain.Interfaces;
using ChristianBookClub.Domain.Models.Forms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChristianBookClub.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "Admin")]
    [ApiController]
    public class SeminarsController : ControllerBase
    {
        private readonly ISeminarService _seminarService;

        public SeminarsController(ISeminarService seminarService)
        {
            _seminarService = seminarService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] SeminarForm form)
        { 
            _seminarService.Create(form);
            return Created();
        }
    }
}
