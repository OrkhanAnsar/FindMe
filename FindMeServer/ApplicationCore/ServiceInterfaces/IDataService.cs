using ApplicationCore.DataTransferObjects;
using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IDataService
    {
        Task<IEnumerable<InstitutionType>> GetInstitutionTypes();
        Task<IEnumerable<LostDTO>> GetLosts();
        Task<IEnumerable<InstitutionDTO>> GetInstitutions();
        Task<LostDTO> RegisterLost(Lost lost);
        Task<bool> DeleteLost(int id);
        Task<bool> EditLost(Lost lost);
        Task<bool> DeleteInstitution(int id);
        Task<IEnumerable<Lost>> GetLostsByInstitution(int id);
        Task<bool> EditInstitution(Institution istitution);
    }
}