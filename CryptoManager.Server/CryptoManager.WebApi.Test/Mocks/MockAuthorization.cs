using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.WebApi.Test.Mocks
{
    public class MockAuthorization
    {
        private static Object _thisLock = new Object();
        private static string _token;
        public static string GetValidToken()
        {
            lock (_thisLock)
            {
                if (string.IsNullOrWhiteSpace(_token))
                {
                    HttpClientFactory client = new HttpClientFactory(MockStartup<Startup>.Instance.GetCliente());
                    string path = $"/api/Account/ExternalLoginFacebook?accessToken={TestWebUtil.FacebookAccessToken}";
                    var result = client.PostAsync(path).Result;

                    _token = (result.Content.ReadAsStringAsync()).Result.Replace("\"", "");
                }
            }
            return _token;
        }
    }
}
