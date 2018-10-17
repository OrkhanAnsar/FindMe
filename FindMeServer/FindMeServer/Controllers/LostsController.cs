using ApplicationCore.Models;
using ApplicationCore.NotificationConfig;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FindMeServer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class LostsController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IDataService dataService;
        private readonly ISubscribeService subscribeService;

        public LostsController(IDataService dataService, IAuthenticationService authenticationService, ISubscribeService subscribeService)
        {
            this.dataService = dataService;
            this.authenticationService = authenticationService;
            this.subscribeService = subscribeService;
        }

        [HttpGet("/api/[controller]/getlosts")]
        public async Task<ActionResult> GetLosts()
        {
            var losts = await this.dataService.GetLosts();
            if (Enumerable.Count(losts) > 0)
                return Json(losts);
            else if (losts is null)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            else
                return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpPut("/api/[controller]/editlost")]
        public async Task<ActionResult> EditLost([FromBody]Lost lost)
        {
            var result = await this.dataService.EditLost(lost);
            if (result == true)
                return StatusCode((int)HttpStatusCode.OK);
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("/api/[controller]/getlostsbyinstitution/{id}")]
        public async Task<ActionResult> GetLostsByInstitution(int id)
        {
            var result = await this.dataService.GetLostsByInstitution(id);
            if (result != null)
                return Json(result);
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpDelete("/api/[controller]/deletelost/{id}")]
        public async Task<ActionResult> DeleteLost(int id)
        {
            var result = await this.dataService.DeleteLost(id);
            if (result == true)
                return StatusCode((int)HttpStatusCode.OK);
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPost("/api/[controller]/newlost")]
        public async Task<IActionResult> CreateLost([FromBody]Lost lost)
        {
            var result = await this.dataService.RegisterLost(lost);
            if (result != null)
            {
                await this.subscribeService.Notify(new Notification
                {
                    Body = "Added new lost!",
                    Title = "New lost!",
                    Tags = new string[] { $"lastName:{lost.LastName}", $"middleName:{lost.MiddleName}", $"firstName:{lost.FirstName}"}
                });
                return Json(result);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}