using static mango.Web.SD;

namespace mango.Web.Models
{
    public class ApiRequest
    {
        public APiType ApiType { get; set; }
        public string? Url { get; set; }
        public object? Data { get; set; }
        public string? AccessToken { get; set; }
    }
}
