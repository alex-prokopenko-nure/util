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
    public class ExcursionsControllerTest
    {
        Mock<IExcursionsRepository> mockExcursionRepository;
        ExcursionsController excursionsController;
        [Test]
        public void GetExcursions_ReturnsOkResult()
        {
            Excursion excursion = new Excursion { Name = "John Snow" };
            Excursion excursion2 = new Excursion { Name = "Jack Black" };
            mockExcursionRepository = new Mock<IExcursionsRepository>(MockBehavior.Strict);
            mockExcursionRepository.Setup(tr => tr.GetAll()).Returns(Task.Run(() => {
                return new List<Excursion> { excursion, excursion2 };
            }));
            excursionsController = new ExcursionsController(mockExcursionRepository.Object);
            Assert.That(excursionsController.GetExcursions().Result is OkObjectResult);
        }
        [TestCase("12355a22-44f6-4e78-0cfa-08d5e56ebea0")]
        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0")]
        public void GetClient_ExistingClient_ReturnsResult(string guid)
        {
            Guid id = Guid.Parse(guid);
            Excursion client = new Excursion { Id = Guid.Parse("12355a22-44f6-4e78-0cfa-08d5e56ebea0"), Name = "John Snow" };
            Excursion client2 = new Excursion { Id = Guid.NewGuid(), Name = "Jack Black" };
            List<Excursion> clients = new List<Excursion> { client, client2 };
            mockExcursionRepository = new Mock<IExcursionsRepository>(MockBehavior.Strict);
            mockExcursionRepository.Setup(tr => tr.Get(id)).Returns(Task.Run(() => {
                return clients.FirstOrDefault(x => x.Id == id);
            }));
            excursionsController = new ExcursionsController(mockExcursionRepository.Object);
            Assert.That(excursionsController.GetExcursion(id).Result is BadRequestResult || (mockExcursionRepository.Object.Get(id) != null && excursionsController.GetExcursion(id).Result is OkObjectResult));
        }
        [TestCase("12355a22-44f6-4e78-0cfa-08d5e56ebea0")]
        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0")]
        public void AddClient_NewClient_ReturnsBadOrOkRequest(string guid)
        {
            Guid id = Guid.Parse(guid);
            Excursion input = new Excursion { Id = id };
            List<Excursion> excursions = new List<Excursion> { new Excursion { Id = Guid.Parse("12355a22-44f6-4e78-0cfa-08d5e56ebea0") } };
            mockExcursionRepository = new Mock<IExcursionsRepository>(MockBehavior.Strict);
            mockExcursionRepository.Setup(tr => tr.Create(input)).Returns(Task.Run(() => {
                if (excursions.Contains(excursions.FirstOrDefault(x => x.Id == input.Id)))
                {
                    throw new Exception();
                }
                return input;
            }));
            excursionsController = new ExcursionsController(mockExcursionRepository.Object);
            Assert.That(excursionsController.AddExcursion(input).Result is BadRequestResult || (mockExcursionRepository.Object.Create(input) != null && excursionsController.AddExcursion(input).Result is OkObjectResult));
        }
    }
}
