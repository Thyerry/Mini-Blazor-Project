using MiniBlazorProject.Models;

namespace MiniBlazorProject.QueryObjects
{
    public class EnterpriseCount
    {
        public Count[] data { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }

    public class Count
    {
        public int count { get; set; }
    }
}
