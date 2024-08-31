using System.Net;

namespace HttpClientFactoryExample.Interfaces
{
    public interface IHttpClientFactory
    {
        HttpClient CreateHttpClient<T>(bool defaultRequestHeaders = true);
        HttpClient CreateHttpClient<T>(WebProxy proxy, bool defaultRequestHeaders = true);
    }
}
