using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TourApi.Models;
using TourApi.Repos;
using Microsoft.AspNetCore.Mvc;

namespace TourApi.Controllers.Tests
{
    [TestFixture]
    public class ToursControllerTest
    {
        Mock<IToursRepository> mockTourRepository;
        ToursController toursController;
        [Test]
        public void GetTours_ReturnsOkResult()
        {
            Tour input = new Tour { Date = DateTimeOffset.Now, ClientId = Guid.Parse("49255a22-44f6-4e78-0cfa-08d5e56ebea0"), ExcursionId = Guid.Parse("b38a98f9-61fd-4484-af1b-08d5e56ebea2") };
            Tour tour = new Tour { Id = Guid.NewGuid(), Date = input.Date, ClientId = input.ClientId, ExcursionId = input.ExcursionId };
            Tour tour2 = new Tour { Id = Guid.NewGuid(), Date = input.Date, ClientId = Guid.Parse("49255a22-44f6-4e78-2dfa-08d5e56ebea0"), ExcursionId = Guid.Parse("b38b48f9-61fd-4484-af1b-08d5e56ebea2") };
            mockTourRepository = new Mock<IToursRepository>(MockBehavior.Strict);
            mockTourRepository.Setup(tr => tr.GetAll()).Returns(Task.Run(() => {
                return new List<Tour> { tour, tour2 };
            }));
            toursController = new ToursController(mockTourRepository.Object);
            OkObjectResult ok = new OkObjectResult(new List<Tour> { tour, tour2 });
            Assert.That(toursController.GetTours().Result is OkObjectResult);
        }
        [TestCase("12355a22-44f6-4e78-0cfa-08d5e56ebea0")]
        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0")]
        public void GetTour_ExistingTour_ReturnsResult(string guid)
        {
            Guid id = Guid.Parse(guid);
            Tour tour = new Tour { Id = Guid.NewGuid(), Date = DateTimeOffset.Now, ClientId = Guid.Parse("49255a22-44f6-4e78-0cfa-08d5e56ebea0"), ExcursionId = Guid.Parse("b38a98f9-61fd-4484-af1b-08d5e56ebea2") };
            Tour tour2 = new Tour { Id = Guid.Parse("12355a22-44f6-4e78-0cfa-08d5e56ebea0"), Date = DateTimeOffset.Now, ClientId = Guid.Parse("49255a22-44f6-3e78-2dfa-08d5e56ebea0"), ExcursionId = Guid.Parse("d38b48f9-61fd-4484-af1b-08d5e56ebea2") };
            List<Tour> tours = new List<Tour> { tour, tour2 };
            mockTourRepository = new Mock<IToursRepository>(MockBehavior.Strict);
            mockTourRepository.Setup(tr => tr.Get(id)).Returns(Task.Run(() => {
                return tours.FirstOrDefault(x => x.Id == id);
            }));
            toursController = new ToursController(mockTourRepository.Object);
            Assert.That(toursController.GetTour(id).Result is BadRequestResult || (mockTourRepository.Object.Get(id) != null && toursController.GetTour(id).Result is OkObjectResult));
        }
        [TestCase("12355a22-44f6-4e78-0cfa-08d5e56ebea0")]
        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0")]
        public void ChangeTour_ExistingOrUnexisting_ReturnsBadOrOkObjectRequest(string guid)
        {
            Guid id = Guid.Parse(guid);
            Tour tour = new Tour { Id = id, Date = DateTimeOffset.Now };
            Tour input = new Tour { Id = Guid.NewGuid(), Date = DateTimeOffset.Now, ClientId = Guid.Parse("49255a22-44f6-4e78-0cfa-08d5e56ebea0"), ExcursionId = Guid.Parse("b38a98f9-61fd-4484-af1b-08d5e56ebea2") };
            Tour tour2 = new Tour { Id = Guid.Parse("12355a22-44f6-4e78-0cfa-08d5e56ebea0"), Date = DateTimeOffset.Now, ClientId = Guid.Parse("49255a22-44f6-3e78-2dfa-08d5e56ebea0"), ExcursionId = Guid.Parse("d38b48f9-61fd-4484-af1b-08d5e56ebea2") };
            List<Tour> tours = new List<Tour> { tour, tour2 };
            mockTourRepository = new Mock<IToursRepository>(MockBehavior.Strict);
            mockTourRepository.Setup(tr => tr.Update(input)).Returns(Task.Run(() => {
                return tours.FirstOrDefault(x => x.Id == input.Id);
            }));
            toursController = new ToursController(mockTourRepository.Object);
            Assert.That(toursController.ChangeTour(id, input).Result is BadRequestResult || (mockTourRepository.Object.Update(tour) != null && toursController.ChangeTour(id, input).Result is OkObjectResult));

        }
        [TestCase("12355a22-44f6-4e78-0cfa-08d5e56ebea0")]
        [TestCase("49255a22-44f6-4e78-0cfa-08d5e56ebea0")]
        public void DeleteTour_ExistingOrUnexisting_ReturnsBadOrOkObjectRequest(string guid)
        {
            Guid id = Guid.Parse(guid);
            Tour tour = new Tour { Id = id, Date = DateTimeOffset.Now };
            Tour input = new Tour { Id = Guid.NewGuid(), Date = DateTimeOffset.Now, ClientId = Guid.Parse("49255a22-44f6-4e78-0cfa-08d5e56ebea0"), ExcursionId = Guid.Parse("b38a98f9-61fd-4484-af1b-08d5e56ebea2") };
            Tour tour2 = new Tour { Id = Guid.Parse("12355a22-44f6-4e78-0cfa-08d5e56ebea0"), Date = DateTimeOffset.Now, ClientId = Guid.Parse("49255a22-44f6-3e78-2dfa-08d5e56ebea0"), ExcursionId = Guid.Parse("d38b48f9-61fd-4484-af1b-08d5e56ebea2") };
            List<Tour> tours = new List<Tour> { tour, tour2 };
            mockTourRepository = new Mock<IToursRepository>(MockBehavior.Strict);
            mockTourRepository.Setup(tr => tr.Delete(id)).Returns(Task.Run(() => {
                if (!tours.Contains(tours.FirstOrDefault(x => x.Id == id)))
                    throw new Exception();
                return id;
            }));
            toursController = new ToursController(mockTourRepository.Object);
            Assert.That(toursController.DeleteTour(id).Result is BadRequestResult || (mockTourRepository.Object.Delete(id) != null && toursController.DeleteTour(id).Result is OkObjectResult));

        }
        [Test]
        public void AddTour_NewTour_ReturnsOkResult()
        {
            Tour input = new Tour { Date = DateTimeOffset.Now, ClientId = Guid.Parse("49255a22-44f6-4e78-0cfa-08d5e56ebea0"), ExcursionId = Guid.Parse("b38a98f9-61fd-4484-af1b-08d5e56ebea2") };
            Guid guid = Guid.NewGuid();
            Tour tour = new Tour { Id = guid, Date = input.Date, ClientId = input.ClientId, ExcursionId = input.ExcursionId };
            mockTourRepository = new Mock<IToursRepository>(MockBehavior.Strict);
            mockTourRepository.Setup(tr => tr.Create(input)).Returns(Task.Run(() => {
                return tour;
            }));
            toursController = new ToursController(mockTourRepository.Object);
            OkObjectResult ok = new OkObjectResult(tour);
            Assert.That(toursController.AddTour(input).Result is OkObjectResult); 
        }
        [Test]
        public void AddTour_NewTour_ReturnsBadRequest()
        {
            Tour input = new Tour { Id = Guid.Parse("49255a12-44f6-4e78-0cfa-08d5e56ebea0"), Date = DateTimeOffset.Now, ClientId = Guid.Parse("49255a22-44f6-4e78-0cfa-08d5e56ebea0"), ExcursionId = Guid.Parse("b38a98f9-61fd-4484-af1b-08d5e56ebea2") };
            mockTourRepository = new Mock<IToursRepository>(MockBehavior.Strict);
            mockTourRepository.Setup(tr => tr.Create(input)).Returns(Task.Run(() => {
                if(input.Id == Guid.Parse("49255a12-44f6-4e78-0cfa-08d5e56ebea0"))
                {
                    throw new Exception();
                }
                return input;
            }));
            toursController = new ToursController(mockTourRepository.Object);
            Assert.That(toursController.AddTour(input).Result is BadRequestResult);
        }
    }
}
