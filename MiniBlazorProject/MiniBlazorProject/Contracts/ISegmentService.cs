using MiniBlazorProject.Models;

namespace MiniBlazorProject.Contracts
{
    public interface ISegmentService
    {
        Task<List<Segment>> GetAllSegments(int pageSize, int currentPage);
        Task<Segment> GetSegmentById(string segmentId);
        Task<List<Segment>> QuerySegments(string query, int pageSize, int currentPage);
        Task CreateSegment(Segment segment);
        Task UpdateSegment(Segment segment);
        Task DeleteSegment(string segmentId);
        Task<int> GetSegmentCount();
    }
}
