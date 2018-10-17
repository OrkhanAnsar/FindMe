using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IInstitutionsTypeRepository
    {
        Task<InstitutionType> GetInstitutionById(int id);
        Task<InstitutionType> GetInstitutionByName(string name);
        Task<IEnumerable<InstitutionType>> GetInstitutionTypes();
    }
}