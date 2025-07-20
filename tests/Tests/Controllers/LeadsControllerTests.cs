using API.Controllers;
using Application.DTOs.Leads;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.Controllers
{
    public class LeadsControllerTests
    {
        private readonly Mock<ILeadService> _mockLeadService;
        private readonly LeadsController _controller;

        public LeadsControllerTests()
        {
            _mockLeadService = new Mock<ILeadService>();
            _controller = new LeadsController(_mockLeadService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfLeads()
        {
            // Arrange
            var mockLeads = new List<LeadResponseDto>
            {
                new LeadResponseDto
                {
                    Id = 1,
                    PlaceId = 100,
                    AppointmentAt = DateTime.UtcNow.AddDays(1),
                    ServiceType = "cambio_aceite",
                    Contact = new ContactDto { Name = "Juan", Email = "juan@mail.com", Phone = "123456" },
                    Vehicle = new VehicleDto { Make = "Toyota", Model = "Corolla", Year = 2020, LicensePlate = "ABC123" }
                },
                new LeadResponseDto
                {
                    Id = 2,
                    PlaceId = 200,
                    AppointmentAt = DateTime.UtcNow.AddDays(2),
                    ServiceType = "rotacion_neumaticos",
                    Contact = new ContactDto { Name = "Ana", Email = "ana@mail.com", Phone = "654321" },
                    Vehicle = new VehicleDto { Make = "Ford", Model = "Fiesta", Year = 2018, LicensePlate = "XYZ789" }
                }
            };
            _mockLeadService.Setup(service => service.GetAllAsync()).ReturnsAsync(mockLeads);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
            var returnValue = Assert.IsType<List<LeadResponseDto>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenLeadExists()
        {
            // Arrange
            var mockLead = new LeadResponseDto
            {
                Id = 1,
                PlaceId = 100,
                AppointmentAt = DateTime.UtcNow.AddDays(1),
                ServiceType = "cambio_aceite",
                Contact = new ContactDto { Name = "Juan", Email = "juan@mail.com", Phone = "123456" },
                Vehicle = new VehicleDto { Make = "Toyota", Model = "Corolla", Year = 2020, LicensePlate = "ABC123" }
            };
            _mockLeadService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(mockLead);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
            var returnValue = Assert.IsType<LeadResponseDto>(actionResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFoundResult_WhenLeadDoesNotExist()
        {
            // Arrange
            _mockLeadService.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((LeadResponseDto)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(StatusCodes.Status404NotFound, actionResult.StatusCode);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult_WhenLeadIsCreated()
        {
            // Arrange
            var request = new LeadRequestDto
            {
                PlaceId = 100,
                AppointmentAt = DateTime.UtcNow.AddDays(1),
                ServiceType = "cambio_aceite",
                Contact = new ContactDto { Name = "Juan", Email = "juan@mail.com", Phone = "123456" },
                Vehicle = new VehicleDto { Make = "Toyota", Model = "Corolla", Year = 2020, LicensePlate = "ABC123" }
            };
            var createdLead = new LeadResponseDto
            {
                Id = 1,
                PlaceId = 100,
                AppointmentAt = request.AppointmentAt,
                ServiceType = request.ServiceType,
                Contact = request.Contact,
                Vehicle = request.Vehicle
            };
            _mockLeadService.Setup(service => service.AddAsync(request)).ReturnsAsync(createdLead);

            // Act
            var result = await _controller.Create(request);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(StatusCodes.Status201Created, actionResult.StatusCode);
            var returnValue = Assert.IsType<LeadResponseDto>(actionResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("cambio_aceite", returnValue.ServiceType);
        }
    }
}
