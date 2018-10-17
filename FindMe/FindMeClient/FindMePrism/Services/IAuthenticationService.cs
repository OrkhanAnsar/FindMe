using FindMePrism.Models;
using System.Threading.Tasks;

namespace FindMePrism.Services
{
    public interface IAuthenticationService
    {
        Task<Institution> Validate(Institution institution);
    }
}
