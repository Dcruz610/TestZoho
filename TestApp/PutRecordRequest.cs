using System.Text.Json.Serialization;

namespace TestApp;

public class PutRecordRequest
{
    [JsonPropertyName("data")]
    public List<PutData> Data { get; set; }
}

public class PutData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("Company")]
    public string Company { get; set; }
}