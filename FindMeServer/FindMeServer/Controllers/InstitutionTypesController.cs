using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace FindMeServer.Controllers
{
    [Produces("application/json")]
    [Route("api/InstitutionTypes")]
    public class InstitutionTypesController : Controller
    {
        private readonly IDataService dataService;
        public InstitutionTypesController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet("/api/[controller]/getinstitutiontypes")]
        public async Task<IActionResult> GetInstitutionTypes()
        {
            return Json(await this.dataService.GetInstitutionTypes());
        }
    }
}