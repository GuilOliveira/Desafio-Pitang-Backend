using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Constants;
using DesafioPitang.Utils.Messages;

namespace DesafioPitang.UnitTests.TestCases.Scheduling
{
    public static class InvalidUserTestCases
    {
       public static IEnumerable<UserRegistrationTestCase> GetTestCases()
            {
                yield return new UserRegistrationTestCase
                {
                    Model = new UserRegistrationModel
                    {
                        Name = "name",
                        Email = "",
                        Password = "123123",
                        Profile = PermissionsConstants.ADMIN
                    },
                    ExpectedErrorMessage = BusinessMessages.EmptyUserEmail
                };
                yield return new UserRegistrationTestCase
                {
                    Model = new UserRegistrationModel
                    {
                        Name = "name",
                        Email = "invalid-email",
                        Password = "123123",
                        Profile = PermissionsConstants.ADMIN
                    },
                    ExpectedErrorMessage = BusinessMessages.InvalidUserEmail
                };
                yield return new UserRegistrationTestCase
                {
                    Model = new UserRegistrationModel
                    {
                        Name = "name",
                        Email = "name@gmail.com",
                        Password = "",
                        Profile = PermissionsConstants.ADMIN
                    },
                    ExpectedErrorMessage = BusinessMessages.EmptyUserPassword
                };
                yield return new UserRegistrationTestCase
                {
                    Model = new UserRegistrationModel
                    {
                        Name = "name",
                        Email = "name@gmail.com",
                        Password = "123",
                        Profile = PermissionsConstants.ADMIN
                    },
                    ExpectedErrorMessage = BusinessMessages.ShortUserPassword
                };
                yield return new UserRegistrationTestCase
                {
                    Model = new UserRegistrationModel
                    {
                        Name = "name",
                        Email = "name@gmail.com",
                        Password = "123123",
                        Profile = "invalid-profile"
                    },
                    ExpectedErrorMessage = string.Format(BusinessMessages.InvalidUserProfile, "invalid-profile")
                };
            }
    }

}
