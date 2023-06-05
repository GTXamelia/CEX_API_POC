using Newtonsoft.Json;
using models;
using Microsoft.Extensions.Logging;

namespace Amelia.CEXPOC.Api
{
    public class Response<T>
    {
        [JsonProperty("response")]
        public ResponseData<T> ResponseData { get; set; }

        public Response()
        {
            ResponseData = new ResponseData<T>();
        }
    }

    public class ResponseData<T>
    {
        [JsonProperty("ack")]
        public string Ack { get; set; }

        [JsonProperty("data")]
        public T? Data { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }

        public ResponseData()
        {
            Ack = string.Empty;
            Error = new Error();
        }
    }

    public class Error
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("internal_message")]
        public string InternalMessage { get; set; }

        [JsonProperty("moreInfo")]
        public List<object> MoreInfo { get; set; }

        public Error()
        {
            Code = string.Empty;
            InternalMessage = string.Empty;
            MoreInfo = new List<object>();
        }
    }

    public abstract class BaseApi<T>
    {
        private readonly ILogger<BaseApi<T>> _logger;

        protected BaseApi(ILogger<BaseApi<T>> logger)
        {
            _logger = logger;
        }

        protected async Task<string> GetJsonAsync(string endpoint)
        {
            var url = Endpoints.BASE_URL + endpoint;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("API error: {StatusCode} {ReasonPhrase}", response.StatusCode, response.ReasonPhrase);
                    throw new Exception($"API error: {response.StatusCode} {response.ReasonPhrase}");
                }
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return json;
            }
        }

        protected async Task<T> GetResponseAsync(string endpoint)
        {
            var json = await GetJsonAsync(endpoint).ConfigureAwait(false);
            var response = JsonConvert.DeserializeObject<Response<T>>(json);
            if (!string.IsNullOrEmpty(response?.ResponseData.Error.Code))
            {
                _logger.LogError("API error: {ErrorMessage}", response.ResponseData.Error.InternalMessage);
                throw new Exception($"API error: {response.ResponseData.Error.InternalMessage}");
            }

            return response.ResponseData!.Data;
        }
    }
}