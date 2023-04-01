using Microsoft.AspNetCore.Components;
using MiniBlazorProject.Contracts;
using MiniBlazorProject.Models;
using MiniBlazorProject.Utils;
using Newtonsoft.Json;

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
            var request = EndPoints.GetEnterprisesEndpoint(pageSize, currentPage);
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
            var requestBody = RequestBodies.UpsertEnterpriseRequestBody(enterprise);
            await _httpClient.PostAsync(EndPoints.CreateEnterpriseEndpoint(), new StringContent(requestBody));
        }

        public async Task<Enterprise> GetEnterpriseById(string enterpriseId)
        {
            Enterprise enterprise = new Enterprise();
            var request = EndPoints.BaseEnterpriseEndpoint(enterpriseId);
            var response = await _httpClient.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic? result = JsonConvert.DeserializeObject(jsonResponse);
                enterprise = new()
                {
                    Id = result.GetValue("_id").GetValue("$oid").Value,
                    Name = result.GetValue("Nome").Value,
                    Site = result.GetValue("Site").Value,
                    Active = result.GetValue("Active").Value,
                    SegmentId = result.GetValue("Segmento").GetValue("$oid").Value,
                };
            }
            return enterprise;
        }

        public async Task<List<Enterprise>> QueryEnterprises(string query, int pageSize, int currentPage)
        {
            var enterprises = new List<Enterprise>();
            var request = EndPoints.QueryEnterprisesEndpoint(pageSize, currentPage);
            var requestBody = Queries.MatchByNameQuery(query);
            var response = await _httpClient.PostAsync(request, new StringContent(requestBody));
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic? result = JsonConvert.DeserializeObject(jsonResponse);
                var data = result.data;
                if (data.Count == 0)
                    return enterprises;

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

        public async Task UpdateEnterprise(Enterprise enterprise)
        {
            var requestBody = RequestBodies.UpsertEnterpriseRequestBody(enterprise);
            await _httpClient.PutAsync(EndPoints.BaseEnterpriseEndpoint(enterprise.Id), new StringContent(requestBody));
        }

        public async Task DeleteEnterprise(string enterpriseId)
        {
            await _httpClient.DeleteAsync(EndPoints.BaseEnterpriseEndpoint(enterpriseId));
        }
    }
}
