## Simple HttpClientFactory and ProxyFactory Example
### Usage (No Proxy)
```C#
HttpClientFactory httpClientFactory = new();
HttpClient? noProxyClient = httpClientFactory.CreateHttpClient<Program>();
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
```

### Usage (With Proxy)
```C#
ProxyAccountModel? proxyAccount = SettingsHelper.ReadSettings<ProxyAccountModel>(); //use the settings.json in the repo for testing
WebProxy? proxy = ProxyFactory.CreateProxy(proxyAccount); 
HttpClientFactory httpClientFactory = new();
HttpClient? proxyClient = httpClientFactory.CreateHttpClient<Program>(proxy);
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
}
```
