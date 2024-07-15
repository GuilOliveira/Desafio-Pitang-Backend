using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;

namespace DesafioPitang.Validators
{
    public static class ScheduleValidator
    {
        public static void ValidatePostFields(SchedulingModel schedule)
        {
            var errors = new List<string>();
            // Name validation
            if (string.IsNullOrEmpty(schedule.PatientName))
            {
                errors.Add(BusinessMessages.EmptySchedulePatientName);
            }
            if (schedule.PatientName.Any(char.IsDigit))
            {
                errors.Add(BusinessMessages.PatientNameWithNumbers);
            }
            // Birthdate validation
            if (schedule.PatientBirthDate > DateTime.Today)
            {
                errors.Add(BusinessMessages.InvalidScheduleBirthdate);
            }
            // Appointment Date validation
            if (schedule.AppointmentDate < DateTime.Today)
            {
                errors.Add(BusinessMessages.InvalidScheduleAppointmentDate);
            }
            // Appointment Time validation
            if (schedule.AppointmentTime.Minutes != 0 ||
                schedule.AppointmentTime.Seconds != 0)
            {
                errors.Add(BusinessMessages.InvalidScheduleTimeRange);
            }
            if (schedule.AppointmentTime.Hours > 20 ||
               schedule.AppointmentTime.Hours < 5)
            {
                errors.Add(BusinessMessages.InvalidScheduleTime);
            }

            if (errors.Any())
            {
                throw new BusinessListException(errors);
            }

        }

        public static void ValidatePostAvailability(int appointmentsOnDate, int appointmentsOnTime)
        {
            if (appointmentsOnDate >= 20)
            {
                throw new BusinessException(BusinessMessages.FullScheduleDate);
            }
            if(appointmentsOnTime >= 2)
            {
                throw new BusinessException(BusinessMessages.FullScheduleTime);
            }
        }
    }
}
