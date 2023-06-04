using Newtonsoft.Json;
using models;

public class Response<T>
{
    [JsonProperty("response")]
    public ResponseData<T> Data { get; set; }
}

public class ResponseData<T>
{
    [JsonProperty("ack")]
    public string Ack { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }

    [JsonProperty("error")]
    public Error Error { get; set; }
}

public class Error
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("internal_message")]
    public string InternalMessage { get; set; }

    [JsonProperty("moreInfo")]
    public List<object> MoreInfo { get; set; }
}

public abstract class BaseApi<T>
{
    protected async Task<string> GetJsonAsync(string endpoint)
    {
        var url = Endpoints.BASE_URL + endpoint;

        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return json;
        }
    }

    protected async Task<T> GetResponseAsync(string endpoint)
    {
        var json = await GetJsonAsync(endpoint).ConfigureAwait(false);
        var response = JsonConvert.DeserializeObject<Response<T>>(json);
        if (!string.IsNullOrEmpty(response.Data.Error.Code))
        {
            throw new Exception($"API error: {response.Data.Error.InternalMessage}");
        }
        return response.Data.Data;
    }
}