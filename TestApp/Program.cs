using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TestApp;

string authenticationEndpoint = "https://accounts.zoho.com/oauth/v2/token";
string getRecordsEndpoint = "https://www.zohoapis.com/crm/v3/Leads?fields=Last_Name,Email&per_page=3";
string putRecordsEndpoint = "https://www.zohoapis.com/crm/v3/Leads";

var authenticationRequest = new Dictionary<string, string>()
{
    { "grant_type","authorization_code"},
    { "client_id","1000.N6SD4M4UQAW3AIPX6MLJHEQN05YRKA"},
    { "client_secret","e44c4516f81f64d57fed1bc8ace943b606b7ac4648"},
    { "redirect_uri",""},
    { "code","1000.34763372687d497e2269c3eb6a9c723a.9cf211531655e9a22499baa299e5cd61"}
};

var content = new FormUrlEncodedContent(authenticationRequest);

HttpClient client = new();
HttpResponseMessage authenticationResponse = new();
HttpResponseMessage recordResponse = new();

authenticationResponse = await client.PostAsync(authenticationEndpoint, content);

if (authenticationResponse.IsSuccessStatusCode)
{
    var responseData = await authenticationResponse.Content.ReadAsStringAsync();
    var responseDataJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseData);
    var accessToken = responseDataJson["access_token"];

    Console.WriteLine("Token Obtenido");
    Console.WriteLine(accessToken);
    //var accessToken = "1000.53f0e208b3d0ac151bb81bc8f8c100bf.b908497bbdb2e74c8db22d2a8866a4c2";

    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)accessToken);

    //Obtencion de los lead
    recordResponse = await client.GetAsync(getRecordsEndpoint);
    var recordResponseData = await recordResponse.Content.ReadAsStringAsync();
    var record = JsonConvert.DeserializeObject<GetRecordResponse>(recordResponseData);

    var recordToUpdate = record.Data.FirstOrDefault();

    Console.WriteLine("");
    Console.WriteLine("Resultado de consulta");
    Console.WriteLine(recordResponseData);

    

    //Actualizacion de un lead
    PutRecordRequest requestRecord = new()
    {
        Data = new List<PutData>()
    {
        new PutData()
        {
            Id = recordToUpdate.Id,
            Company = "Test Final"
        }
    }
    };

    recordResponse = await client.PutAsJsonAsync($"{putRecordsEndpoint}/{recordToUpdate.Id}", requestRecord);
    recordResponseData = await recordResponse.Content.ReadAsStringAsync();

    Console.WriteLine("");
    Console.WriteLine("Resultado de actualizacion");
    Console.WriteLine(recordResponseData);
}