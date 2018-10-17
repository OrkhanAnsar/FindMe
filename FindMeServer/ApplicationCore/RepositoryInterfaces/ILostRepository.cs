using ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface ILostRepository
    {
        Task<Lost> CreateLost(Lost newLost);
        Task<Lost> GetLostById(int id);
        Task<IEnumerable<Lost>> GetLostsByInstitution(Institution institution);
        Task<IEnumerable<Lost>> GetLosts();
        Task<int> UpdateLost(Lost lost);
        Task<int> RemoveLost(int id);
    }
}