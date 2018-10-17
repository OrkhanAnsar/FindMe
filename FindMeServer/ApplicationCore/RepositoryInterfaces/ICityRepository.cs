using ApplicationCore.Models;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface ICityRepository
    {
        Task<City> CreateCity(City newCity);
        void RemoveCity(City city);
        Task<City> GetCityByName(string name);
        Task<City> GetCityById(int id);
    }
}