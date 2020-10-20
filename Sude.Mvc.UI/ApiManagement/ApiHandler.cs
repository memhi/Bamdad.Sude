using System;
using System.Collections.Generic;
 
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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

    public static class ApiAddress
    {
        public static IConfiguration Config { get;  set; }

        public static string ServerAddress { get; set; }
        //protected static readonly string ServerAddress = "http://localhost:2184";
        public class Serving
        {
            public static readonly string GetServings = ServerAddress + "/api/serving/GetServings/";
            public static readonly string GetServingById = ServerAddress + "/api/serving/GetServingById/";
            public static readonly string EditServing = ServerAddress + "/api/serving/EditServing/";
            public static readonly string AddServing = ServerAddress + "/api/serving/AddServing/";
            public static readonly string DeleteServing = ServerAddress + "/api/serving/DeleteServing/";
        }
        public class WorkType
        {
            public static readonly string GetWorkTypes = ServerAddress + "/api/worktype/GetWorkTypes/";
            public static readonly string GetWorkTypeById = ServerAddress + "/api/worktype/GetWorkTypeById/";
            public static readonly string EditWorkType = ServerAddress + "/api/worktype/EditWorkType/";
            public static readonly string AddWorkType = ServerAddress + "/api/worktype/AddWorkType/";
            public static readonly string DeleteWorkType = ServerAddress + "/api/worktype/DeleteWorkType/";
        }
         
    }
}
