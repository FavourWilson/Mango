using mango.Web.Models;
using Mango.Services.Models;

namespace mango.Web.Service.IService
{
    public interface IBaseService : IDisposable
    {
        ResponseDto response { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
