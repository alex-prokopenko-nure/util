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
    class ClientsControllerTests : ControllerTests
    {
        private Client testClient1;
        private Client testClient2;
        private Guid testId;
        private Guid unexistingId;

        public ClientsControllerTests() : base()
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
            testClient1 = new Client { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc697"), Name = "Jihn Sniw" };
            testClient2 = new Client { Id = Guid.Parse("ffe5e70a-8338-4135-85d5-0fbe348cc695"), Name = "Jick Blick" };
        }

        [Test]
        public async Task GetClients_ResultAlwaysReturned()
        {
            var response = await client.GetAsync("clients");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unknown error occured");
            }
            var model = JsonConvert.DeserializeObject<List<Client>>(await response.Content.ReadAsStringAsync());
            Assert.That(model[0].Id == testClient1.Id && model[1].Id == testClient2.Id);
        }

        [Test]
        public async Task GetClient_ExistingClient_ClientReturned()
        {
            var response = await client.GetAsync($"clients/{testId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Client hasn't been found (maybe you hadn't added it before?)");
            }
            var model = JsonConvert.DeserializeObject<Client>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testClient1.Id);
        }

        [Test]
        public async Task GetClient_UnexistingClient_NotFoundReturned()
        {
            var response = await client.GetAsync($"clients/{unexistingId}");
            if(response.IsSuccessStatusCode)
            {
                throw new Exception("This record shouldn't exist in database");
            }
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Test]
        public async Task AddClient_UnexistingClient_ClientReturned()
        {
            var response = await client.PostAsync("clients", new StringContent(JsonConvert.SerializeObject(testClient2), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Client adding failed (maybe this client already exists?)");
            }
            var model = JsonConvert.DeserializeObject<Tour>(await response.Content.ReadAsStringAsync());
            Assert.That(model.Id == testClient2.Id);
        }

        [Test]
        public async Task AddClient_ExistingClient_BadRequestReturned()
        {
            await client.PostAsync("clients", new StringContent(JsonConvert.SerializeObject(testClient1), Encoding.UTF8, "application/json"));
            var response = await client.PostAsync("clients", new StringContent(JsonConvert.SerializeObject(testClient1), Encoding.UTF8, "application/json"));
            if(response.IsSuccessStatusCode)
            {
                throw new Exception("Dubbed adding of one client should've generated BadRequest (maybe clients are different?)");
            }
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }
    }
}
