using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementations
{
    public class CityRepository : ICityRepository
    {
        private readonly FindMeDbContext context;

        public CityRepository(FindMeDbContext context)
        {
            this.context = context;
        }

        public async Task<City> CreateCity(City newCity)
        {
            try
            {
                var result = await this.context.Cities.AddAsync(newCity);
                await this.context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<City> GetCityById(int id)
        {
            return await this.context.Cities.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<City> GetCityByName(string name)
        {
            return await this.context.Cities.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async void RemoveCity(City city)
        {
            this.context.Cities.Remove(city);
            await this.context.SaveChangesAsync();
        }
    }
}
