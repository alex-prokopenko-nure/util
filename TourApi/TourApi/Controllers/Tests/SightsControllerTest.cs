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
    public class SightsControllerTest
    {
        Mock<ISightsRepository> mockSightRepository;
        SightsController sightsController;
        [Test]
        public void GetSights_ReturnsOkResult()
        {
            Sight sight = new Sight { Name = "John Snow" };
            Sight sight2 = new Sight { Name = "Jack Black" };
            mockSightRepository = new Mock<ISightsRepository>(MockBehavior.Strict);
            mockSightRepository.Setup(tr => tr.GetAllSights()).Returns(Task.Run(() => {
                return new List<Sight> { sight, sight2 };
            }));
            sightsController = new SightsController(mockSightRepository.Object);
            Assert.That(sightsController.GetAllSights().Result is OkObjectResult);
        }
        [TestCase("12355a22-44f6-4e78-0cfa-08d5e56ebea0")]
        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0")]
        public void GetSight_ExistingSight_ReturnsResult(string guid)
        {
            Guid id = Guid.Parse(guid);
            Sight sight = new Sight { Id = Guid.Parse("12355a22-44f6-4e78-0cfa-08d5e56ebea0"), Name = "John Snow" };
            Sight sight2 = new Sight { Id = Guid.NewGuid(), Name = "Jack Black" };
            List<Sight> sights = new List<Sight> { sight, sight2 };
            mockSightRepository = new Mock<ISightsRepository>(MockBehavior.Strict);
            mockSightRepository.Setup(tr => tr.GetSights(id)).Returns(Task.Run(() => {
                return sights.Where(x => x.Id == id).ToList();
            }));
            sightsController = new SightsController(mockSightRepository.Object);
            Assert.That(sightsController.GetSights(id).Result is BadRequestResult || (mockSightRepository.Object.GetSights(id) != null && sightsController.GetSights(id).Result is OkObjectResult));
        }
        [TestCase("12355a22-44f6-4e78-0cfa-08d5e56ebea0")]
        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0")]
        public void AddSight_NewSight_ReturnsBadOrOkRequest(string guid)
        {
            Guid id = Guid.Parse(guid);
            Sight input = new Sight { Id = id };
            mockSightRepository = new Mock<ISightsRepository>(MockBehavior.Strict);
            mockSightRepository.Setup(tr => tr.Create(input)).Returns(Task.Run(() => {
                if (input.Id == Guid.Parse("12355a22-44f6-4e78-0cfa-08d5e56ebea0"))
                {
                    throw new Exception();
                }
                return input;
            }));
            sightsController = new SightsController(mockSightRepository.Object);
            Assert.That(sightsController.AddSight(input).Result is BadRequestResult || (mockSightRepository.Object.Create(input) != null && sightsController.AddSight(input).Result is OkObjectResult));
        }
    }
}
