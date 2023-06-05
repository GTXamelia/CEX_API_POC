using Newtonsoft.Json;
using Amelia.CEXPOC.models;
using Microsoft.Extensions.Logging;

namespace Amelia.CEXPOC.Api
{
    public class SuperCatagoriesResponse : Response<SuperCatagoriesData>
    {
    }

    public class SuperCatagoriesData
    {
        [JsonProperty("superCats")]
        public List<SuperCat> SuperCats { get; set; }

        public SuperCatagoriesData()
        {
            SuperCats = new List<SuperCat>();
        }
    }

    public class SuperCat
    {
        [JsonProperty("superCatId")]
        public int SuperCatId { get; set; }

        [JsonProperty("superCatFriendlyName")]
        public string SuperCatFriendlyName { get; set; }

        public SuperCat()
        {
            SuperCatFriendlyName = string.Empty;
        }
    }

    public class SuperCatagories : BaseApi<SuperCatagoriesData>
    {
        private string ENDPOINT;

        public SuperCatagories(ILogger<SuperCatagories> logger) : base(logger)
        {
            this.ENDPOINT = Endpoints.SuperCategories.ENDPOINT;
        }

        public async Task<List<SuperCat>> GetSuperCatsAsync()
        {
            var superCatagoriesData = await GetResponseAsync(ENDPOINT).ConfigureAwait(false);
            return superCatagoriesData.SuperCats;
        }
    }
}