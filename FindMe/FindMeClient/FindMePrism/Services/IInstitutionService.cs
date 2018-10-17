using FindMePrism.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindMePrism.Services
{
    public interface IInstitutionService
    {
        Task<IEnumerable<Institution>> GetInstitutions();
        Task<Institution> AddInstitution(Institution institution);
        Task<bool> RemoveInstitution(Institution institution);
        Task<bool> EditInstitution(Institution institution);
        Task<bool> ChangePassword(Institution institution);
        Task<List<InstitutionType>> GetInstitutionTypes();
    }
}
