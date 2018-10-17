using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IInstitutionRepository
    {
        Task<int> UpdatePassword(Institution institution);
        Task<Institution> CreateInstitution(Institution newInstitution);
        Task<Institution> GetInstitutionByName(string name);
        Task<Institution> GetInstitutionByLogin(string login);
        Task<Institution> GetInstitutionById(int id);
        Task<int> UpdateInstitution(Institution institution);
        Task<int> RemoveInstitution(int id);
        Task<IEnumerable<Institution>> GetInstitutions();
        Task<Institution> GetInstitutionByNameAndPassword(string name, string password);
    }
}