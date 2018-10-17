using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations
{
    public class InstitutionsTypeRepository : IInstitutionsTypeRepository
    {
        private readonly FindMeDbContext context;
        public InstitutionsTypeRepository(FindMeDbContext findMeDbContext)
        {
            this.context = findMeDbContext;
        }

        public async Task<InstitutionType> GetInstitutionById(int id)
        {
            return await this.context.InstitutionTypes.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<InstitutionType> GetInstitutionByName(string name)
        {
            return await this.context.InstitutionTypes.FirstOrDefaultAsync(i => i.Type == name);
        }

        public async Task<IEnumerable<InstitutionType>> GetInstitutionTypes()
        {
            return await this.context.InstitutionTypes.ToListAsync();
        }
    }
}