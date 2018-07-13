using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using TourApi.Repos;

namespace TourApi.Controllers.Tests
{
    [TestFixture]
    public class ClientsControllerTest
    {
        Mock<IClientsRepository> mockClientRepository;
        ClientsController clientsController;
        [Test]
        public void GetClients_ReturnsOkResult()
        {
            Client client = new Client { Name = "John Snow" };
            Client client2 = new Client { Name = "Jack Black" };
            mockClientRepository = new Mock<IClientsRepository>(MockBehavior.Strict);
            mockClientRepository.Setup(tr => tr.GetAll()).Returns(Task.Run(() => {
                return new List<Client> { client, client2 };
            }));
            clientsController = new ClientsController(mockClientRepository.Object);
            Assert.That(clientsController.GetClients().Result is OkObjectResult);
        }
        [TestCase("12355a22-44f6-4e78-0cfa-08d5e56ebea0")]
        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0")]
        public void GetClient_ExistingClient_ReturnsResult(string guid)
        {
            Guid id = Guid.Parse(guid);
            Client client = new Client { Id = Guid.Parse("12355a22-44f6-4e78-0cfa-08d5e56ebea0"), Name = "John Snow" };
            Client client2 = new Client { Id = Guid.NewGuid(), Name = "Jack Black" };
            List<Client> clients = new List<Client> { client, client2 };
            mockClientRepository = new Mock<IClientsRepository>(MockBehavior.Strict);
            mockClientRepository.Setup(tr => tr.Get(id)).Returns(Task.Run(() => {
                return clients.FirstOrDefault(x => x.Id == id);
            }));
            clientsController = new ClientsController(mockClientRepository.Object);
            Assert.That(clientsController.GetClient(id).Result is BadRequestResult || (mockClientRepository.Object.Get(id) != null && clientsController.GetClient(id).Result is OkObjectResult));
        }
        [TestCase("12355a22-44f6-4e78-0cfa-08d5e56ebea0")]
        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0")]
        public void AddClient_NewClient_ReturnsBadOrOkRequest(string guid)
        {
            Guid id = Guid.Parse(guid);
            Client input = new Client { Id = id };
            List<Client> clients = new List<Client> { new Client { Id = Guid.Parse("12355a22-44f6-4e78-0cfa-08d5e56ebea0") } };
            mockClientRepository = new Mock<IClientsRepository>(MockBehavior.Strict);
            mockClientRepository.Setup(tr => tr.Create(input)).Returns(Task.Run(() => {
                if (clients.Contains(clients.FirstOrDefault(x => x.Id == input.Id)))
                {
                    throw new Exception();
                }
                return input;
            }));
            clientsController = new ClientsController(mockClientRepository.Object);
            Assert.That(clientsController.AddClient(input).Result is BadRequestResult || (mockClientRepository.Object.Create(input) != null && clientsController.AddClient(input).Result is OkObjectResult));
        }
    }
}
