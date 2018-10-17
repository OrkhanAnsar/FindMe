using ApplicationCore.Helpers;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FindMeServer.Controllers {
    [Route("api/[controller]/[action]")]
    public class InstitutionsController : Controller {
        private readonly IDataService dataService;
        private readonly IAuthenticationService authenticationService;

        public InstitutionsController(IDataService dataService, IAuthenticationService authenticationService) {
            this.dataService = dataService;
            this.authenticationService = authenticationService;
        }

        [HttpPut("/api/[controller]/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody]Institution institution) {
            var result = await this.authenticationService.ResetPassword(institution);
            if (result.Message == Message.Successful)
                return Json(result.Data);
            else if (result.Message == Message.IncorrectPassword)
                return StatusCode((int)HttpStatusCode.Unauthorized);
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPost("/api/[controller]/registerinstitution")]
        public async Task<JsonResult> RegisterInstitution([FromBody]Institution institution) {
            if (institution != null) {
                return Json(await this.authenticationService.RegisterInstitution(institution));
            } else {
                return Json(null);
            }
        }

        [HttpPost("/api/[controller]/login")]
        public async Task<JsonResult> Login([FromBody]Institution institution) {
            return Json(await this.authenticationService.Login(institution));
        }

        [HttpPut("/api/[controller]/editinstitution")]
        public async Task<ActionResult> EditInstitution([FromBody]Institution institution) {
            var result = await this.dataService.EditInstitution(institution);
            if (result == true)
                return StatusCode((int)HttpStatusCode.OK);
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpDelete("/api/[controller]/deleteinstitution/{id}")]
        public async Task<ActionResult> DeleteInstitution(int id) {
            var result = await this.dataService.DeleteInstitution(id);
            if (result == true)
                return StatusCode((int)HttpStatusCode.OK);
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("/api/[controller]/getinstitutions")]
        public async Task<ActionResult> Get() {
            var institutions = await this.dataService.GetInstitutions();
            if (Enumerable.Count(institutions) > 0)
                return Json(institutions);
            else if (institutions is null)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            else
                return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}