using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sude.Mvc.UI.ApiManagement
{
    public class Api
    {
        private static Api apiHandler = null;

        private Api() { }

        public static Api GetHandler
        {
            get
            {
                if (apiHandler == null)
                    apiHandler = new Api();
                return apiHandler;
            }
        }
        public async Task<T> GetApiAsync<T>(string ActionAddress)
        {
            T resultGet = default(T);
            using (var client = new HttpClient())
            {
                /*client.BaseAddress = new Uri("");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));*/

                //http Get Request
                HttpResponseMessage response = await client.GetAsync(ActionAddress);

                string result = await response.Content.ReadAsStringAsync();
                resultGet = JsonConvert.DeserializeObject<T>(result);

            }

            return resultGet;
        }

        public async Task<T> GetApiAsync<T>(string ActionAddress,object request)
        {
            T resultGet = default(T);
            using (var client = new HttpClient())
            {
                /*client.BaseAddress = new Uri("");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));*/

                /*var JsonData = JsonConvert.SerializeObject(request);
                var requestContent = new StringContent(JsonData, Encoding.UTF8, "application/json");*/

                //http Get Request
                HttpResponseMessage response = await client.PostAsJsonAsync(ActionAddress, request);//.PostAsync(ActionAddress,requestContent);

                string result = await response.Content.ReadAsStringAsync();
                resultGet = JsonConvert.DeserializeObject<T>(result);

            }

            return resultGet;
        }
    }

    public class ApiAddress
    {
        private const string ServerAddress = "https://localhost:5001";
        public const string GetServings = ServerAddress + "/api/serving/GetServings/";
        public const string GetServingById = ServerAddress + "/api/serving/GetServingById/";
        public const string EditServing = ServerAddress + "/api/serving/EditServing/";
        public const string AddServing = ServerAddress + "/api/serving/AddServing/";
        public const string DeleteServing = ServerAddress + "/api/serving/DeleteServing/";
    }
}
