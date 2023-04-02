using Microsoft.AspNetCore.Components;
using MiniBlazorProject.Contracts;
using MiniBlazorProject.Models;
using MiniBlazorProject.Pages;
using MiniBlazorProject.QueryObjects;
using MiniBlazorProject.Utils;
using Newtonsoft.Json;

namespace MiniBlazorProject.Services
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly HttpClient _httpClient;
        private readonly ISegmentService _segmentService;
        public EnterpriseService(HttpClient httpClient, ISegmentService segmentService)
        {
            _httpClient = httpClient;
            _segmentService = segmentService;
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
                    enterprises.Add(await MapEnterprise(item));
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

                if (result.Count == 0)
                    return enterprise;

                enterprise = await MapEnterprise(result);
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

                if (result.data.Count == 0)
                    return enterprises;

                foreach (var item in result.data)
                {
                    enterprises.Add(await MapEnterprise(item));
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

        public async Task<int> GetEnterprisesCount()
        {
            return await GetEnterprisesCount(Queries.CountAll());
        }

        public async Task<int> GetEnterprisesCount(string query)
        {
            int count = 0;
            var response = await _httpClient.PostAsync(EndPoints.QueryEnterprisesEndpoint(), new StringContent(query));
            if(response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CountResponse>(jsonResponse);
                count = result.data.FirstOrDefault().count;
            }
            return count;
        }

        private async Task<Enterprise> MapEnterprise(dynamic? jsonObject)
        {
            string segmentId = string.Empty;
            try
            {
                segmentId = jsonObject.GetValue("Segmento").GetValue("$oid").Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new()
            {
                Id = jsonObject.GetValue("_id").GetValue("$oid").Value,
                Name = jsonObject.GetValue("Nome").Value,
                Site = jsonObject.GetValue("Site").Value,
                Active = jsonObject.GetValue("Active").Value,
                SegmentId = segmentId,
            };
        }
    }
}
