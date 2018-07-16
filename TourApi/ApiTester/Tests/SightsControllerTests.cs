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
    class SightsControllerTests : ControllerTests
    {
        private Sight testSight1;
        private Sight testSight2;
        private Guid testId;
        private Guid unexistingId;
        private Guid excursionId;

        public SightsControllerTests() : base()
        {
            apiRoute = "sights";
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aaaa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response = client.PostAsync("accounts/register", stringContent);
            var jsonString = response.Result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<bool>(jsonString.Result);
            var stringContent2 = new StringContent(JsonConvert.SerializeObject(new LoginDto { Email = "aaaa@ukr.net", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response2 = client.PostAsync("accounts/login", stringContent2);
            var jsonString2 = response2.Result.Content.ReadAsStringAsync();
            var model2 = JsonConvert.DeserializeObject<User>(jsonString2.Result);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {model2.Token}");
            testId = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc697");
            unexistingId = Guid.Parse("ffa5e70a-8338-4135-85d5-0fbe348cc697");
            excursionId = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc698");
            testSight1 = new Sight { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc697"), Name = "Jihn Sniw" };
            testSight2 = new Sight { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc695"), Name = "Jick Blick" };
        }

        [Test]
        public async Task GetSights_ResultAlwaysReturned()
        {
            var response = await client.GetAsync(apiRoute);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var model = JsonConvert.DeserializeObject<List<Sight>>(await response.Content.ReadAsStringAsync());
            Assert.That(model[0].Id == testSight1.Id && model[1].Id == testSight2.Id);
        }

        [Test]
        public async Task GetSights_ExistingSight_SightReturned()
        {
            var addResponse = await client.PostAsync("excursionsights", new StringContent(JsonConvert.SerializeObject(new ExcursionSight { SightId = testSight1.Id, ExcursionId = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc698") }), Encoding.UTF8, "application/json"));
            var addResponse2 = await client.PostAsync("excursionsights", new StringContent(JsonConvert.SerializeObject(new ExcursionSight { SightId = testSight2.Id, ExcursionId = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc698") }), Encoding.UTF8, "application/json"));
            Assert.That(addResponse.IsSuccessStatusCode == addResponse2.IsSuccessStatusCode == true);
            var response = await client.GetAsync($"{apiRoute}/{excursionId}");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var model = JsonConvert.DeserializeObject<List<Sight>>(await response.Content.ReadAsStringAsync());
            Assert.That(model[0].Id == testSight1.Id);
        }

        [Test]
        public async Task GetSights_UnexistingSight_NotFoundReturned()
        {
            var response = await client.GetAsync($"{apiRoute}/{unexistingId}");
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task AddSight_UnexistingSight_SightReturned()
        {
            var response = await client.PostAsync(apiRoute, new StringContent(JsonConvert.SerializeObject(testSight2), Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.IsSuccessStatusCode);
            var model = JsonConvert.DeserializeObject<Sight>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testSight2.Id);
        }

        [Test]
        public async Task AddSight_ExistingSight_BadRequestReturned()
        {
            var response = await client.PostAsync(apiRoute, new StringContent(JsonConvert.SerializeObject(testSight1), Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.IsSuccessStatusCode);
            var dubbedResponse = await client.PostAsync(apiRoute, new StringContent(JsonConvert.SerializeObject(testSight1), Encoding.UTF8, "application/json"));
            Assert.IsFalse(dubbedResponse.IsSuccessStatusCode);
            Assert.That(dubbedResponse.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
    }
}
