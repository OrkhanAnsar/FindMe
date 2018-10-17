using ApplicationCore.DataTransferObjects;
using ApplicationCore.Helpers;
using ApplicationCore.Models;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IAuthenticationService
    {
        Task<InstitutionDTO> RegisterInstitution(Institution institution);
        Task<InstitutionDTO> Login(Institution institution);
        Task<DataResult> ResetPassword(Institution institution);
    }
}