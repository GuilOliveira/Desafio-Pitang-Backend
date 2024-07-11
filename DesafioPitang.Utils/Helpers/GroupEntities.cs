using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;

namespace DesafioPitang.Utils.Helpers
{
    public static class GroupEntities
    {
        public static List<List<AppointmentDTO>> GroupAppointmentsByDate(IEnumerable<Appointment> appointments)
        {
            return appointments
                .GroupBy(a => a.Date.Date)
                .Select(g => g.Select(a => new AppointmentDTO
                {
                    Id = a.Id,
                    Date = a.Date,
                    Time = a.Time,
                    Status = a.Status,
                    PatientName = a.Patient?.Name
                }).ToList())
                .ToList();
        }
    }
}
