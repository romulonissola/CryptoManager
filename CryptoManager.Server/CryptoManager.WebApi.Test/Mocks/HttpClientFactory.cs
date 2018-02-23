using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.WebApi.Test.Mocks
{
    public class HttpClientFactory
    {
        private HttpClient _client;
        public HttpClientFactory(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(Constants.AplicationTestBaseUri);
        }

        public async Task<HttpResponseMessage> GetAsync(string path)
        {
            return await _client.GetAsync(path);
        }

        public HttpResponseMessage Get(string path)
        {
            return _client.GetAsync(path).Result;
        }

        public void AddHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
        }

        /// <summary>
        /// This method provides a way to post form data to an MVC controller
        /// </summary>
        /// <typeparam name="T">The view model type that will be posted.</typeparam>
        /// <param name="client">The HttpClient that will be posting the data.</param>
        /// <param name="url">The url of the controller</param>
        /// <param name="viewModel">The view model data that will be passed in the request.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync<T>(string url, T viewModel) where T : class
        {
            var list = PrepareData(viewModel);
            return await PostAsync(url, list);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, List<KeyValuePair<string, string>> list)
        {
            var formData = new FormUrlEncodedContent(list);
            return await _client.PostAsync(url, formData);
        }

        public HttpResponseMessage Post<T>(string url, T viewModel) where T : class
        {
            var list = PrepareData(viewModel);

            return Post(url, list);
        }

        public HttpResponseMessage Post(string url, List<KeyValuePair<string, string>> list)
        {
            var formData = new FormUrlEncodedContent(list);
            return _client.PostAsync(url, formData).Result;
        }

        private List<KeyValuePair<string, string>> PrepareData<T>(T viewModel)
        {
            var list = viewModel.GetType()
                .GetProperties()
                .Where(t => !(t.PropertyType.Name.Equals("ICollection`1")))
                .Select(t => new KeyValuePair<string, string>(t.Name, (t.GetValue(viewModel) ?? new object()).ToString()))
                .ToList();

            return list;
        }
    }
}
