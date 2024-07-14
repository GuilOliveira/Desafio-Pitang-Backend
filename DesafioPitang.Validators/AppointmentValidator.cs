using DesafioPitang.Entities.Models;
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
                throw new BusinessException(string.Format(BusinessMessages.ElementNotFound));
            }
        }

        public static void ValidateGetByDate(DateTime initialDate, DateTime finalDate)
        {
            if (initialDate > finalDate)
            {
                throw new BusinessException(string.Format(BusinessMessages.InvalidDateRange));
            }
        }

        public static void ValidateStatusChange(AppointmentStatusUpdateModel statusModel)
        {
            if (statusModel.Status != "Accomplished" && statusModel.Status != "Waiting")
            {
                throw new BusinessException(string.Format(BusinessMessages.InvalidAppointmentStatus, statusModel.Status));
            }
        }


    }
}
