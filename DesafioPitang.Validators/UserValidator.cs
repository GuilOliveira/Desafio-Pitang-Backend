using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Constants;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;
using System.Text.RegularExpressions;

namespace DesafioPitang.Validators
{
    public static class UserValidator
    {
        public static void ValidateEmailIsUnique(User user)
        {
            if(user!=null)
            {
                throw new BusinessAuthenticationException(AuthorizationMessages.EmailAlreadyExists);
            }
        }
        public static void ValidateRegisterFields(UserRegistrationModel userRegistration)
        {
            var errors = new List<string>();

            // Email validation
            if (string.IsNullOrEmpty(userRegistration.Email))
            {
                errors.Add(BusinessMessages.EmptyUserEmail);
            }
            else if (!IsValidEmail(userRegistration.Email))
            {
                errors.Add(BusinessMessages.InvalidUserEmail);
            }

            // Name validation
            if (string.IsNullOrEmpty(userRegistration.Name))
            {
                errors.Add(BusinessMessages.EmptyUserName);
            }
            else if (userRegistration.Name.Any(char.IsDigit))
            {
                errors.Add(BusinessMessages.UserNameWithNumbers);
            }

            // Password validation
            if (string.IsNullOrEmpty(userRegistration.Password))
            {
                errors.Add(BusinessMessages.EmptyUserPassword);
            }
            else if (userRegistration.Password.Length < 6)
            {
                errors.Add(BusinessMessages.ShortUserPassword);
            }

            // Profile validation
            if (userRegistration.Profile != PermissionsConstants.ADMIN &&
                userRegistration.Profile != PermissionsConstants.COMMON)
            {
                errors.Add(string.Format(BusinessMessages.InvalidUserProfile, userRegistration.Profile));
            }

            if (errors.Any())
            {
                throw new BusinessListException(errors);
            }
        }

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
    }
}
