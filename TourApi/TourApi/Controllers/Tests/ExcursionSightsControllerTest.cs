using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourApi.Models;
using TourApi.Repos;
using TourApi.ViewModels;

namespace TourApi.Controllers.Tests
{
    [TestFixture]
    public class ExcursionSightsControllerTest
    {
        Mock<IExcursionSightRepository> mockExcursionSightRepository;
        ExcursionSightsController excursionSightsController;

        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0", "b38a98f9-61fd-4484-af1b-08d5e56ebea2")]
        [TestCase("49155a22-44f6-4e78-0cfa-08d5e56ebea0", "b38a98f9-61fd-4484-af1b-08d5e56ebea1")]
        public void AddExcursionSight_NewExcursionSight_ReturnsBadOrOkRequest(string excursionGuid, string sightGuid)
        {
            ExcursionSight input = new ExcursionSight {SightId = Guid.Parse(sightGuid), ExcursionId = Guid.Parse(excursionGuid) };
            Guid guid = Guid.NewGuid();
            ExcursionSight excursionSight = new ExcursionSight { SightId = Guid.Parse("b38a98f9-61fd-4484-af1b-08d5e56ebea2"), ExcursionId = Guid.Parse("49255a22-44f6-4e78-0cfa-08d5e56ebea0") };
            List<ExcursionSight> excursionSights = new List<ExcursionSight> { excursionSight };
            mockExcursionSightRepository = new Mock<IExcursionSightRepository>(MockBehavior.Strict);
            mockExcursionSightRepository.Setup(tr => tr.Create(Guid.Parse(excursionGuid), Guid.Parse(sightGuid))).Returns(Task.Run(() => {
                if(excursionSights.Contains(input))
                    return null;
                return input;
            }));
            excursionSightsController = new ExcursionSightsController(mockExcursionSightRepository.Object);
            Assert.That(excursionSightsController.AddExcursionSight(new Pair { ExcursionId = Guid.Parse(excursionGuid), SightId = Guid.Parse(sightGuid) }).Result is BadRequestResult || (mockExcursionSightRepository.Object.Create(Guid.Parse(excursionGuid), Guid.Parse(sightGuid)) != null && excursionSightsController.AddExcursionSight(new Pair{ ExcursionId = Guid.Parse(excursionGuid), SightId = Guid.Parse(sightGuid)}).Result is OkObjectResult));
        }

        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0", "b38a98f9-61fd-4484-af1b-08d5e56ebea2")]
        [TestCase("49155a22-44f6-4e78-0cfa-08d5e56ebea0", "b38a98f9-61fd-4484-af1b-08d5e56ebea1")]
        public void DeleteExcursionSight_ExistingOrNotExcursionSight_ReturnsBadOrOkRequest(string excursionGuid, string sightGuid)
        {
            ExcursionSight input = new ExcursionSight { SightId = Guid.Parse(sightGuid), ExcursionId = Guid.Parse(excursionGuid) };
            ExcursionSight excursionSight = new ExcursionSight { SightId = Guid.Parse("b38a98f9-61fd-4484-af1b-08d5e56ebea2"), ExcursionId = Guid.Parse("49255a22-44f6-4e78-0cfa-08d5e56ebea0") };
            List<ExcursionSight> excursionSights = new List<ExcursionSight> { excursionSight };
            mockExcursionSightRepository = new Mock<IExcursionSightRepository>(MockBehavior.Strict);
            mockExcursionSightRepository.Setup(tr => tr.Delete(Guid.Parse(excursionGuid), Guid.Parse(sightGuid))).Returns(Task.Run(() => {
                if (excursionSights.Contains(input))
                    return new Tuple<Guid, Guid>(Guid.Parse(excursionGuid), Guid.Parse(sightGuid));
                throw new Exception();
            }));
            excursionSightsController = new ExcursionSightsController(mockExcursionSightRepository.Object);
            Assert.That(excursionSightsController.DeleteExcursionSight(Guid.Parse(excursionGuid), Guid.Parse(sightGuid)).Result is BadRequestResult || (mockExcursionSightRepository.Object.Delete(Guid.Parse(excursionGuid), Guid.Parse(sightGuid)) != null && excursionSightsController.DeleteExcursionSight(Guid.Parse(excursionGuid), Guid.Parse(sightGuid)).Result is OkObjectResult));
        }
    }
}
