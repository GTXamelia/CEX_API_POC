using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class SuperCat
{
    [JsonProperty("superCatId")]
    public int SuperCatId { get; set; }

    [JsonProperty("superCatFriendlyName")]
    public string SuperCatFriendlyName { get; set; }
}

public class Data
{
    [JsonProperty("superCats")]
    public List<SuperCat> SuperCats { get; set; }
}

public class Response
{
    [JsonProperty("ack")]
    public string Ack { get; set; }

    [JsonProperty("data")]
    public Data Data { get; set; }

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

public class RootObject
{
    [JsonProperty("response")]
    public Response Response { get; set; }
}

public class SuperCatagories
{
    public async Task<List<SuperCat>> GetSuperCatsAsync()
    {
        using var client = new HttpClient();
        var response = await client.GetAsync("https://wss2.cex.uk.webuy.io/v3/supercats");
        var content = await response.Content.ReadAsStringAsync();
        var root = JsonConvert.DeserializeObject<RootObject>(content);
        return root.Response.Data.SuperCats;
    }
}