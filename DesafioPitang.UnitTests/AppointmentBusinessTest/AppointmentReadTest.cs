using DesafioPitang.Business.Businesses;
using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
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
    public class AppointmentReadTest : BaseUnitTest
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
            RegisterObject(typeof(IUserRepository), _repository);
            RegisterObject(typeof(IUserContext), _userContext);

            Register<IAppointmentBusiness, AppointmentBusiness>();

            _business = GetService<IAppointmentBusiness>();
        }

        [Test]
        public async Task GetAll_ReturnsGroupedAppointments()
        {

            // Arrange
            List<List<AppointmentDTO>> searchedAppointments;

            // Act
            searchedAppointments = await _business.GetAll();

            // Assert
            Assert.IsNotNull(searchedAppointments);
            Assert.That(searchedAppointments.Count, Is.EqualTo(4));
            Assert.That(searchedAppointments[0].Count, Is.EqualTo(1));
            Assert.That(searchedAppointments[1].Count, Is.EqualTo(1));
            Assert.That(searchedAppointments[2].Count, Is.EqualTo(1));
            Assert.That(searchedAppointments[3].Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetByDate_ReturnsGroupedAppointments()
        {
            // Arrange
            var firstInitialDate = DateTime.Today;
            var firstFinalDate = DateTime.Today.AddDays(3);

            var secondInitialDate = DateTime.Today.AddDays(1);
            var secondFinalDate = DateTime.Today.AddDays(3);

            // Act
            var firstResult = await _business.GetByDate(firstInitialDate, firstFinalDate);
            var secondResult = await _business.GetByDate(secondInitialDate, secondFinalDate);

            // Assert
            Assert.NotNull(firstResult);
            Assert.That(firstResult.Count, Is.EqualTo(4));
            Assert.That(firstResult[0].Count, Is.EqualTo(1));

            Assert.NotNull(secondResult);
            Assert.That(secondResult.Count, Is.EqualTo(3));
            Assert.That(firstResult[0].Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetByDate_ThrowsBusinessException_WhenInitialDateIsGreaterThanFinalDate()
        {
            // Arrange
            var initialDate = DateTime.Today.AddDays(3);
            var finalDate = DateTime.Today;

            // Act and Assert
            var ex = Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _business.GetByDate(initialDate, finalDate);
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo(BusinessMessages.InvalidDateRange));

        }

        [Test]
        public async Task GetAllByUser_ShouldReturnGroupedAppointmentsByUser()
        {

            // Arrange
            var userContext = GetService<IUserContext>();
            userContext.AddData("Id", "1");
            userContext.AddData("Role", "admin");

            // Act 
            var searchedAppointments = await _business.GetAllByUser();

            // Assert
            Assert.IsNotNull(searchedAppointments);
            Assert.That(searchedAppointments.Count, Is.EqualTo(4));
            Assert.That(searchedAppointments[0].Count, Is.EqualTo(1));
            Assert.That(searchedAppointments[1].Count, Is.EqualTo(1));

        }
    }

}
