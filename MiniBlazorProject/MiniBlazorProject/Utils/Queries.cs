using MiniBlazorProject.QueryObjects;

namespace MiniBlazorProject.Utils;

public class Queries
{
    public static string countQuery()
    {
        return $"[ {{ $count: \"Total de Registros\" }} ]";
    }
    public static string GroupEnterpriseQuery(EnterpriseQO enterpriseQO)
    {
        var insideQuery = string.Empty;
        foreach (var prop in enterpriseQO.propsToGroup)
        {
            insideQuery += $"{prop}: \"${prop}\", ";
        }
        return $"[ {{ $group: {{ _id: {{ {insideQuery} }} }} }} ]";
    }
    public static string MatchByNameQuery(string name)
    {
        return $"[ {{ $match : {{ \"Nome\" :{{ $regex:\"^(?i){name}[a-zA-Z]\" }} }} }} ]";
    }
    public static string MatchBySegment(string segmentId)
    {
        return $"[ {{ $match : {{ \"Segmento\" :{{ \"$oid\": \"{segmentId}\" }} }} }} ]";
    }
}
