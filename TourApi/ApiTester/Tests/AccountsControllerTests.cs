using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourApi.Helpers;
using TourApi.ViewModels;

namespace ApiTester.Tests
{
    [TestFixture]
    class AccountsControllerTests : ControllerTests
    {
        public AccountsControllerTests() : base()
        {
            apiRoute = "accounts";
        }

        [Test]
        public async Task Register_UnexistingAccount_TrueReturned()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aaaa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{apiRoute}/register", stringContent);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var model = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            Assert.That(model == true);
        }

        [Test]
        public async Task Register_ExistingAccount_BadRequestReturned()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aaa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var addResponse = await client.PostAsync($"{apiRoute}/register", stringContent);
            Assert.IsTrue(addResponse.IsSuccessStatusCode);
            var response = await client.PostAsync($"{apiRoute}/register", stringContent);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Login_ExistingAccount_UserReturned()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{apiRoute}/register", stringContent);
            Assert.IsTrue(response.IsSuccessStatusCode);
            var stringContent2 = new StringContent(JsonConvert.SerializeObject(new LoginDto { Email = "aa@ukr.net", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response2 = await client.PostAsync($"{apiRoute}/login", stringContent2);
            Assert.IsTrue(response2.IsSuccessStatusCode);
            var model2 = JsonConvert.DeserializeObject<User>(await response2.Content.ReadAsStringAsync());
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {model2.Token}");
            var response3 = await client.GetAsync("tours");
            Assert.That(model2.AppUser.Email == "aa@ukr.net" && response3.IsSuccessStatusCode);
        }

        [Test]
        public async Task Login_UnexistingAccount_NotFoundReturned()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(new LoginDto { Email = "aaaaaaa@ukr.net", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{apiRoute}/login", stringContent);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }
    }
}
