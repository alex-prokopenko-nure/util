using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourApi.Models;
using TourApi.ViewModels;

namespace ApiTester.Tests
{
    [TestFixture]
    class ExcursionsControllerTests
    {
        TestServer server;
        HttpClient client;
        private Excursion testExcursion1;
        private Excursion testExcursion2;

        public ExcursionsControllerTests()
        {
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aaaa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response = client.PostAsync("accounts/register", stringContent);
            var jsonString = response.Result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<bool>(jsonString.Result);
            var stringContent2 = new StringContent(JsonConvert.SerializeObject(new LoginDto { Email = "aaaa@ukr.net", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response2 = client.PostAsync("accounts/login", stringContent2);
            var jsonString2 = response2.Result.Content.ReadAsStringAsync();
            var model2 = JsonConvert.DeserializeObject<User>(jsonString2.Result);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {model2.Token}");
            testExcursion1 = new Excursion { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc697"), Name = "Jihn Sniw" };
            testExcursion2 = new Excursion { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc695"), Name = "Jick Blick" };
        }

        [Test]
        public async Task GetExcursions_ResultAlwaysReturned()
        {
            var response = await client.GetAsync("excursions");
            var model = JsonConvert.DeserializeObject<List<Excursion>>(await response.Content.ReadAsStringAsync());
            Assert.That(model[0].Id == testExcursion1.Id && model[1].Id == testExcursion2.Id);
        }

        [Test]
        public async Task GetExcursion_ExistingExcursion_ExcursionReturned()
        {
            var response = await client.GetAsync("excursions/ffe5e70a-8338-4135-85d5-0fbe348cc697");
            var model = JsonConvert.DeserializeObject<Excursion>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testExcursion1.Id);
        }

        [Test]
        public async Task GetExcursion_UnexistingExcursion_BadRequestReturned()
        {
            var response = await client.GetAsync("excursions/ffa5e70a-8338-4135-85d5-0fbe348cc697");
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task AddExcursion_UnexistingExcursion_ExcursionReturned()
        {
            var response = await client.PostAsync("excursions", new StringContent(JsonConvert.SerializeObject(testExcursion2), Encoding.UTF8, "application/json"));
            var model = JsonConvert.DeserializeObject<Tour>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testExcursion2.Id);
        }

        [Test]
        public async Task AddExcursion_ExistingExcursion_BadRequestReturned()
        {
            await client.PostAsync("excursions", new StringContent(JsonConvert.SerializeObject(testExcursion1), Encoding.UTF8, "application/json"));
            var response = await client.PostAsync("excursions", new StringContent(JsonConvert.SerializeObject(testExcursion1), Encoding.UTF8, "application/json"));
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
    }
}
