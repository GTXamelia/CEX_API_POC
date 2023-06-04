using Newtonsoft.Json;
using System.Text.Json.Serialization;
using models;

public class CatagoriesResponse : Response<productLinesData>
{
}

public class productLinesData
{
    [JsonPropertyName("productLines")]
    public List<productLines> ProductLines { get; set; }

    public productLinesData()
    {
        ProductLines = new List<productLines>();
    }
}

public class productLines
{
    [JsonPropertyName("superCatId")]
    public int SuperCatId { get; set; }

    [JsonPropertyName("productLineId")]
    public int ProductLineId { get; set; }

    [JsonPropertyName("productLineName")]
    public string ProductLineName { get; set; }

    [JsonPropertyName("totalCategories")]
    public int TotalBoxes { get; set; }

    public productLines()
    {
        ProductLineName = string.Empty;
    }
}

public class ProductLines : BaseApi<productLinesData>
{
    private string ENDPOINT;

    public ProductLines() : base()
    {
        this.ENDPOINT = Endpoints.ProductLines.ENDPOINT;
    }

    public async Task<List<productLines>> GetProductLinesAsync(List<int> superCatIds)
    {
        var endpoint = ENDPOINT + $"?superCatIds=[{string.Join(",", superCatIds)}]";
        var categoriesData = await GetResponseAsync(endpoint).ConfigureAwait(false);
        return categoriesData.ProductLines;
    }
}