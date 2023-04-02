namespace MiniBlazorProject.Utils;

public static class EndPoints
{
    private static string API_KEY = "6428f9becc6d53302309ceb3";
    private static string ENTERPRISES_ENDPOINT = "611ed902fd5915f2ae005dbb";
    private static string SEGMENTS_ENDPOINT = "611edbd7fd5915f2ae005dc2";

    #region Enterprise Endpoints
    /// <summary>
    /// Used to get by id, update and delete enterprises
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string BaseEnterpriseEndpoint(string id)
    {
        return $"{ENTERPRISES_ENDPOINT}/{id}?apiKey={API_KEY}";
    }

    public static string CreateEnterpriseEndpoint()
    {
        return $"{ENTERPRISES_ENDPOINT}?apiKey={API_KEY}";
    }

    public static string GetEnterprisesEndpoint(int pageSize, int currentPage)
    {
        return $"{ENTERPRISES_ENDPOINT}?apiKey={API_KEY}&page={currentPage}&pageSize={pageSize}";
    }
    public static string QueryEnterprisesEndpoint(int pageSize, int currentPage)
    {
        return $"{ENTERPRISES_ENDPOINT}/query?apiKey={API_KEY}&page={currentPage}&pageSize={pageSize}";
    }
    public static string QueryEnterprisesEndpoint()
    {
        return $"{ENTERPRISES_ENDPOINT}/query?apiKey={API_KEY}";
    }
    #endregion

    #region Segment Endpoints
    public static string BaseSegmentEndpoint(string id)
    {
        return $"{SEGMENTS_ENDPOINT}/{id}?apiKey={API_KEY}";
    }

    public static string CreateSegmentEndpoint()
    {
        return $"{SEGMENTS_ENDPOINT}?apiKey={API_KEY}";
    }

    public static string GetSegmentsEndpoint(int pageSize, int currentPage)
    {
        return $"{SEGMENTS_ENDPOINT}?apiKey={API_KEY}&page={currentPage}&pageSize={pageSize}";
    }

    public static string QuerySegmentsEndpoint(int pageSize, int currentPage)
    {
        return $"{SEGMENTS_ENDPOINT}/query?apiKey={API_KEY}&page={currentPage}&pageSize={pageSize}";
    }
    public static string QuerySegmentsEndpoint()
    {
        return $"{SEGMENTS_ENDPOINT}/query?apiKey={API_KEY}";
    }
    #endregion
}
