using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ApplicationCore.ServiceInterfaces;

namespace FindMeServer.Controllers
{
    [Produces("application/json")]
    [Route("api/register")]
    public class RegisterController : Controller
    {
        private readonly ISubscribeService subscribeService;

        public RegisterController(ISubscribeService subscribeService)
        {
            this.subscribeService = subscribeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.subscribeService.Notify(new ApplicationCore.NotificationConfig.Notification
            {
                Body = "Added new lost!",
                Title = "New lost!"
            });
            if (result != null)
                return Ok();
            else
                return BadRequest();
        }

        // POST api/register
        // This creates a registration id
        [Route("{regId}")]
        public async Task<IActionResult> Post([FromBody]string[] tags, string regId)
        {
            if (regId == null && tags is null)
                return BadRequest();
            var result = await this.subscribeService.Subsribe(tags, regId);
            if (result != null)
                return Ok();
            else
                return StatusCode((int)HttpStatusCode.ServiceUnavailable);
        }

        [HttpPost("api/[controller]/remove/{regId}")]
        public async Task<IActionResult> Delete([FromBody]string[] tags, string regId)
        {
            if (tags != null && regId != null)
            {
                var result = await this.subscribeService.Unsubscribe(tags, regId);
                if (result)
                    return StatusCode((int)HttpStatusCode.OK);
                else
                    return StatusCode((int)HttpStatusCode.ServiceUnavailable);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}