using MiniBlazorProject.Contracts;
using MiniBlazorProject.Models;
using MiniBlazorProject.QueryObjects;
using MiniBlazorProject.Utils;
using Newtonsoft.Json;

namespace MiniBlazorProject.Services
{
    public class SegmentService : ISegmentService
    {
        private readonly HttpClient _httpClient;

        public SegmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateSegment(Segment segment)
        {
            var requestBody = RequestBodies.UpsertSegmentRequestBody(segment);
            await _httpClient.PostAsync(EndPoints.CreateSegmentEndpoint(), new StringContent(requestBody));
        }

        public async Task DeleteSegment(string segmentId)
        {
            var response = await _httpClient.DeleteAsync(EndPoints.BaseSegmentEndpoint(segmentId));
            if(!response.IsSuccessStatusCode)
            {
               throw new HttpRequestException(await response.Content.ReadAsStringAsync()); 
            }
        }

        public async Task<List<Segment>> GetAllSegments(int pageSize, int currentPage)
        {
            var segments = new List<Segment>();
            var request = EndPoints.GetSegmentsEndpoint(pageSize, currentPage);
            var response = await _httpClient.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic? result = JsonConvert.DeserializeObject(jsonResponse);
                if (result.data.Count == 0)
                    return segments;

                foreach (var item in result.data)
                {
                    segments.Add(MapSegment(item));
                }
            }
            return segments;
        }

        public async Task<Segment> GetSegmentById(string segmentId)
        {
            var segment = new Segment();
            var request = EndPoints.BaseSegmentEndpoint(segmentId);
            var response = await _httpClient.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic? result = JsonConvert.DeserializeObject(jsonResponse);

                if (result.Count == 0)
                    return null;

                segment = MapSegment(result);
            }
            return segment;
        }

        public async Task<List<Segment>> QuerySegments(string query, int pageSize, int currentPage)
        {
            var segments = new List<Segment>();
            var request = EndPoints.QuerySegmentsEndpoint(pageSize, currentPage);
            var requestBody = Queries.MatchByNameQuery(query);
            var response = await _httpClient.PostAsync(request, new StringContent(requestBody));
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic? result = JsonConvert.DeserializeObject(jsonResponse);

                if (result.data.Count == 0)
                    return segments;

                foreach (var item in result.data)
                {
                    segments.Add(MapSegment(item));
                }
            }
            return segments;
        }

        public async Task UpdateSegment(Segment segment)
        {
            var requestBody = RequestBodies.UpsertSegmentRequestBody(segment);
            await _httpClient.PutAsync(EndPoints.BaseSegmentEndpoint(segment.Id), new StringContent(requestBody));
        }

        public async Task<int> GetSegmentCount()
        {
            int count = 0;
            var requestBody = Queries.CountAll();
            var response = await _httpClient.PostAsync(EndPoints.QuerySegmentsEndpoint(), new StringContent(requestBody));
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CountResponse>(jsonResponse);
                count = result.data.FirstOrDefault().count;
            }
            return count;
        }

        private Segment MapSegment(dynamic? jsonObject)
        {
            var id = jsonObject.GetValue("_id").GetValue("$oid").Value;
            var name = jsonObject.GetValue("Nome").Value;
            var description = jsonObject.GetValue("Descrição").Value;
            return new()
            {
                Id = id,
                Name = name,
                Description = description,
            };
        }
    }
}
