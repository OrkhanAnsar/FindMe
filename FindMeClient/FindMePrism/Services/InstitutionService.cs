using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FindMePrism.Models;
using Newtonsoft.Json;

namespace FindMePrism.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly HttpClient client;

        public InstitutionService()
        {
            this.client = new HttpClient
            {
                BaseAddress = new Uri(App.ServerUrl),
                Timeout = TimeSpan.FromMinutes(30)
            };
        }

        public async Task<Institution> AddInstitution(Institution institution)
        {
            var data = JsonConvert.SerializeObject(institution);
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var response = await this.client.PostAsync("/api/institutions/registerinstitution", content);
            if (response.IsSuccessStatusCode)
            {
                var answer = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Institution>(answer);
            }
            else
                return null;
        }

        public async Task<bool> EditInstitution(Institution institution)
        {
            var data = JsonConvert.SerializeObject(institution);
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var response = await this.client.PutAsync("/api/institutions/editinstitution", content);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<Institution>> GetInstitutions()
        {
            var response = await this.client.GetAsync("/api/institutions/getinstitutions");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Institution>>(data);
            }
            else
                return null;
        }


        public async Task<bool> RemoveInstitution(Institution institution)
        {
            var response = await client.DeleteAsync($"/api/institutions/deleteinstitution/{institution.Id}");
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<List<InstitutionType>> GetInstitutionTypes()
        {
            var response = this.client.GetAsync("/api/institutiontypes/getinstitutiontypes").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<InstitutionType>>(data);
            }
            else
                return null;
        }

        public async Task<bool> ChangePassword(Institution institution)
        {
            var data = JsonConvert.SerializeObject(institution);
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var response = await this.client.PutAsync("/api/institutions/resetpassword", content);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}