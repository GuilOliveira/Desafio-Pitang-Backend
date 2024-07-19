using DesafioPitang.Business.Businesses;
using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Repository.Repositories;
using DesafioPitang.UnitTests.TestCases.Scheduling;
using DesafioPitang.Utils.Constants;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;
using DesafioPitang.Utils.UserContext;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DesafioPitang.UnitTests.UserBusinessTest
{
    public class UserCreateTest : BaseUnitTest
    {
        private IUserBusiness _business;
        private IUserRepository _userRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase("InMemoryDatabase")
                .Options;
            _context = new Context(options);

            _userRepository = new UserRepository(_context);

            RegisterObject(typeof(IUserRepository), _userRepository);
            Register<IUserBusiness, UserBusiness>();
            Register<IUserContext, UserContext>();

            _business = GetService<IUserBusiness>();
        }

        [Test]
        public async Task Register_CreateNewUserCorrectly()
        {
            // Arrange
            var userModel = new UserRegistrationModel
            {
                Name = "name",
                Email = "name@gmail.com",
                Password = "123123",
                Profile = PermissionsConstants.ADMIN
            };

            // Act 
            await _business.Register(userModel);
            var user = await _userRepository.GetByEmail(userModel.Email);

            // Assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Profile, Is.EqualTo(userModel.Profile));
            Assert.That(user.Id, Is.GreaterThan(0));
            Assert.That(user.Name, Is.EqualTo(userModel.Name));
            Assert.That(user.Email, Is.EqualTo(userModel.Email));
        }

        private static IEnumerable<TestCaseData> InvalidNameTestCases()
        {
            yield return new TestCaseData("", BusinessMessages.EmptyUserName);
            yield return new TestCaseData("123", BusinessMessages.UserNameWithNumbers);
            yield return new TestCaseData("Name With 1 number", BusinessMessages.UserNameWithNumbers);
        }

        [TestCaseSource(nameof(InvalidNameTestCases))]
        public async Task Register_ThrowsBusinessListException_WhenNameIsInvalid(string name, string errorMessage)
        {
            // Arrange
            var userModel = new UserRegistrationModel
            {
                Name = name,
                Email = name+"@gmail.com",
                Password = "123123",
                Profile = PermissionsConstants.COMMON
            };

            // Act 
            var ex = Assert.ThrowsAsync<BusinessListException>(async () =>
            {
                await _business.Register(userModel);
            });

            // Assert
            Assert.That(ex.Messages, Does.Contain(errorMessage));
        }

        [TestCaseSource(typeof(InvalidUserTestCases), nameof(InvalidUserTestCases.GetTestCases))]
        public async Task Register_ThrowsBusinessListException_WhenUserModelIsInvalid(UserRegistrationTestCase testCase)
        {
            // Arrange
            var userModel = testCase.Model;

            // Act 
            var ex = Assert.ThrowsAsync<BusinessListException>(async () =>
            {
                await _business.Register(userModel);
            });

            // Assert
            Assert.That(ex.Messages, Does.Contain(testCase.ExpectedErrorMessage));
        }

        [Test]
        public async Task Post_ThrowsBusinessListException_WhenVariousUserFieldsAreInvalid()
        {
            // Arrange
            var userModel = new UserRegistrationModel
            {
                Name = "its 1 name",
                Email = string.Empty,
                Password = "a",
                Profile = "invalid"
                
            };

            // Act 
            var ex = Assert.ThrowsAsync<BusinessListException>(async () =>
            {
                await _business.Register(userModel);
            });

            // Assert
            Assert.That(ex.Messages, Does.Contain(BusinessMessages.UserNameWithNumbers));
            Assert.That(ex.Messages, Does.Contain(BusinessMessages.EmptyUserEmail));
            Assert.That(ex.Messages, Does.Contain(BusinessMessages.ShortUserPassword));
            Assert.That(ex.Messages, Does.Contain(string.Format(BusinessMessages.InvalidUserProfile, userModel.Profile)));
        }
        
        [Test]
        public async Task Register_ThrowsBusinessAuthenticationException_WhenEmailIsAlreadyUsed()
        {
            // Arrange
            var userModel = new UserRegistrationModel
            {
                Name = "name",
                Email = "gui@pitang.com",
                Password = "123123",
                Profile = PermissionsConstants.ADMIN
            };

            // Act 
            var ex = Assert.ThrowsAsync<BusinessAuthenticationException>(async () =>
            {
                await _business.Register(userModel);
            });

            // Assert
            Assert.That(ex.Message, Is.EqualTo(AuthorizationMessages.EmailAlreadyExists));
        }
    }
}
