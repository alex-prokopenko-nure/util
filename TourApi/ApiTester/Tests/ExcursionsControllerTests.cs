﻿using Microsoft.AspNetCore.Hosting;
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
    class ExcursionsControllerTests : ControllerTests
    {
        private Excursion testExcursion1;
        private Excursion testExcursion2;
        private Guid testId;
        private Guid unexistingId;

        public ExcursionsControllerTests() : base()
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
            testId = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc697");
            unexistingId = Guid.Parse("ffa5e70a-8338-4135-85d5-0fbe348cc697");
            testExcursion1 = new Excursion { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc697"), Name = "Jihn Sniw" };
            testExcursion2 = new Excursion { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc695"), Name = "Jick Blick" };
        }

        [Test]
        public async Task GetExcursions_ResultAlwaysReturned()
        {
            var response = await client.GetAsync("excursions");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unknown error occured");
            }
            var model = JsonConvert.DeserializeObject<List<Excursion>>(await response.Content.ReadAsStringAsync());
            Assert.That(model[0].Id == testExcursion1.Id && model[1].Id == testExcursion2.Id);
        }

        [Test]
        public async Task GetExcursion_ExistingExcursion_ExcursionReturned()
        {
            var response = await client.GetAsync($"excursions/{testId}");
            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("This excursion doesn't exist in DB (maybe you haven't added it before?)");
            }
            var model = JsonConvert.DeserializeObject<Excursion>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testExcursion1.Id);
        }

        [Test]
        public async Task GetExcursion_UnexistingExcursion_NotFoundReturned()
        {
            var response = await client.GetAsync($"excursions/{unexistingId}");
            if(response.IsSuccessStatusCode)
            {
                throw new Exception("This record shouldn't have existed in DB");
            }
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task AddExcursion_UnexistingExcursion_ExcursionReturned()
        {
            var response = await client.PostAsync("excursions", new StringContent(JsonConvert.SerializeObject(testExcursion2), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("This record already existed in DB (maybe you've added it before?)");
            }
            var model = JsonConvert.DeserializeObject<Tour>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testExcursion2.Id);
        }

        [Test]
        public async Task AddExcursion_ExistingExcursion_BadRequestReturned()
        {
            var response = await client.PostAsync("excursions", new StringContent(JsonConvert.SerializeObject(testExcursion1), Encoding.UTF8, "application/json"));
            var dubbedResponse = await client.PostAsync("excursions", new StringContent(JsonConvert.SerializeObject(testExcursion1), Encoding.UTF8, "application/json"));
            if (dubbedResponse.IsSuccessStatusCode)
            {
                throw new Exception("Dubbed adding of one excursion should've generated BadRequest (maybe excursions are different?)");
            }
            Assert.That(dubbedResponse.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
    }
}
