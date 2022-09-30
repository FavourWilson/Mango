using mango.Web.Models;
using mango.Web.Service.IService;
using Mango.Services.Models;
using Newtonsoft.Json;
using System.Text;

namespace mango.Web.Service
{
    public class BaseService : IBaseService
    {
        public ResponseDto? response { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
            this.response = new ResponseDto();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MangoAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
#pragma warning disable CS8604 // Possible null reference argument.
                message.RequestUri = new Uri(apiRequest.Url);
#pragma warning restore CS8604 // Possible null reference argument.
                client.DefaultRequestHeaders.Clear();

                if(apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case SD.APiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case SD.APiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.APiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.APiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);

#pragma warning disable CS8603 // Possible null reference return.
                return apiResponseDto;
#pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false,
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
#pragma warning disable CS8603 // Possible null reference return.
                return apiResponseDto;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }
    }
}
