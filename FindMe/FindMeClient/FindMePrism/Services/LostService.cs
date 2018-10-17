using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FindMePrism.Models;
using Newtonsoft.Json;

namespace FindMePrism.Services
{
    public class LostService : ILostService
    {
        private readonly HttpClient client;
        public LostService()
        {
            this.client = new HttpClient
            {
                BaseAddress = new Uri(App.ServerUrl),
                Timeout = TimeSpan.FromMinutes(30)
            };
        }


        public async Task<Lost> AddLost(Lost lost)
        {
            var data = JsonConvert.SerializeObject(lost);
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var response = this.client.PostAsync("/api/losts/newlost", content).Result;
            if (response.IsSuccessStatusCode) {
                var answer = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Lost>(answer);             
            }
            else
                return null;
        }


        public async Task<bool> EditLost(Lost lost)
        {

            var data = JsonConvert.SerializeObject(lost);
            var content = new StringContent(data, UnicodeEncoding.UTF8, "application/json");
            var response = await client.PutAsync("/api/losts/editlost", content);
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }


        public async Task<bool> RemoveLost(Lost lost)
        {

            var response = await client.DeleteAsync($"/api/losts/deletelost/{lost.Id}");
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<Lost>> GetLosts(Institution institution)
        {

            var response = await client.GetAsync($"/api/losts/getlostsbyinstitution/{institution.Id}");
            if (response.IsSuccessStatusCode) {
                var answer = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Lost>>(answer);
            }
            else
                return null;

        }

        public IEnumerable<string> GetEyeColors()
        {
            List<string> Colors = new List<string>() { "Blue", "Gray", "Green", "Brown" };
            return Colors;
        }

        public IEnumerable<string> GetHairColors()
        {
            List<string> Colors = new List<string>() { "Blonde", "Brown", "Black" };
            return Colors;
        }

        public IEnumerable<string> GetGenders()
        {
            List<string> Genders = new List<string>() { "Male", "Female", "Unknown" };
            return Genders;
        }

        public IEnumerable<string> GetBodyTypes()
        {
            List<string> Types = new List<string>() { "Thin", "Fat", "Medium" };
            return Types;
        }
    }
}
