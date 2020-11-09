﻿using System;
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
        public class Customer
        {
            public static readonly string GetCustomersByWorkId = ServerAddress + "/api/Customer/GetCustomersByWorkId/";
           
        }
        public class Order
        {
            public static readonly string GetOrders = ServerAddress + "/api/Order/GetOrders/";
            public static readonly string GetOrderById = ServerAddress + "/api/Order/GetOrderById/";
            public static readonly string EditOrder = ServerAddress + "/api/Order/EditOrder/";
            public static readonly string AddOrder = ServerAddress + "/api/Order/AddOrder/";
            public static readonly string DeleteOrder = ServerAddress + "/api/Order/DeleteOrder/";
        }
        public class OrderDetail
        {
            public static readonly string GetOrderDetails = ServerAddress + "/api/OrderDetail/GetOrderDetails/";
            public static readonly string GetOrderDetailById = ServerAddress + "/api/OrderDetail/GetOrderDetailById/";
            public static readonly string EditOrderDetail = ServerAddress + "/api/OrderDetail/EditOrderDetail/";
            public static readonly string AddOrderDetail = ServerAddress + "/api/OrderDetail/AddOrderDetail/";
            public static readonly string DeleteOrderDetail = ServerAddress + "/api/OrderDetail/DeleteOrderDetail/";
        }
        public class InventoryType
        {
            public static readonly string GetInventoryTypes = ServerAddress + "/api/InventoryType/GetInventoryTypes/";
            public static readonly string GetInventoryTypeById = ServerAddress + "/api/InventoryType/GetInventoryTypeById/";
            public static readonly string EditInventoryType = ServerAddress + "/api/InventoryType/EditInventoryType/";
            public static readonly string AddInventoryType = ServerAddress + "/api/InventoryType/AddInventoryType/";
            public static readonly string DeleteInventoryType = ServerAddress + "/api/InventoryType/DeleteInventoryType/";
        }
        public class Serving
        {
            public static readonly string GetServings = ServerAddress + "/api/serving/GetServings/";
            public static readonly string GetServingsByWorkId = ServerAddress + "/api/serving/GetServingsByWorkId/";
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
        public class Work
        {
            public static readonly string GetWorks= ServerAddress + "/api/work/GetWorks/";
            public static readonly string GetWorkById = ServerAddress + "/api/work/GetWorkById/";
            public static readonly string EditWork = ServerAddress + "/api/work/EditWork/";
            public static readonly string AddWork = ServerAddress + "/api/work/AddWork/";
            public static readonly string DeleteWork = ServerAddress + "/api/work/DeleteWork/";
        }

    }
}
