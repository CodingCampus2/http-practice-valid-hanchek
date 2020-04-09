using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Client
{
    class MyClient
    {
        public MyClient()
        {
            m_Client = new HttpClient();
            m_Client.BaseAddress = new Uri("http://localhost:5000/somedata/");
            m_Client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            m_Client.DefaultRequestHeaders.TryAddWithoutValidation("appId", "campus-task");
        }

        public async Task GetDataByID(string id) => await MakeRequest(new HttpRequestMessage(HttpMethod.Get, id));
        public async Task GetAllData(bool isSorted) => await MakeRequest(new HttpRequestMessage(HttpMethod.Get, isSorted ? "?sorted=True" : ""));
        public async Task PostData(string data)
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, "");
            msg.Content = new StringContent(data, Encoding.UTF8, "application/json");
            await MakeRequest(msg);
        }
        public async Task PutData(string ID, string data)
        {
            HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Put, ID);
            msg.Content = new StringContent(data, Encoding.UTF8, "application/json");
            await MakeRequest(msg);
        }

        public async Task DeleteData(string ID) => await MakeRequest(new HttpRequestMessage(HttpMethod.Delete, ID));

        public async Task MakeRequest(HttpRequestMessage msg)
        {
            HttpResponseMessage result = await m_Client.SendAsync(msg);
            if (result.RequestMessage.Method.Equals(HttpMethod.Get))
            {
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine(await result.Content.ReadAsStringAsync());
                }
                else
                {
                    Console.WriteLine($"Status code: {result.StatusCode}");
                    Console.WriteLine($"Message: { await result.Content.ReadAsStringAsync()}");
                }
            }
            else
            {
                Console.WriteLine($"Status code: {result.StatusCode}");
                if (!result.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Message: { await result.Content.ReadAsStringAsync()}");
                }
            }
            
        }

        private HttpClient m_Client;
    }
}
