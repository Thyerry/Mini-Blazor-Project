namespace MiniBlazorProject.Utils;

public static class EndPoints
{
    private static string API_KEY = "64263e42eb03b34e55623b8e";
    private static string ENTERPRISES_ENDPOINT = "611ed902fd5915f2ae005dbb";
    private static string SEGMENTS_ENDPOINT = "611edbd7fd5915f2ae005dc2";

    public static string BaseEnterpriseEndpoint()
    {
        return $"{ENTERPRISES_ENDPOINT}?apiKey={API_KEY}";
    }

    public static string GetEnterprisesEndpoint(int pageSize, int currentPage)
    {
        return $"{ENTERPRISES_ENDPOINT}?apiKey={API_KEY}&page={currentPage}&pageSize={pageSize}";
    }

    public static string GetEnterpriseByIdEndpoint(string id)
    {
        return $"{ENTERPRISES_ENDPOINT}/{id}?apiKey={API_KEY}";
    }

    public static string QueryEnterprisesEndpoint(int pageSize, int currentPage)
    {
        return $"{ENTERPRISES_ENDPOINT}/query?apiKey={API_KEY}&page={currentPage}&pageSize={pageSize}";
    }

    public static string BaseSegmentEndpoint()
    {
        return $"{SEGMENTS_ENDPOINT}?apiKey={API_KEY}";
    }

    public static string GetSegmentsEndpoint(int pageSize, int currentPage)
    {
        return $"{SEGMENTS_ENDPOINT}?apiKey={API_KEY}&page={currentPage}&pageSize={pageSize}";
    }

    public static string GetSegmentByIdEndpoint(string id)
    {
        return $"{SEGMENTS_ENDPOINT}/{id}?apiKey={API_KEY}";
    }

    public static string QuerySegmentsEndpoint(int pageSize, int currentPage)
    {
        return $"{SEGMENTS_ENDPOINT}/query?apiKey={API_KEY}&page={currentPage}&pageSize={pageSize}";
    }
}
