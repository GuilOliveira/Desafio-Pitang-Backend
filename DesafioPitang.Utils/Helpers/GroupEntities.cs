using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;

namespace DesafioPitang.Utils.Helpers
{
    public static class GroupEntities
    {
        public static List<List<AppointmentDTO>> GroupAppointmentsByDate(IEnumerable<Appointment> appointments)
        {
            return appointments
                .GroupBy(appointment => appointment.Date.Date)
                .Select(group => group.Select(appointment => new AppointmentDTO
                {
                    Id = appointment.Id,
                    Date = appointment.Date,
                    Time = appointment.Time,
                    Status = appointment.Status,
                    PatientName = appointment.Patient?.Name
                }
                ).OrderBy(appointment => appointment.Time).ToList())
                .ToList();
        }
    }
}
