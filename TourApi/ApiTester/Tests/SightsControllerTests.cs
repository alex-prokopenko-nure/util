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
    class SightsControllerTests
    {
        TestServer server;
        HttpClient client;
        private Sight testSight1;
        private Sight testSight2;

        public SightsControllerTests()
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
            testSight1 = new Sight { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc697"), Name = "Jihn Sniw" };
            testSight2 = new Sight { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc695"), Name = "Jick Blick" };
        }

        [Test]
        public async Task GetSights_ResultAlwaysReturned()
        {
            var response = await client.GetAsync("sights");
            var model = JsonConvert.DeserializeObject<List<Sight>>(await response.Content.ReadAsStringAsync());
            Assert.That(model[0].Id == testSight1.Id && model[1].Id == testSight2.Id);
        }

        [Test]
        public async Task GetSights_ExistingSight_SightReturned()
        {
            await client.PostAsync("excursionsights", new StringContent(JsonConvert.SerializeObject(new ExcursionSight { SightId = testSight1.Id, ExcursionId = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc698") }), Encoding.UTF8, "application/json"));
            await client.PostAsync("excursionsights", new StringContent(JsonConvert.SerializeObject(new ExcursionSight { SightId = testSight2.Id, ExcursionId = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc698") }), Encoding.UTF8, "application/json"));
            var response = await client.GetAsync("sights/ffe5e70a-8338-4135-85d5-0fbe348cc698");
            var model = JsonConvert.DeserializeObject<List<Sight>>(await response.Content.ReadAsStringAsync());
            Assert.That(model[0].Id == testSight1.Id);
        }

        [Test]
        public async Task GetSights_UnexistingSight_BadRequestReturned()
        {
            var response = await client.GetAsync("sights/ffa5e70a-8338-4135-85d5-0fbe348cc723");
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task AddSight_UnexistingSight_SightReturned()
        {
            var response = await client.PostAsync("sights", new StringContent(JsonConvert.SerializeObject(testSight2), Encoding.UTF8, "application/json"));
            var model = JsonConvert.DeserializeObject<Sight>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testSight2.Id);
        }

        [Test]
        public async Task AddSight_ExistingSight_BadRequestReturned()
        {
            await client.PostAsync("sights", new StringContent(JsonConvert.SerializeObject(testSight1), Encoding.UTF8, "application/json"));
            var response = await client.PostAsync("sights", new StringContent(JsonConvert.SerializeObject(testSight1), Encoding.UTF8, "application/json"));
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
    }
}
