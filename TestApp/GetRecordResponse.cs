using System.Text.Json.Serialization;

namespace TestApp;

public class GetRecordResponse
{
    [JsonPropertyName("data")]
    public List<Data> Data { get; set; }
}

public class Data
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("Last_Name")]
    public string LastName { get; set; }
}