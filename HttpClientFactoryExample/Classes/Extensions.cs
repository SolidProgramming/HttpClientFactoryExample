namespace HttpClientFactoryExample.Classes
{
    public static class Extensions
    {
        public static async Task<(bool success, string? ipv4)> GetIPv4(this HttpClient httpClient, int cancelAfterSeconds = 5)
        {
            CancellationTokenSource cts = new(TimeSpan.FromSeconds(cancelAfterSeconds));

            try
            {
                string result = await httpClient.GetStringAsync("https://api.ipify.org/", cts.Token);

                return (!string.IsNullOrEmpty(result), result);
            }
            catch(OperationCanceledException ex)
            {
                Console.WriteLine($"{DateTime.Now} | Canceled {nameof(GetIPv4)} after set time {cancelAfterSeconds}s.  {ex.Message}");
                return (false, default);
            }
            catch(TimeoutException ex)
            {
                Console.WriteLine($"{DateTime.Now} | {ex.Message}");
                return (false, default);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} | {ex.Message}");
                return (false, default);
            }
        }
    }
}
