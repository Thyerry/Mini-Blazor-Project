using MiniBlazorProject.Contracts;
using MiniBlazorProject.Models;
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
            await _httpClient.DeleteAsync(EndPoints.BaseSegmentEndpoint(segmentId));
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
                    return segment;

                segment = MapSegment(result);
            }
            return segment;
        }

        public async Task<List<Segment>> QuerySegment(string query, int pageSize, int currentPage)
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

        private Segment MapSegment(dynamic? jsonObject)
        {
            return new()
            {
                Id = jsonObject.GetValue("_id").GetValue("$oid").Value,
                Name = jsonObject.GetValue("Nome").Value,
                Description = jsonObject.GetValue("Descrição").Value,
            };
        }
    }
}
