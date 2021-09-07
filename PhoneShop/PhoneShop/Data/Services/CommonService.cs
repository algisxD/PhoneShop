using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PSAPI.Data.Entities;
using PSAPI.Data.Helpers;
using PSAPI.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PSAPI.Data.Services
{
    public class CommonService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public CommonService(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
        }


        public async Task<bool> BankValidation()
        {
            return true;
        }

        //Planai:

        public async Task<IEnumerable<PlanasViewModel>> GetPlanaiAsync(string baseUrl)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/Planu/");
            return JsonConvert.DeserializeObject<IEnumerable<PlanasViewModel>>(json);
        }
        public async Task<PlanasViewModel> GetPlanasAsync(string baseUrl, int id)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/Planu/{id}");
            return JsonConvert.DeserializeObject<PlanasViewModel>(json);
        }
        public async Task<int> InsertPlanasAsync(string baseUrl, Planas planas)
        {
            var response = await _client.PostAsync($"{baseUrl}api/Planu/", RequestHelper.GetStringContentFromObject(planas));
            return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }
        public async Task<HttpResponseMessage> UpdatePlanasAsync(string baseUrl, Planas planas)
        {
            return await _client.PutAsync($"{baseUrl}api/Planu/", RequestHelper.GetStringContentFromObject(planas));
        }
        public async Task<HttpResponseMessage> DeletePlanasAsync(string baseUrl, int id)
        {
            return await _client.DeleteAsync($"{baseUrl}api/Planu/{id}");
        }

        //Detalės:

        public async Task<IEnumerable<DetaleViewModel>> GetDetalesAsync(string baseUrl)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/DetaliuUzsakymo/");
            return JsonConvert.DeserializeObject<IEnumerable<DetaleViewModel>>(json);
        }
        public async Task<DetaleViewModel> GetDetaleAsync(string baseUrl, int id)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/DetaliuUzsakymo/{id}");
            return JsonConvert.DeserializeObject<DetaleViewModel>(json);
        }
        public async Task<int> InsertDetaleAsync(string baseUrl, Detale detale)
        {
            var response = await _client.PostAsync($"{baseUrl}api/DetaliuUzsakymo/", RequestHelper.GetStringContentFromObject(detale));
            return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }
        public async Task<HttpResponseMessage> UpdateDetaleAsync(string baseUrl, Detale detale)
        {
            var a = await _client.PutAsync($"{baseUrl}api/DetaliuUzsakymo/", RequestHelper.GetStringContentFromObject(detale));
            return a;
        }
        public async Task<HttpResponseMessage> DeleteDetaleAsync(string baseUrl, int id)
        {
            return await _client.DeleteAsync($"{baseUrl}api/DetaliuUzsakymo/{id}");
        }

        
        //Telefono modeliai
        public async Task<IEnumerable<TelefonoModelisViewModel>> GetTelefonoModeliaiAsync(string baseUrl)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/Pirkimo/");
            return JsonConvert.DeserializeObject<IEnumerable<TelefonoModelisViewModel>>(json);
        }
        public async Task<TelefonoModelisViewModel> GetTelefonoModelisAsync(string baseUrl, int id)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/Pirkimo/{id}");
            return JsonConvert.DeserializeObject<TelefonoModelisViewModel>(json);
        }
        public async Task<int> InsertTelefonoModelisAsync(string baseUrl, TelefonoModelis modelis)
        {
            var response = await _client.PostAsync($"{baseUrl}api/Pirkimo/", RequestHelper.GetStringContentFromObject(modelis));
            return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }
        public async Task<HttpResponseMessage> UpdateTelefonoModelisAsync(string baseUrl, TelefonoModelis modelis)
        {
            return await _client.PutAsync($"{baseUrl}api/Pirkimo/", RequestHelper.GetStringContentFromObject(modelis));
        }
        public async Task<HttpResponseMessage> DeleteTelefonoModelisAsync(string baseUrl, int id)
        {
            return await _client.DeleteAsync($"{baseUrl}api/Pirkimo/{id}");
        }

        //Saskaitos:

        public async Task<IEnumerable<SaskaitaViewModel>> GetSaskaitosAsync(string baseUrl)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/Saskaitos/");
            return JsonConvert.DeserializeObject<IEnumerable<SaskaitaViewModel>>(json);
        }
        public async Task<SaskaitaViewModel> GetSaskaitaAsync(string baseUrl, string id)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/Saskaitos/{id}");
            return JsonConvert.DeserializeObject<SaskaitaViewModel>(json);
        }
        public async Task<int> InsertSaskaitaAsync(string baseUrl, Saskaita saskaita)
        {
            var response = await _client.PostAsync($"{baseUrl}api/Saskaitos/", RequestHelper.GetStringContentFromObject(saskaita));
            return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }
        public async Task<HttpResponseMessage> UpdateSaskaitaAsync(string baseUrl, Saskaita saskaita)
        {
            var a = await _client.PutAsync($"{baseUrl}api/Saskaitos/", RequestHelper.GetStringContentFromObject(saskaita));
            return a;
        }
        public async Task<HttpResponseMessage> DeleteSaskaitaAsync(string baseUrl, int id)
        {
            return await _client.DeleteAsync($"{baseUrl}api/Saskaitos/{id}");
        }

        //Uzsakymai:

        public async Task<IEnumerable<UzsakymasViewModel>> GetUzsakymaiAsync(string baseUrl)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/Uzsakymo/");
            return JsonConvert.DeserializeObject<IEnumerable<UzsakymasViewModel>>(json);
        }
        public async Task<UzsakymasViewModel> GetUzsakymasAsync(string baseUrl, string id)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/Uzsakymo/{id}");
            return JsonConvert.DeserializeObject<UzsakymasViewModel>(json);
        }
        public async Task<int> InsertUzsakymasAsync(string baseUrl, Uzsakymas uzsakymas)
        {
            var response = await _client.PostAsync($"{baseUrl}api/Uzsakymo/", RequestHelper.GetStringContentFromObject(uzsakymas));
            return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }
        public async Task<HttpResponseMessage> UpdateUzsakymasAsync(string baseUrl, Uzsakymas uzsakymas)
        {
            var a = await _client.PutAsync($"{baseUrl}api/Uzsakymo/", RequestHelper.GetStringContentFromObject(uzsakymas));
            return a;
        }
        public async Task<HttpResponseMessage> DeleteUzsakymasAsync(string baseUrl, int id)
        {
            return await _client.DeleteAsync($"{baseUrl}api/Uzsakymo/{id}");
        }

        //El parasai:

        public async Task<IEnumerable<EParasasViewModel>> GetEParasaiAsync(string baseUrl)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/ElektroninioParaso/");
            return JsonConvert.DeserializeObject<IEnumerable<EParasasViewModel>>(json);
        }
        public async Task<EParasasViewModel> GetEParasasAsync(string baseUrl, int id)
        {
            var json = await _client.GetStringAsync($"{baseUrl}api/ElektroninioParaso/{id}");
            return JsonConvert.DeserializeObject<EParasasViewModel>(json);
        }
        public async Task<int> InsertEParasasAsync(string baseUrl, EParasas parasas)
        {
            var response = await _client.PostAsync($"{baseUrl}api/ElektroninioParaso/", RequestHelper.GetStringContentFromObject(parasas));
            return Convert.ToInt32(response.Content.ReadAsStringAsync().Result);
        }
        public async Task<HttpResponseMessage> UpdateEParasasAsync(string baseUrl, EParasas parasas)
        {
            var a = await _client.PutAsync($"{baseUrl}api/ElektroninioParaso/", RequestHelper.GetStringContentFromObject(parasas));
            return a;
        }
        public async Task<HttpResponseMessage> DeleteEParasasAsync(string baseUrl, int id)
        {
            return await _client.DeleteAsync($"{baseUrl}api/ElektroninioParaso/{id}");
        }
    }
}
