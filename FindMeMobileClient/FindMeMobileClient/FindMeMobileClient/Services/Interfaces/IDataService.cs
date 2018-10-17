using FindMeMobileClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindMeMobileClient.Services.Interfaces
{
    public interface IDataService
    {
        Task<IEnumerable<Lost>> GetLosts();
        Task<IEnumerable<Institution>> GetInstitutions();
    }
}