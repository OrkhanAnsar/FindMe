using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.DataTransferObjects;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using AutoMapper;

namespace ApplicationCore.ServiceImplementations
{
    public class DataService : IDataService
    {
        private readonly ILostRepository lostRepository;
        private readonly IInstitutionRepository institutionRepository;
        private readonly IMapper mapper;
        private readonly IInstitutionsTypeRepository institutionsTypeRepository;

        public DataService(ILostRepository lostRepository, IInstitutionRepository institutionRepository, IInstitutionsTypeRepository institutionsTypeRepository,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.lostRepository = lostRepository;
            this.institutionRepository = institutionRepository;
            this.institutionsTypeRepository = institutionsTypeRepository;
        }

        public async Task<IEnumerable<InstitutionDTO>> GetInstitutions()
        {
            var result = await this.institutionRepository.GetInstitutions();
            return this.mapper.Map<IEnumerable<Institution>, IEnumerable<InstitutionDTO>>(result);
        }

        public async Task<IEnumerable<LostDTO>> GetLosts()
        {
            return this.mapper.Map<IEnumerable<Lost>, IEnumerable<LostDTO>>(await this.lostRepository.GetLosts());
        }

        public async Task<IEnumerable<Lost>> GetLostsByInstitution(int id)
        {
            return await this.lostRepository.GetLostsByInstitution(new Institution { Id = id });
        }

        public async Task<bool> EditLost(Lost lost)
        {
            var result = await this.lostRepository.UpdateLost(lost);
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteLost(int id)
        {
            var result = await this.lostRepository.RemoveLost(id);
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<LostDTO> RegisterLost(Lost lost)
        {
            var result = await this.lostRepository.CreateLost(lost);
            if (result != null)
            {
                return this.mapper.Map<Lost, LostDTO>(result);
            }
            return null;
        }

        public async Task<bool> DeleteInstitution(int id)
        {
            var result = await this.institutionRepository.RemoveInstitution(id);
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> EditInstitution(Institution institution)
        {
            var result = await this.institutionRepository.UpdateInstitution(institution);
            if (result > 0)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<InstitutionType>> GetInstitutionTypes()
        {
            return await this.institutionsTypeRepository.GetInstitutionTypes();
        }
    }
}