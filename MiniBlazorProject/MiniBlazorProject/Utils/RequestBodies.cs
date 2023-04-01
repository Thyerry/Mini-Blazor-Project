using MiniBlazorProject.Models;

namespace MiniBlazorProject.Utils;

public class RequestBodies
{
    public static string CreateEnterpriseRequestBody(Enterprise enterprise)
    {
        var name = "\"Nome\":" + $"\"{enterprise.Name}\",";
        var site = "\"Site\":" + $"\"{enterprise.Site}\",";
        var segment = "\"Segmento\": {\"$oid\": " + $"\"{enterprise.SegmentId}\"" + "},";
        var active = "\"Active\":" + $"\"{enterprise.Active}\"";
        return "{" + name + site + segment + active + "}";
    }
}
