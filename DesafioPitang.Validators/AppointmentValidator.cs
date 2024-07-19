using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Constants;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;

namespace DesafioPitang.Validators
{
    public static class AppointmentValidator
    {
        public static void EnsureAppointmentExists(bool Exists)
        {
            if (Exists == false)
            {
                throw new BusinessException(BusinessMessages.ElementNotFound);
            }
        }

        public static void EnsureUserIsAuthorized(int currentUserId, int UserId, string profile)
        {
            if (profile != PermissionsConstants.ADMIN) 
            { 
                if (currentUserId != UserId)
                {
                    throw new BusinessAuthenticationException(AuthorizationMessages.InvalidUserOperation);
                }
            }
        }

        public static void ValidateGetByDate(DateTime initialDate, DateTime finalDate)
        {
            if (initialDate > finalDate)
            {
                throw new BusinessException(BusinessMessages.InvalidDateRange);
            }
        }

        public static void ValidateStatusChange(AppointmentStatusUpdateModel statusModel)
        {
            if (statusModel.Status != AppointmentStatusConstants.ACCOMPLISHED && 
                statusModel.Status != AppointmentStatusConstants.WAITING)
            {
                throw new BusinessException(string.Format(BusinessMessages.InvalidAppointmentStatus, statusModel.Status));
            }
        }


    }
}
