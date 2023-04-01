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
            var request = $"{EndPoints.GET_ENTERPRISES}&page={currentPage}&pageSize={pageSize}";
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
    }
}
