using MiniBlazorProject.Contracts;
using MiniBlazorProject.Models;
using MiniBlazorProject.Utils;
using Newtonsoft.Json;
using System.Security;

namespace MiniBlazorProject.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        private HttpClient _httpClient;
        public EnterpriseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Enterprise>> GetEnterprises(int pageSize, int currentPage)
        {
            var enterprises = new List<Enterprise>();
            var request = EndPoints.GetSegmentsEndpoint(pageSize,currentPage);
            var response = await _httpClient.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic? result = JsonConvert.DeserializeObject(jsonResponse);
                var data = result.data;
                foreach (var item in data)
                {
                    enterprises.Add(new()
                    {
                        Id = item.GetValue("_id").GetValue("$oid").Value,
                        Name = item.GetValue("Nome").Value,
                        Site = item.GetValue("Site").Value,
                        Active = item.GetValue("Active").Value,
                        SegmentId = item.GetValue("Segmento").GetValue("$oid").Value,
                    });
                }
            }
            return enterprises;
        }

        public async Task CreateEnterprise(Enterprise enterprise)
        {
            var requestBody = RequestBodies.CreateEnterpriseRequestBody(enterprise);
            await _httpClient.PostAsync(EndPoints.BaseEnterpriseEndpoint(), new StringContent(requestBody));
        }

        public Task<Enterprise> GetEnterpriseById(string EnterpriseId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Enterprise>> QueryEnterprises(string query)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEnterprise(Enterprise Enterprise)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEnterprise(string EnterpriseId)
        {
            throw new NotImplementedException();
        }
    }
}
