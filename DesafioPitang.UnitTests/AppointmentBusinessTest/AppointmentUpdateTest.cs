using DesafioPitang.Business.Businesses;
using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Repository.Repositories;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;
using DesafioPitang.Utils.UserContext;
using DesafioPitang.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DesafioPitang.UnitTests
{
    public class AppointmentUpdateTest : BaseUnitTest
    {
        private IAppointmentBusiness _business;
        private IAppointmentRepository _repository;
        private IUserContext _userContext;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<Context>()
                              .UseInMemoryDatabase("InMemoryDatabase")
                              .Options;

            _context = new Context(options);
            _userContext = new UserContext();

            _repository = new AppointmentRepository(_context);
            RegisterObject(typeof(IAppointmentRepository), _repository);
            RegisterObject(typeof(IUserContext), _userContext);

            Register<IAppointmentBusiness, AppointmentBusiness>();

            var userContext = GetService<IUserContext>();
            userContext.AddData("Id", "1");
            userContext.AddData("Role", "admin");

            _business = GetService<IAppointmentBusiness>();

        }

        [TestCase("Waiting")]
        [TestCase("Accomplished")]
        public async Task UpdateStatus_ReturnsTheNewStatus(string status)
        {
            // Arrange
            var statusModel = new AppointmentStatusUpdateModel { Id = 1, Status = status };

            // Act 
            var changedAppointmentDTO = await _business.UpdateStatus(statusModel);
            var newAppointment = await _repository.GetById(statusModel.Id);

            // Assert
            Assert.That(changedAppointmentDTO, Is.Not.Null);
            Assert.That(changedAppointmentDTO.Status, Is.EqualTo(status));
            Assert.That(changedAppointmentDTO.Status, Is.EqualTo(newAppointment.Status));
        }

        [TestCase(20)]
        [TestCase(200)]
        [TestCase(-1)]
        public async Task UpdateStatus_ThrowsBusinessException_WhenIdBeInvalid(int id)
        {
            // Arrange
            var statusModel = new AppointmentStatusUpdateModel { Id = id, Status = "Waiting" };

            // Act 
            var ex = Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _business.UpdateStatus(statusModel);
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo(BusinessMessages.ElementNotFound));
        }

        [TestCase("123")]
        [TestCase("AAAAAAA")]
        [TestCase("")]
        public async Task UpdateStatus_ThrowsBusinessException_WhenStatusBeInvalid(string status)
        {
            // Arrange
            var statusModel = new AppointmentStatusUpdateModel { Id = 1, Status = status };

            // Act 
            var ex = Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _business.UpdateStatus(statusModel);
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo(String.Format(BusinessMessages.InvalidAppointmentStatus, status)));
        }

    }

}
