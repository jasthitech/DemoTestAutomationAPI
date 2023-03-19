using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;

namespace DemoTestAutomationAPI.Helpers
{
    public static class APICalls
    {
        public static HttpClient client;
        public static HttpResponseMessage response;
        public static RestResponse restResponse;
        public static string endpoint = ConfigurationManager.AppSettings["endpoint"];
        public static string postEndpoint = ConfigurationManager.AppSettings["postEndpoint"];

        public static async Task GetUsersAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    response = await client.GetAsync(endpoint);
                    response.EnsureSuccessStatusCode();
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }
        }

        public static async Task GetUsersByIdAsync(int Id)
        {
            client = new HttpClient();
            string endPoint = string.Format("{0}?id={1}", endpoint, Id);
            response = await client.GetAsync(endPoint);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        public static async Task GetRecordsCountAsync(int numberOfRecords)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(responseBody);            
            int? count = (users?.Count);
            Assert.AreEqual(numberOfRecords, count);           
        }

        public static async Task GetNameFromRecordAsync(string expUserName)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(responseBody);
            string? name = users?.FirstOrDefault()?.Name;
            Assert.IsTrue(name.Contains(expUserName));            
        }

        public static void PostNewUserRecord()
        {
            RestClient client = new RestClient(postEndpoint);
            RestRequest request = new RestRequest("users", Method.Post);

            // Set request headers
            request.AddHeader("Content-Type", "application/json");

            // Set request body
            request.AddJsonBody(new
            {
                id = 11,
                name = "Steve Macaine",
                username = "Steve_Macaine",
                email = "steve@rosamond.me",
                address = new
                {
                    street = "XYZ Summit",
                    suite = "Suite 729",
                    city = "Aliyaview",
                    zipcode = "45169",
                    geo = new
                    {
                        lat = "-14.3990",
                        lng = "-120.7677"
                    }
                },
                phone = "486.493.6943 x140",
                website = "minimaxi.com",
                company = new
                {
                    name = "TechW Group",
                    catchPhrase = "Implemented secondary concept",
                    bs = "e-enable extensible e-tailers"
                }
            });

            // Send the request and get the response
            restResponse = client.Execute(request);
              
        }

        public static void ValidatePostStatusCode()
        {
            // Check the response status code and message
            Assert.AreEqual(201, (int)restResponse.StatusCode);
            Assert.AreEqual("Created", restResponse.StatusDescription);
        }

        public static void ValidatePostResponseBody()
        {
            // Check the response body contains the posted data
            var responseBody = restResponse.Content;
            Assert.IsTrue(responseBody?.Contains("Steve Macaine"));
            Assert.IsTrue(responseBody?.Contains("11"));
            Assert.IsTrue(responseBody?.Contains("steve@rosamond.me"));
        }

    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        // add other properties as needed
    }
}
