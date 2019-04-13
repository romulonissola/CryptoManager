using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoManager.WebApi.Test.Mocks
{
    public class MockAuthorization
    {
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1,1);
        private static string _token;
        public static async Task<string> GetValidTokenAsync()
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                if (string.IsNullOrWhiteSpace(_token))
                {
                    HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
                    string path = $"/api/Account/ExternalLoginFacebook?accessToken={TestWebUtil.FacebookAccessToken}";
                    var result = await client.PostAsync(path);

                    _token = (await result.Content.ReadAsStringAsync()).Replace("\"", "");
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }
            return _token;
        }
    }
}
