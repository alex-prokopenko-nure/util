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
    class ToursControllerTests : ControllerTests
    {
        private Tour testTour1;
        private Tour testTour2;
        private Guid testId;
        private Guid unexistingId;

        public ToursControllerTests() : base()
        {
            apiRoute = "tours";
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
            testTour1 = new Tour { Id = testId, Date = DateTimeOffset.Now, ClientId = Guid.Parse("0f7ef80b-36f6-42b1-996e-421664c7ada7"), ExcursionId = Guid.Parse("57a55c05-b671-4edd-b63f-92c9561e4364") };
            testTour2 = new Tour { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc695"), Date = DateTimeOffset.Now, ClientId = Guid.Parse("0f7ef80b-36f6-42b1-996e-421664c7ada5"), ExcursionId = Guid.Parse("57a55c05-b671-4edd-b63f-92c9561e4365") };
        }

        [Test]
        public async Task GetTours_ResultAlwaysReturned()
        {
            var response = await client.GetAsync(apiRoute);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var model = JsonConvert.DeserializeObject<List<Tour>>(await response.Content.ReadAsStringAsync());
            Assert.That(model[0].Id == testTour1.Id && model[1].Id == testTour2.Id);
        }

        [Test]
        public async Task GetTour_ExistingTour_TourReturned()
        {
            var response = await client.GetAsync($"{apiRoute}/{testId}");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var model = JsonConvert.DeserializeObject<Tour>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testTour1.Id);
        }

        [Test]
        public async Task GetTour_UnexistingTour_NotFoundReturned()
        {
            var response = await client.GetAsync($"{apiRoute}/{unexistingId}");
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task AddTour_UnexistingTour_TourReturned()
        {
            var response = await client.PostAsync(apiRoute, new StringContent(JsonConvert.SerializeObject(testTour2), Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.IsSuccessStatusCode);
            var model = JsonConvert.DeserializeObject<Tour>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testTour2.Id);
        }

        [Test]
        public async Task AddTour_ExistingTour_BadRequestReturned()
        {
            var response = await client.PostAsync(apiRoute, new StringContent(JsonConvert.SerializeObject(testTour1), Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.IsSuccessStatusCode);
            var dubbedResponse = await client.PostAsync(apiRoute, new StringContent(JsonConvert.SerializeObject(testTour1), Encoding.UTF8, "application/json"));
            Assert.IsFalse(dubbedResponse.IsSuccessStatusCode);
            Assert.That(dubbedResponse.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task ChangeTour_ExistingTour_TourReturned()
        {
            var response = await client.PutAsync($"{apiRoute}/{testTour1.Id}", new StringContent(JsonConvert.SerializeObject(testTour1), Encoding.UTF8, "application/json"));
            Assert.IsTrue(response.IsSuccessStatusCode);
            var model = JsonConvert.DeserializeObject<Tour>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testTour1.Id);
        }

        [Test]
        public async Task ChangeTour_UnexistingTour_NotFoundReturned()
        {
            var response = await client.PutAsync($"{apiRoute}/{unexistingId}", new StringContent(JsonConvert.SerializeObject(testTour1), Encoding.UTF8, "application/json"));
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task DeleteTour_ExistingTour_TourReturned()
        {
            var response = await client.DeleteAsync($"{apiRoute}/{testTour2.Id}");
            Assert.IsTrue(response.IsSuccessStatusCode);
            var model = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
            await client.PostAsync(apiRoute, new StringContent(JsonConvert.SerializeObject(testTour2), Encoding.UTF8, "application/json"));
            Assert.That(model == testTour2.Id);
        }

        [Test]
        public async Task DeleteTour_UnexistingTour_NotFoundReturned()
        {
            var response = await client.DeleteAsync($"{apiRoute}/{unexistingId}");
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }
    }
}
