using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace miniAuth
{
    public interface IOAuthStrategy
    {
        void SetBackChannel(HttpClient client);
        Task StartOAuthAsync(HttpContext context);
        Task ProcessCallbackAsync(HttpContext context);
        OAuthStrategyOptions GetOptions();

        string AuthScheme { get; }
    }
}
