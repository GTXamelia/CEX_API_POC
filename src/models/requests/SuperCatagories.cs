using Newtonsoft.Json;
using models;

public class SuperCatagoriesResponse : Response<SuperCatagoriesData>
{
}

public class SuperCatagoriesData
{
    [JsonProperty("superCats")]
    public List<SuperCat> SuperCats { get; set; }
}

public class SuperCat
{
    [JsonProperty("superCatId")]
    public int SuperCatId { get; set; }

    [JsonProperty("superCatFriendlyName")]
    public string SuperCatFriendlyName { get; set; }
}

public class SuperCatagories : BaseApi<SuperCatagoriesData>
{
    private string ENDPOINT;

    public SuperCatagories() : base()
    {
        this.ENDPOINT = Endpoints.SuperCategories.ENDPOINT;
    }

    public async Task<List<SuperCat>> GetSuperCatsAsync()
    {
        var endpoint = "/supercats";
        var superCatagoriesData = await GetResponseAsync(ENDPOINT).ConfigureAwait(false);
        return superCatagoriesData.SuperCats;
    }
}