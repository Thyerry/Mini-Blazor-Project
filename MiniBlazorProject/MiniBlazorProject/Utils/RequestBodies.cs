using MiniBlazorProject.Models;

namespace MiniBlazorProject.Utils;

public class RequestBodies
{
    public static string UpsertEnterpriseRequestBody(Enterprise enterprise)
    {
        var id = "\"_id\": {\"$oid\": " + $"\"{enterprise.Id}\"" + "},";
        var name = "\"Nome\":" + $"\"{enterprise.Name}\",";
        var site = "\"Site\":" + $"\"{enterprise.Site}\",";
        var segment = "\"Segmento\": {\"$oid\": " + $"\"{enterprise.SegmentId}\"" + "},";
        var active = "\"Active\":" + $"\"{enterprise.Active}\"";

        if(enterprise.Id != null) return "{" + id + name + site + segment + active + "}";

        return "{" + name + site + segment + active + "}";
    }

    public static string UpsertSegmentRequestBody(Segment segment)
    {
        var id = "\"_id\": {\"$oid\": " + $"\"{segment.Id}\"" + "},";
        var name = "\"Nome\":" + $"\"{segment.Name}\",";
        var description = "\"Descrição\":" + $"\"{segment.Description}\"";

        if (segment.Id != null) return "{" + id + name + description + "}";

        return "{" + name + description + "}";
    }
}
