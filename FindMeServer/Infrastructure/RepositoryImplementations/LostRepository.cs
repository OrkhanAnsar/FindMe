using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryImplementations
{
    public class LostRepository : ILostRepository
    {
        private readonly FindMeDbContext context;
        private readonly IInstitutionRepository institutionRepository;

        public LostRepository(FindMeDbContext context, IInstitutionRepository institutionRepository)
        {
            this.context = context;
            this.institutionRepository = institutionRepository;
        }

        public async Task<Lost> CreateLost(Lost newLost)
        {
            var instFromServer = await this.context.Institutions.FirstOrDefaultAsync(i => i.Id == newLost.InstitutionId);
            newLost.Institution.Password = instFromServer.Password;
            var result = await this.context.Losts.AddAsync(newLost);
            await this.context.SaveChangesAsync();
            return await this.GetLostById(result.Entity.Id);
        }

        public async Task<Lost> GetLostById(int id)
        {
            return await this.context.Losts.
                                  Include(l => l.Institution).
                                  Include(l => l.Institution.City).
                                  Include(l => l.Institution.InstitutionType).
                                  FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<int> UpdateLost(Lost lost)
        {
            var institutionFromServer = this.context.Institutions.Where(i => i.Id == lost.InstitutionId).
                AsNoTracking();
            lost.Institution.Password = institutionFromServer.First().Password;
            this.context.Losts.Update(lost);
            return await this.context.SaveChangesAsync();
        }

        public async Task<int> RemoveLost(int id)
        {
            this.context.Losts.Remove(await this.context.Losts.FirstAsync(l => l.Id == id));
            return await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lost>> GetLosts()
        {
            return await this.context.Losts.
                Include(l => l.Institution).
                Include(l => l.Institution.City).
                Include(l => l.Institution.InstitutionType).
                ToListAsync();
        }

        public async Task<IEnumerable<Lost>> GetLostsByInstitution(Institution institution)
        {
            return await this.context.Losts.
                Include(l => l.Institution).
                Include(l => l.Institution.City).
                Include(l => l.Institution.InstitutionType).
                Where(l => l.InstitutionId == institution.Id).
                ToListAsync();
        }
    }
}