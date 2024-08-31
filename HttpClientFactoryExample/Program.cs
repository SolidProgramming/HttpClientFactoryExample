global using HttpClientFactoryExample.MockServicesAndClasses;
global using HttpClientFactoryExample.Classes;
global using HttpClientFactoryExample.Models;
using System.Net;

//For ASP.NET(Core) / Dependency Injection
//builder.Services.AddSingleton<Interfaces.IHttpClientFactory, HttpClientFactory>();
//Interfaces.IHttpClientFactory? httpClientFactory = app.Services.GetRequiredService<Interfaces.IHttpClientFactory>(); (Optional, only needed if you want to use the factory in program.cs)

HttpClientFactory httpClientFactory = new();

HttpClient? noProxyClient = httpClientFactory.CreateHttpClient<Program>();
ProxyAccountModel? proxyAccount = SettingsHelper.ReadSettings<ProxyAccountModel>();
WebProxy? proxy = null;
HttpClient? proxyClient;

(bool successNoProxy, string? ipv4NoProxy) = await noProxyClient.GetIPv4();
if (!successNoProxy)
{
    Console.WriteLine($"{DateTime.Now} | HttpClient could not retrieve WAN IP Address. Shutting down...");
    return;
}
else
{
    Console.WriteLine($"{DateTime.Now} | Your WAN IP: {ipv4NoProxy}");
}

if (proxyAccount is not null)
{
    proxy = ProxyFactory.CreateProxy(proxyAccount);

    if (proxy is null)
    {
        Console.WriteLine($"{DateTime.Now} | Could not create 'WebProxy'. Shutting down...");
        return;
    }
    else
    {
        proxyClient = httpClientFactory.CreateHttpClient<Service1>(proxy);
        //proxyClient = httpClientFactory.CreateHttpClient<MockApi1>(proxy);
        //proxyClient = httpClientFactory.CreateHttpClient<Program>(proxy);
    }

    (bool successProxyClient, string? ipv4ProxyClient) = await proxyClient.GetIPv4(cancelAfterSeconds: 10);
    if (!successProxyClient)
    {
        Console.WriteLine($"{DateTime.Now} | HttpClient could not retrieve WAN IP Address. Shutting down...");
        Console.ReadKey();
        return;
    }
    else
    {
        Console.WriteLine($"{DateTime.Now} | WAN IP of {nameof(proxyClient)}: {ipv4ProxyClient}");
        Console.ReadKey();
        return;
    }
}