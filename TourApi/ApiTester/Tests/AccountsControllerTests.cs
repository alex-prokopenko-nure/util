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
        { }

        [Test]
        public async Task Register_UnexistingAccount_TrueReturned()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aaaa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("accounts/register", stringContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Registration failed (maybe this user already exists?)");
            }
            var model = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            Assert.That(model == true);
        }

        [Test]
        public async Task Register_ExistingAccount_BadRequestReturned()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aaa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            await client.PostAsync("accounts/register", stringContent);
            var response = await client.PostAsync("accounts/register", stringContent);
            if(response.IsSuccessStatusCode)
            {
                throw new Exception("Registration isn't failed (maybe you are trying to register different users?)");
            }
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Login_ExistingAccount_UserReturned()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(new RegisterDto { Email = "aa@ukr.net", FirstName = "Aaaa", LastName = "Aaa", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("accounts/register", stringContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Registration failed (maybe this user is already registered?)");
            }
            var stringContent2 = new StringContent(JsonConvert.SerializeObject(new LoginDto { Email = "aa@ukr.net", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response2 = await client.PostAsync("accounts/login", stringContent2);
            if (!response2.IsSuccessStatusCode)
            {
                throw new Exception("Login failed (maybe you hadn't registered user first?)");
            }
            var model2 = JsonConvert.DeserializeObject<User>(await response2.Content.ReadAsStringAsync());
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {model2.Token}");
            var response3 = await client.GetAsync("tours");
            Assert.That(model2.AppUser.Email == "aa@ukr.net" && response3.IsSuccessStatusCode);
        }

        [Test]
        public async Task Login_UnexistingAccount_NotFoundReturned()
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(new LoginDto { Email = "aaaaaaa@ukr.net", Password = "aaaaaa" }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("accounts/login", stringContent);
            if(response.IsSuccessStatusCode)
            {
                throw new Exception("There is registered account that shouldn't exist in DB");
            }
            Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }
    }
}
