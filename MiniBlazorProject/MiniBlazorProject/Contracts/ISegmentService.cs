using MiniBlazorProject.Models;

namespace MiniBlazorProject.Contracts
{
    public interface ISegmentService
    {
        Task<List<Segment>> GetAllSegments();
        Task<Segment> GetSegmentById(string segmentId);
        Task<List<Segment>> QuerySegment(string query);
        Task CreateSegment(Segment segment);
        Task UpdateSegment(Segment segment);
        Task DeleteSegment(string segmentId);
    }
}
