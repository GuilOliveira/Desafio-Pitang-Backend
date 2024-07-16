using DesafioPitang.Business.Businesses;
using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Repository;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Repository.Repositories;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DesafioPitang.UnitTests
{
    public class AppointmentDeleteTest : BaseUnitTest
    {
        private IAppointmentBusiness _business;
        private IAppointmentRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<Context>()
                              .UseInMemoryDatabase("InMemoryDatabase")
                              .Options;

            _context = new Context(options);

            _repository = new AppointmentRepository(_context);
            RegisterObject(typeof(IAppointmentRepository), _repository);

            Register<IAppointmentBusiness, AppointmentBusiness>();

            _business = GetService<IAppointmentBusiness>();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task DeleteById_DeleteCorrectly(int id)
        {
            // Arrange and Act
            await _business.DeleteById(id);
            var isDeleted = !await _repository.ExistsById(id);

            // Assert
            Assert.That(isDeleted, Is.True);
        }

        [TestCase(100)]
        [TestCase(-100)]
        public async Task DeleteById_ThrowsBusinessException_WhenIdBeInvalid(int id)
        {
            // Arrange and Act 
            var ex = Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _business.DeleteById(id);
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo(BusinessMessages.ElementNotFound));
        }
    }

}
