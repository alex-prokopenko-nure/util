using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using TourApi;
using TourApi.ViewModels;
using System.Threading;
using Microsoft.Extensions.Configuration;
using System.IO;
using TourApi.Models;
using TourApi.Helpers;

namespace ApiTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Guid.NewGuid());
            Console.WriteLine(Guid.NewGuid());
            Console.WriteLine(Guid.NewGuid());
            Request();
            Thread.Sleep(5000);
        }
        public static async void Request()
        {
            TestServer server;
            HttpClient client;
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            await client.PostAsync("accounts/register", stringContent);
            var stringContent2 = new StringContent(JsonConvert.SerializeObject(new LoginDto { Email = "aa@ukr.net", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response2 = await client.PostAsync("accounts/login", stringContent2);
            var model2 = JsonConvert.DeserializeObject<User>(await response2.Content.ReadAsStringAsync());
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration Configuration = builder.Build();
            Console.WriteLine(model2.AppUser.Email);
            Console.WriteLine(model2.Token);
            Console.WriteLine(JwtGenerator.GenerateJwtToken("aa@ukr.net", model2.AppUser, Configuration));
        }
    }
}
