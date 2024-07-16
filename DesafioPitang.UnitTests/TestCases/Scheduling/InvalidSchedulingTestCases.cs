using DesafioPitang.Entities.Models;
using DesafioPitang.Utils.Messages;

namespace DesafioPitang.UnitTests.TestCases.Scheduling
{
    public static class InvalidSchedulingTestCases
    {
        public static IEnumerable<SchedulingTestCase> GetTestCases()
        {
            yield return new SchedulingTestCase
            {
                Model = new SchedulingModel
                {
                    PatientName = "name",
                    PatientBirthDate = DateTime.Today.AddDays(2),
                    AppointmentDate = DateTime.Today.AddDays(1),
                    AppointmentTime = new TimeSpan(8, 0, 0)
                },
                ExpectedErrorMessage = BusinessMessages.InvalidScheduleBirthdate
            };
            yield return new SchedulingTestCase
            {
                Model = new SchedulingModel
                {
                    PatientName = "name",
                    PatientBirthDate = new DateTime(2000, 1, 1),
                    AppointmentDate = DateTime.Today.AddDays(-2),
                    AppointmentTime = new TimeSpan(8, 0, 0)
                },
                ExpectedErrorMessage = BusinessMessages.InvalidScheduleAppointmentDate
            };
            yield return new SchedulingTestCase
            {
                Model = new SchedulingModel
                {
                    PatientName = "name",
                    PatientBirthDate = new DateTime(2000, 1, 1),
                    AppointmentDate = DateTime.Today.AddDays(1),
                    AppointmentTime = new TimeSpan(8, 15, 0)
                },
                ExpectedErrorMessage = BusinessMessages.InvalidScheduleTimeRange
            };
            yield return new SchedulingTestCase
            {
                Model = new SchedulingModel
                {
                    PatientName = "name",
                    PatientBirthDate = new DateTime(2000, 1, 1),
                    AppointmentDate = DateTime.Today.AddDays(1),
                    AppointmentTime = new TimeSpan(12, 0, 30)
                },
                ExpectedErrorMessage = BusinessMessages.InvalidScheduleTimeRange
            };
            yield return new SchedulingTestCase
            {
                Model = new SchedulingModel
                {
                    PatientName = "name",
                    PatientBirthDate = new DateTime(2000, 1, 1),
                    AppointmentDate = DateTime.Today.AddDays(1),
                    AppointmentTime = new TimeSpan(21, 0, 0)
                },
                ExpectedErrorMessage = BusinessMessages.InvalidScheduleTime
            };
            yield return new SchedulingTestCase
            {
                Model = new SchedulingModel
                {
                    PatientName = "name",
                    PatientBirthDate = new DateTime(2000, 1, 1),
                    AppointmentDate = DateTime.Today.AddDays(1),
                    AppointmentTime = new TimeSpan(4, 0, 0)
                },
                ExpectedErrorMessage = BusinessMessages.InvalidScheduleTime
            };
        }
    }

}
