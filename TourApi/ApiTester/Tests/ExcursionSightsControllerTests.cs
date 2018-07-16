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
    class ExcursionSightsControllerTests : ControllerTests
    {
        private ExcursionSight testExcursionSight1;
        private ExcursionSight testExcursionSight2;
        private Guid unexistingExcursionId;
        private Guid unexistingSightId;

        public ExcursionSightsControllerTests() : base()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aaaa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response = client.PostAsync("accounts/register", stringContent);
            var jsonString = response.Result.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<bool>(jsonString.Result);
            var stringContent2 = new StringContent(JsonConvert.SerializeObject(new LoginDto { Email = "aaaa@ukr.net", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response2 = client.PostAsync("accounts/login", stringContent2);
            var jsonString2 = response2.Result.Content.ReadAsStringAsync();
            var model2 = JsonConvert.DeserializeObject<User>(jsonString2.Result);
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {model2.Token}");
            unexistingExcursionId = Guid.Parse("ffa5e70a-8338-4135-85d5-0fbe348cc697");
            unexistingSightId = Guid.Parse("ffa5e70a-8338-4135-85d5-0fbe348cc693");
            testExcursionSight1 = new ExcursionSight { SightId = Guid.Parse("0f7ef80b-36f6-42b1-996e-421664c7ada7"), ExcursionId = Guid.Parse("57a55c05-b671-4edd-b63f-92c9561e4364") };
            testExcursionSight2 = new ExcursionSight { SightId = Guid.Parse("0f7ef80b-36f6-42b1-996e-421664c7ada5"), ExcursionId = Guid.Parse("57a55c05-b671-4edd-b63f-92c9561e4365") };
        }

        [Test]
        public async Task AddExcursionSight_UnexistingExcursionSight_ExcursionSightReturned()
        {
            var response = await client.PostAsync("excursionsights", new StringContent(JsonConvert.SerializeObject(testExcursionSight2), Encoding.UTF8, "application/json"));
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("Adding failed (maybe this record already exists in DB?)");
            }
            var model = JsonConvert.DeserializeObject<ExcursionSight>(await response.Content.ReadAsStringAsync());
            Assert.That(model.ExcursionId == testExcursionSight2.ExcursionId && model.SightId == testExcursionSight2.SightId);
        }

        [Test]
        public async Task AddExcursionSight_ExistingExcursionSight_BadRequestReturned()
        {
            var response = await client.PostAsync("excursionsights", new StringContent(JsonConvert.SerializeObject(testExcursionSight1), Encoding.UTF8, "application/json"));
            var dubbedResponse = await client.PostAsync("excursionsights", new StringContent(JsonConvert.SerializeObject(testExcursionSight1), Encoding.UTF8, "application/json"));
            if(dubbedResponse.IsSuccessStatusCode)
            {
                throw new Exception("Dubbed adding of one excursionsight should've generated BadRequest (maybe excursionsights are different?)");
            }
            Assert.That(dubbedResponse.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task DeleteExcursionSight_ExistingExcursionSight_ExcursionSightReturned()
        {
            var response = await client.DeleteAsync($"excursionsights/{testExcursionSight2.ExcursionId}/{testExcursionSight2.SightId}");
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("This record doesn't exist in DB (maybe you haven't added it?)");
            }
            var model = JsonConvert.DeserializeObject<Tuple<Guid, Guid>>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Item1 == testExcursionSight2.ExcursionId && model.Item2 == testExcursionSight2.SightId);
        }

        [Test]
        public async Task DeleteExcursionSight_UnexistingExcursionSight_NotFoundReturned()
        {
            var response = await client.DeleteAsync($"excursionsights/{unexistingExcursionId}/{unexistingSightId}");
            if(response.IsSuccessStatusCode)
            {
                throw new Exception("Deleting failed (maybe this record already existed in DB?)");
            }
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }
    }
}
