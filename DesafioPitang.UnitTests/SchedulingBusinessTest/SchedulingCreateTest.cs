using DesafioPitang.Business.Businesses;
using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Repository.Repositories;
using DesafioPitang.UnitTests.TestCases.Scheduling;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DesafioPitang.UnitTests
{
    public class SchedulingCreateTest : BaseUnitTest
    {
        private ISchedulingBusiness _business;
        private IAppointmentRepository _appointmentRepository;
        private IPatientRepository _patientRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<Context>()
                              .UseInMemoryDatabase("InMemoryDatabase")
                              .Options;

            _context = new Context(options);

            _appointmentRepository = new AppointmentRepository(_context);
            _patientRepository = new PatientRepository(_context);

            RegisterObject(typeof(IAppointmentRepository), _appointmentRepository);
            RegisterObject(typeof(IPatientRepository), _patientRepository);

            Register<ISchedulingBusiness, SchedulingBusiness>();

            _business = GetService<ISchedulingBusiness>();
        }

        [Test]
        public async Task Post_CreateNewSchedulingCorrectly()
        {
            // Arrange
            var schedulingModel = new SchedulingModel
            {
                PatientName = "name",
                PatientBirthDate = new DateTime(2000, 1, 1),
                AppointmentDate = DateTime.Today.AddDays(1),
                AppointmentTime = new TimeSpan(8, 0, 0)
            };

            // Act 
            var scheduling = await _business.Post(schedulingModel);

            // Assert
            Assert.That(scheduling, Is.Not.Null);
            Assert.That(scheduling.Status, Is.EqualTo("Waiting"));
            Assert.That(scheduling.Id, Is.GreaterThan(0));
            Assert.That(scheduling.PatientName, Is.EqualTo(schedulingModel.PatientName));
            Assert.That(scheduling.Date, Is.EqualTo(schedulingModel.AppointmentDate.Date));
            Assert.That(scheduling.Time, Is.EqualTo(schedulingModel.AppointmentTime));
        }

        private static IEnumerable<TestCaseData> InvalidNameTestCases()
        {
            yield return new TestCaseData("", BusinessMessages.EmptySchedulePatientName);
            yield return new TestCaseData("123", BusinessMessages.PatientNameWithNumbers);
            yield return new TestCaseData("Name With 1 number", BusinessMessages.PatientNameWithNumbers);
        }

        [TestCaseSource(nameof(InvalidNameTestCases))]
        public async Task UpdateStatus_ThrowsBusinessListException_WhenNameIsInvalid(string name, string errorMessage)
        {
            // Arrange
            var schedulingModel = new SchedulingModel
            {
                PatientName = name,
                PatientBirthDate = new DateTime(2000, 1, 1),
                AppointmentDate = DateTime.Today.AddDays(1),
                AppointmentTime = new TimeSpan(8, 0, 0)
            };

            // Act 
            var ex = Assert.ThrowsAsync<BusinessListException>(async () =>
            {
                await _business.Post(schedulingModel);
            });

            // Assert
            Assert.That(ex.Messages, Does.Contain(errorMessage));
        }

        [TestCaseSource(typeof(InvalidSchedulingTestCases), nameof(InvalidSchedulingTestCases.GetTestCases))]
        public async Task Post_ThrowsBusinessListException_WhenModelIsInvalid(SchedulingTestCase testCase)
        {
            // Arrange
            var schedulingModel = testCase.Model;

            // Act 
            var ex = Assert.ThrowsAsync<BusinessListException>(async () =>
            {
                await _business.Post(schedulingModel);
            });

            // Assert
            Assert.That(ex.Messages, Does.Contain(testCase.ExpectedErrorMessage));
        }

        [Test]
        public async Task Post_ThrowsBusinessListException_WhenVariousFieldsAreInvalid()
        {
            // Arrange
            var schedulingModel = new SchedulingModel
            {
                PatientName = "its 1 name",
                PatientBirthDate = DateTime.Today.AddDays(2),
                AppointmentDate = DateTime.Today.AddDays(-2),
                AppointmentTime = new TimeSpan(2, 2, 2)
            };

            // Act 
            var ex = Assert.ThrowsAsync<BusinessListException>(async () =>
            {
                await _business.Post(schedulingModel);
            });

            // Assert
            Assert.That(ex.Messages, Does.Contain(BusinessMessages.PatientNameWithNumbers));
            Assert.That(ex.Messages, Does.Contain(BusinessMessages.InvalidScheduleBirthdate));
            Assert.That(ex.Messages, Does.Contain(BusinessMessages.InvalidScheduleAppointmentDate));
            Assert.That(ex.Messages, Does.Contain(BusinessMessages.InvalidScheduleTimeRange));
            Assert.That(ex.Messages, Does.Contain(BusinessMessages.InvalidScheduleTime));
        }

        [Test]
        public async Task Post_ThrowsBusinessException_WhenScheduleTimeIsAlreadyFull()
        {
            // Arrange
            var schedulingModel = new SchedulingModel
            {
                PatientName = "name",
                PatientBirthDate = DateTime.Today.AddDays(-3),
                AppointmentDate = DateTime.Today.AddDays(9),
                AppointmentTime = new TimeSpan(14, 0, 0)
            };

            // Act 
            await _business.Post(schedulingModel);
            await _business.Post(schedulingModel);
            var ex = Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _business.Post(schedulingModel);
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo(BusinessMessages.FullScheduleTime));
        }

        [Test]
        public async Task Post_ThrowsBusinessException_WhenScheduleDateIsAlreadyFull()
        {
            // Arrange
            var schedulingModelBase = new SchedulingModel
            {
                PatientName = "name",
                PatientBirthDate = DateTime.Today.AddDays(-3),
                AppointmentDate = DateTime.Today.AddDays(7)
            };

            for (int i = 0; i < 20; i++)
            {
                var schedulingModel = new SchedulingModel
                {
                    PatientName = schedulingModelBase.PatientName,
                    PatientBirthDate = schedulingModelBase.PatientBirthDate,
                    AppointmentDate = schedulingModelBase.AppointmentDate,
                    AppointmentTime = new TimeSpan(8 + (i % 12), 0, 0)
                };
                await _business.Post(schedulingModel);
            }

            // Act
            var ex = Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _business.Post(new SchedulingModel
                {
                    PatientName = schedulingModelBase.PatientName,
                    PatientBirthDate = schedulingModelBase.PatientBirthDate,
                    AppointmentDate = schedulingModelBase.AppointmentDate,
                    AppointmentTime = new TimeSpan(8, 0, 0)
                });
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo(BusinessMessages.FullScheduleDate));
        }


    }
}
