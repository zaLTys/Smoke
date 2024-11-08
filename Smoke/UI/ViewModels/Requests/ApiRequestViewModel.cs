namespace UI.ViewModels.Requests;

public class ApiRequestViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ApiRequestDataViewModel ApiRequestData { get; set; }
    //public DateTime CreatedDate { get; set; }
    //public DateTime ModifiedDate { get; set; }
    public string Type { get; set; } = "GET";

    public ApiRequestViewModel(Guid id, string name, ApiRequestDataViewModel apiRequestData, string type) //DateTime createdDate, DateTime modifiedDate,
    {
        Id = id;
        Name = name;
        ApiRequestData = apiRequestData;
        //CreatedDate = createdDate;
        //ModifiedDate = modifiedDate;
        Type = type;
    }
}

public class ApiRequestDataViewModel
{
    public string HttpMethod { get; set; }
    public string Url { get; set; }
    public List<HeaderViewModel> Headers { get; set; }
    public string? Body { get; set; }
    public object? ExpectedResponse { get; set; }

    public ApiRequestDataViewModel(string httpMethod, string url, List<HeaderViewModel> headers, string? body, object? expectedResponse)
    {
        HttpMethod = httpMethod;
        Url = url;
        Headers = headers;
        Body = body;
        ExpectedResponse = expectedResponse;
    }
}
